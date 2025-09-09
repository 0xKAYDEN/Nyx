﻿using System;
using System.Collections.Generic;
using Nyx.Server.Network.GamePackets;

namespace Nyx.Server.Game.ConquerStructures
{
    public struct BoothItem
    {
        public void Regenerate(BoothItem item, Booth booth)
        {
            booth.ItemList.Remove(item.Item.UID);
            this.Cost = item.Cost;
            this.Item = new ConquerItem(true);
            this.Item.ID = item.Item.ID;
            this.Item.UID = ConquerItem.ItemUID.Next;
            this.Item.Plus = item.Item.Plus;
            this.Item.Enchant = item.Item.Enchant;
            this.Item.Bless = item.Item.Bless;
            this.Item.SocketOne = item.Item.SocketOne;
            this.Item.SocketTwo = item.Item.SocketTwo;
            this.Item.StackSize = item.Item.StackSize;
            this.Item.Bound = false;
            Database.ConquerItemBaseInformation CIBI = null;
            CIBI = Database.ConquerItemInformation.BaseInformations[this.Item.ID];
            if (CIBI == null)
                return;
            this.Item.Durability = CIBI.Durability;
            this.Item.MaximDurability = CIBI.Durability;
            this.Cost_Type = item.Cost_Type;
            booth.ItemList.Add(this.Item.UID, this);


        }
        public enum CostType : byte
        {
            Silvers = 1,
            ConquerPoints = 3
        }
        public ConquerItem Item;
        public uint Cost;
        public CostType Cost_Type;
    }
    public class Booth
    {
        public static Counter BoothCounter = new Counter(1) { Finish = 10000 };
        private static Dictionary<uint, Booth> Booths = new Dictionary<uint, Booth>();
        public static Dictionary<uint, Booth> Booths2 = new Dictionary<uint, Booth>();
        public static object SyncRoot = new Object();
        public static bool TryGetValue(uint uid, out Booth booth)
        {
            lock (SyncRoot)
                return Booths.TryGetValue(uid, out booth);
        }
        public static bool TryGetValue2(uint uid, out Booth booth)
        {
            lock (SyncRoot)
                return Booths2.TryGetValue(uid, out booth);
        }

        public SafeDictionary<uint, BoothItem> ItemList;
        Client.GameClient Owner;
        public SobNpcSpawn Base;
        public Network.GamePackets.Message HawkMessage;
        public Booth(Client.GameClient client, Data data)
        {
            Owner = client;
            Owner.Booth = this;
            Owner.Entity.Action = Enums.ConquerAction.Sit;
            ItemList = new SafeDictionary<uint, BoothItem>(20);
            Base = new SobNpcSpawn();
            Base.Owner = Owner;
            lock (SyncRoot)
            {
                Base.UID = BoothCounter.Next;
                while (Booths.ContainsKey(Base.UID))
                    Base.UID = BoothCounter.Next;
                Booths.Add(Base.UID, this);
            }
            Base.Mesh = 406;
            Base.Type = Game.Enums.NpcType.Booth;
            Base.ShowName = true;
            Base.Name = Name;
            Base.MapID = client.Entity.MapID;
            Base.X = (ushort)(Owner.Entity.X + 1);
            Base.Y = Owner.Entity.Y;
            Owner.SendScreenSpawn(Base, true);
            data.dwParam = Base.UID;
            data.wParam1 = Base.X;
            data.wParam2 = Base.Y;
            data.ID = Data.OwnBooth;
            Owner.Send(data);
        }

        public Booth()
        {
            ItemList = new SafeDictionary<uint, BoothItem>(20);

        }
        public string Name
        {
            get
            {
                return Owner.Entity.Name;
            }
        }
        public static implicit operator byte[](Booth booth)
        {
            return booth.Base.Encode();
        }
        public static implicit operator SobNpcSpawn(Booth booth)
        {
            return booth.Base;
        }
        public void Remove()
        {
            Network.GamePackets.Data data = new Network.GamePackets.Data(true);
            data.UID = Base.UID;
            data.ID = Network.GamePackets.Data.RemoveEntity;
            Owner.SendScreen(data, true);
            lock (SyncRoot) Booths.Remove(Base.UID);
        }
    }
}