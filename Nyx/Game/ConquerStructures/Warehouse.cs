using System;
using System.Linq;
using System.Collections.Generic;
using Nyx.Server.Network.GamePackets;

namespace Nyx.Server.Game.ConquerStructures
{
    public class Warehouse
    {
        Dictionary<uint, ConquerItem> items;
        ConquerItem[] objects;
        Client.GameClient Owner;
        private WarehouseID _ID;
        private ushort MaxCount = 20;

        public Warehouse(Client.GameClient client, WarehouseID ID, ushort maxCount = 60)
        {
            Owner = client;
            _ID = ID;
            MaxCount = maxCount;
            items = new Dictionary<uint, ConquerItem>(MaxCount);
            objects = new ConquerItem[0];
        }
        public bool Add(ConquerItem item)
        {
            if (!items.ContainsKey(item.UID) && Count < MaxCount)
            {
                item.Warehouse = (uint)_ID;
                item.Position = 0;
                Owner.Inventory.Remove(item, Game.Enums.ItemUse.Move);
                items.Add(item.UID, item);
                objects = items.Values.ToArray();
                return true;
            }
            return false;
        }

        public bool Remove(ConquerItem item)
        {
            if (items.ContainsKey(item.UID))
            {
                item.Warehouse = 0;
                if (Owner.Inventory.Add(item, Enums.ItemUse.Move))
                {
                    items.Remove(item.UID);
                    objects = items.Values.ToArray();
                    Network.GamePackets.Warehouse warehouse = new Nyx.Server.Network.GamePackets.Warehouse(true);
                    warehouse.Type = Network.GamePackets.Warehouse.RemoveItem;
                    warehouse.Count = 1;
                    warehouse.Append(item);
                    Owner.Send(warehouse);
                    return true;
                }
            }
            return false;
        }

        public bool Remove(uint UID)
        {
            if (items.ContainsKey(UID))
            {
                ConquerItem item = items[UID];
                item.Warehouse = 0;
                if (Owner.Inventory.Add(item, Enums.ItemUse.Move))
                {
                    items.Remove(item.UID);
                    objects = items.Values.ToArray();
                    return true;
                }
            }
            return false;
        }

        public ConquerItem[] Objects
        { 
            get 
            {
                return objects; 
            }
        }

        public ushort Count { get { return (ushort)Objects.Length; } }

        public ConquerItem GetItem(uint UID)
        {
            ConquerItem item = null;
            items.TryGetValue(UID, out item);
            return item;
        }

        public bool ContainsUID(uint UID)
        {
            return items.ContainsKey(UID);
        }

        public enum WarehouseID : uint
        {
            TwinCity = 8,
            PhoenixCity = 10012,
            ApeCity = 10028,
            DesertCity = 10011,
            BirdCity = 10027,
            StoneCity = 4101,
            Market = 44,
            Poker = 10010,
        }
    }
}
