using Nyx.Server.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Nyx.Server.Network.GamePackets
{
    public static class InnerPowerStage
    {
        public enum ActionID : ushort
        {
            UpdateStage = 1,
            UpdateScore = 2
        }
    }
    public class MsgInnerPower
    {
        public enum ActionID : byte
        {
            InfoStage = 0,
            UpdateGong = 3,
            UnlockStage = 4,
            OpenInner = 5,
            TransferInnerPowers = 6,
        }
        public unsafe static void InnerPowerHandler(Client.GameClient client, byte[] stream)
        {
            ActionID Action;
            uint dwparam;
            uint dwparam2;

            stream.GetInnerPower(out Action, out dwparam, out dwparam2);

            switch (Action)
            {
                case ActionID.TransferInnerPowers:
                    {

                        break;
                    }
                case ActionID.OpenInner:
                    {
                        Client.GameClient user;
                        if (Kernel.GamePool.TryGetValue(dwparam, out user))
                        {
                            if (user == null)
                                return;
                            if (user.Entity == null)
                                return;
                            if (user.Entity.InnerPower == null)
                                return;
                            client.Send(stream.InnerPowerGui(user.Entity.InnerPower.GetNeiGongs()));
                            var stages = user.Entity.InnerPower.Stages.Where(p => p.Complete).ToArray();
                            if (stages.Length > 0)
                                client.Send(stream.InnerPowerStageInfo(InnerPowerStage.ActionID.UpdateStage, user.Entity.UID, stages.Last()));
                        }
                        break;
                    }
                case ActionID.InfoStage:
                    {

                        Client.GameClient user;
                        if (Kernel.GamePool.TryGetValue(dwparam, out user))
                        {
                            client.Send(stream.InnerPowerStageInfo(InnerPowerStage.ActionID.UpdateStage, user.Entity.UID, user.Entity.InnerPower.Stages[dwparam2 - 1]));
                            client.Send(stream.InnerPowerStageInfo(InnerPowerStage.ActionID.UpdateScore, user.Entity.UID, user.Entity.InnerPower.Stages[dwparam2 - 1]));
                        }
                        break;
                    }
                case ActionID.UnlockStage:
                    {
                        Database.InnerPowerTable.Stage DBStage = null;
                        Database.InnerPowerTable.Stage.NeiGong DBGong = null;
                        if (Database.InnerPowerTable.GetDBInfo(dwparam, out DBStage, out DBGong))
                        {
                            if (DBGong.CheckAccount(client.Entity.Reborn, client.Entity.Level) && client.Inventory.Contains(DBGong.ItemID, 1)
                                && client.Entity.InnerPower.isUnlockedNeiGong((byte)dwparam))
                            {
                                InnerPower.Stage stage = null;
                                InnerPower.Stage.NeiGong gong = null;
                                if (client.Entity.InnerPower.TryGetStageAndGong((byte)dwparam, out stage, out gong))
                                {
                                    stage.UnLocked = gong.Unlocked = true;

                                    stream.InnerPowerGui(client.Entity.InnerPower.GetNeiGongs());
                                    client.Send(stream);
                                    client.Send(stream.InnerPowerStageInfo(InnerPowerStage.ActionID.UpdateStage, client.Entity.UID, stage));
                                    client.Send(stream.InnerPowerStageInfo(InnerPowerStage.ActionID.UpdateScore, client.Entity.UID, stage));

                                    client.Inventory.Remove(DBGong.ItemID, 1);
                                    client.Send(new Network.GamePackets.Message("You successfully aquired " + Database.ConquerItemInformation.BaseInformations[DBGong.ItemID].Name + " (B) !", System.Drawing.Color.Red, Network.GamePackets.Message.System));
                                    foreach (var player in Kernel.GamePool.Values)
                                    {
                                        player.Send(new Network.GamePackets.Message(client.Entity.Name + " successfully acquired [" + Database.ConquerItemInformation.BaseInformations[DBGong.ItemID].Name + "]  getting closer to be a super hero! [Link Go acquire ###1 692]", System.Drawing.Color.Bisque, Network.GamePackets.Message.InnerPower));
                                    }
                                }
                            }
                        }
                        break;
                    }
                case ActionID.UpdateGong:
                    {
                        Database.InnerPowerTable.Stage DBStage = null;
                        Database.InnerPowerTable.Stage.NeiGong DBGong = null;
                        if (Database.InnerPowerTable.GetDBInfo(dwparam, out DBStage, out DBGong))
                        {
                            InnerPower.Stage stage = null;
                            InnerPower.Stage.NeiGong gong = null;
                            if (client.Entity.InnerPower.TryGetStageAndGong((byte)dwparam, out stage, out gong))
                            {
                                if (stage.UnLocked && gong.Unlocked && gong.level < DBGong.MaxLevel)
                                {
                                    int potency_cost = (int)DBGong.ProgressNeiGongValue[Math.Min(gong.level, (int)(DBGong.ProgressNeiGongValue.Length - 1))];
                                    if (client.Entity.InnerPower.Potency >= potency_cost)
                                    {
                                        client.Entity.InnerPower.AddPotency(stream, client, -potency_cost);

                                        gong.level += 1;
                                        gong.Score = (byte)Math.Ceiling(((float)((float)100 / (float)DBGong.MaxLevel) * (float)gong.level));
                                        gong.Complete = gong.level == DBGong.MaxLevel;

                                        client.Send(stream.InnerPowerGui(client.Entity.InnerPower.GetNeiGongs()));
                                        client.Send(stream.InnerPowerStageInfo(InnerPowerStage.ActionID.UpdateStage, client.Entity.UID, stage));
                                        client.Send(stream.InnerPowerStageInfo(InnerPowerStage.ActionID.UpdateScore, client.Entity.UID, stage));
                                        client.Entity.InnerPower.UpdateStatus();
                                        client.LoadItemStats();
                                        InnerPower.InnerPowerRank.UpdateRank(client.Entity.InnerPower);
                                    }
                                }
                            }
                        }
                        break;
                    }
            }
        }
    }
    public static class InnerMsg
    {
        public static unsafe void GetInnerPower(this byte[] packet, out MsgInnerPower.ActionID mode, out uint dwparam, out uint dwparam2)
        {
            mode = (MsgInnerPower.ActionID)packet[4];
            dwparam = BitConverter.ToUInt32(packet, 5);
            dwparam2 = BitConverter.ToUInt32(packet, 9);
        }

        public static unsafe byte[] InnerPowerGui(this byte[] packet, InnerPower.Stage.NeiGong[] gongs)
        {
            packet = new byte[8 + 6 + (ushort)gongs.Length * 5];
            Writer.Write((ushort)(packet.Length - 8), 0, packet);
            Writer.Write(2612, 2, packet);
            if (gongs != null)
            {
                Writer.Write((ushort)gongs.Length, 4, packet);
                int offset = 6;
                for (int x = 0; x < gongs.Length; x++)
                {
                    var element = gongs[x];
                    Writer.Write((byte)element.ID, offset, packet);
                    offset += 1;
                    Writer.Write((uint)element.Score, offset, packet);
                    offset += 4;
                }
            }
            return packet;
        }

        public static unsafe byte[] InnerPowerStageInfo(this byte[] packet, InnerPowerStage.ActionID action, uint UID, InnerPower.Stage stage)
        {
            var array_gongs = stage.NeiGongs.Where(p => p.Unlocked).ToArray();
            var DBStage = Database.InnerPowerTable.Stages[stage.ID - 1];

            MemoryStream strm = new MemoryStream();
            BinaryWriter stream = new BinaryWriter(strm);
            stream.Write((ushort)0);
            stream.Write((ushort)2611);
            stream.Write(UID);
            stream.Write((uint)stage.Score);
            stream.Write(0);
            stream.Write((ushort)action);
            stream.Write((ushort)array_gongs.Length);
            stream.Write(stage.GetNoumberAtributes(array_gongs));
            for (int x = 0; x < array_gongs.Length; x++)
            {
                var element = array_gongs[x];
                stream.Write((ushort)element.ID);
                stream.Write(element.level);
                stream.Write(element.Score);
                stream.Write((byte)(element.Complete ? 1 : 0));
            }

            for (int x = 0; x < array_gongs.Length; x++)
            {
                var element = array_gongs[x];
                for (int y = 0; y < DBStage.NeiGongAtributes[x].AtributesType.Length; y++)
                {
                    stream.Write((ushort)element.ID);
                    var atribut = DBStage.NeiGongAtributes[x];
                    stream.Write((byte)atribut.AtributesType[y]);
                    stream.Write((uint)((atribut.AtributesValues[y] / atribut.MaxLevel) * element.level));
                }
            }
            int packetlength = (int)strm.Length;
            strm.Position = 0;
            stream.Write((ushort)packetlength);
            strm.Position = strm.Length;
            stream.Write(Encoding.Default.GetBytes("TQServer"));
            strm.Position = 0;
            byte[] buf = new byte[strm.Length];
            strm.Read(buf, 0, buf.Length);
            stream.Close();
            strm.Close();
            return buf;
        }
    }
    public class InnerPower
    {

        public static class InnerPowerRank
        {
            public const int MaxPlayers = 100;
            private static System.Collections.Concurrent.ConcurrentDictionary<uint, InnerPower> InnerPowerRanks = new System.Collections.Concurrent.ConcurrentDictionary<uint, InnerPower>();
            private static InnerPower[] Rankings;
            private static object synroot = new object();

            public static void UpdateRank(InnerPower inner)
            {
                if (InnerPowerRanks.Count < MaxPlayers)
                {
                    InnerPowerRanks.TryAdd(inner.UID, inner);
                    CreateRanks();
                }
                else
                {
                    var lastPlayer = Rankings.Last();
                    if (lastPlayer.TotalScore < inner.TotalScore)
                    {
                        InnerPowerRanks.TryAdd(inner.UID, inner);
                        CreateRanks();
                    }
                    else if (InnerPowerRanks.ContainsKey(inner.UID))
                        CreateRanks();
                }
            }
            public static void CreateRanks()
            {
                lock (synroot)
                {
                    var array = InnerPowerRanks.Values.ToArray();
                    Rankings = array.OrderByDescending(p => p.TotalScore).ToArray();
                    SetRank(Rankings, (a, b) => { a.Rank = b; });

                }
            }
            public static InnerPower[] GetRankingList()
            {
                InnerPower[] array = null;
                lock (synroot)
                    array = Rankings.ToArray();

                SetRank(array, (a, b) => { a.Rank = b; });

                return array;
            }

            public static void SetRank(InnerPower[] array, Action<InnerPower, int> modify)
            {
                for (int i = 0; i < array.Length; i++)
                    modify(array[i], i + 1);
            }
        }

        public static System.Collections.Concurrent.ConcurrentDictionary<uint, InnerPower> InnerPowerPolle = new System.Collections.Concurrent.ConcurrentDictionary<uint, InnerPower>();

        public int Potency = 0;
        public uint TotalScore
        {
            get
            {
                uint points = 0;
                foreach (var stg in Stages)
                {
                    points += stg.Score;
                }
                return points;
            }
        }
        public uint UID;
        public string Name = "";

        public override string ToString()
        {
            StringBuilder build = new StringBuilder();
            build.Append(Name + "/" + UID + "/" + Potency + "/");
            var array = Stages.Where(p => p.UnLocked).ToArray();
            build.Append(array.Length + "/");
            foreach (var stage in array)
            {
                build.Append(stage.ID + "/" + (byte)(stage.UnLocked ? 1 : 0) + "/" + stage.NeiGongs.Length + "/");
                foreach (var neigong in stage.NeiGongs)
                    build.Append(neigong.ID + "/" + neigong.Score + "/" + (byte)(neigong.Unlocked ? 1 : 0) + "/" + neigong.level + "/" + (byte)(neigong.Complete ? 1 : 0) + "/");
            }
            return build.ToString();
        }

        public uint CriticalStrike { get; private set; }
        public uint SkillCriticalStrike { get; private set; }
        public uint Immunity { get; private set; }
        public uint Breakthrough { get; private set; }
        public uint Counteraction { get; private set; }
        public uint MaxLife { get; private set; }
        public uint AddAttack { get; private set; }
        public uint AddMagicAttack { get; private set; }
        public uint AddMagicDefense { get; private set; }
        public uint FinalAttack { get; private set; }
        public uint FinalMagicAttack { get; private set; }
        public uint FinalDefense { get; private set; }
        public uint FinalMagicDefense { get; private set; }
        public uint Defence { get; private set; }

        public Stage[] Stages = new Stage[Database.InnerPowerTable.Count];

        public class Stage
        {
            public ushort ID;
            public bool UnLocked = false;
            public bool Complete
            {
                get
                {
                    return NeiGongs.Length == NeiGongs.Where(p => p.Complete).Count();
                }
            }
            public uint Score { get { return (uint)NeiGongs.Sum(p => p.Score); } }
            public NeiGong[] NeiGongs;

            public ushort GetNoumberAtributes(NeiGong[] gongs)
            {
                var DBStage = Database.InnerPowerTable.Stages[ID - 1];
                ushort value = 0;
                for (int x = 0; x < gongs.Length; x++)
                    value += (ushort)DBStage.NeiGongAtributes[x].AtributesValues.Length;

                return value;
            }
            public ushort GetAtributesCount(ushort gong_noumber)
            {
                return (ushort)Database.InnerPowerTable.Stages[ID - 1].NeiGongAtributes[gong_noumber].AtributesValues.Length;
            }

            public class NeiGong
            {
                public byte ID;
                public byte Score;
                public bool Unlocked = false;
                public byte level;
                public bool Complete = false;
            }
        }

        public InnerPower(string _Name, uint _uid)
        {
            Name = _Name;
            UID = _uid;

            for (int x = 0; x < Database.InnerPowerTable.Count; x++)
            {
                var DBStage = Database.InnerPowerTable.Stages[x];
                Stage stage = new Stage();
                stage.ID = DBStage.ID;


                stage.NeiGongs = new Stage.NeiGong[DBStage.NeiGongNum];
                for (int y = 0; y < DBStage.NeiGongNum; y++)
                {
                    stage.NeiGongs[y] = new Stage.NeiGong();
                    stage.NeiGongs[y].ID = DBStage.NeiGongAtributes[y].ID;
                }
                Stages[x] = stage;
            }
            InnerPowerPolle.TryAdd(_uid, this);
        }


        public void UpdateStatus()
        {

            Defence = 0;
            CriticalStrike = 0;
            SkillCriticalStrike = 0;
            Immunity = 0;
            Breakthrough = 0;
            Counteraction = 0;
            MaxLife = 0;
            AddAttack = 0;
            AddMagicAttack = 0;
            AddMagicDefense = 0;
            FinalAttack = 0;
            FinalDefense = 0;
            FinalMagicAttack = 0;
            FinalMagicDefense = 0;

            foreach (var stage in Stages)
            {
                if (!stage.UnLocked)
                    break;
                var DBStage = Database.InnerPowerTable.Stages[stage.ID - 1];
                for (int x = 0; x < stage.NeiGongs.Length; x++)
                {
                    var neigong = stage.NeiGongs[x];
                    if (!neigong.Unlocked || neigong.level == 0)
                        break;
                    var DBNeiGong = DBStage.NeiGongAtributes[x];
                    for (int i = 0; i < DBNeiGong.AtributesType.Length; i++)
                    {
                        var AtributType = DBNeiGong.AtributesType[i];
                        var AtributValue = DBNeiGong.AtributesValues[i];

                        AddAtributes(AtributType, (uint)((AtributValue / DBNeiGong.MaxLevel) * neigong.level));
                    }
                }
                if (stage.Complete)
                {
                    for (int y = 0; y < DBStage.SpecialAtributesType.Length; y++)
                    {
                        var SpecialAtributType = DBStage.SpecialAtributesType[y];
                        var SpecialAtributValue = DBStage.AtributesValues[y];
                        AddAtributes(SpecialAtributType, SpecialAtributValue);
                    }
                }
            }
        }
        public void AddAtributes(Database.InnerPowerTable.AtributeType AtributType, uint AtributValue)
        {
            switch (AtributType)
            {
                case Database.InnerPowerTable.AtributeType.Break:
                    Breakthrough += (uint)(AtributValue / 10);
                    break;
                case Database.InnerPowerTable.AtributeType.FinalMAttack:
                    FinalMagicAttack += AtributValue;
                    break;
                case Database.InnerPowerTable.AtributeType.FinalPAttack:
                    FinalAttack += AtributValue;
                    break;
                case Database.InnerPowerTable.AtributeType.FinalMDamage:
                    FinalMagicDefense += AtributValue;
                    break;
                case Database.InnerPowerTable.AtributeType.FinalPDamage:
                    FinalDefense += AtributValue;
                    break;
                case Database.InnerPowerTable.AtributeType.Immunity:
                    Immunity += AtributValue;
                    break;
                case Database.InnerPowerTable.AtributeType.MAttack:
                    AddMagicAttack += AtributValue;
                    break;
                case Database.InnerPowerTable.AtributeType.MaxHP:
                    MaxLife += AtributValue;
                    break;
                case Database.InnerPowerTable.AtributeType.MDefense:
                    AddMagicDefense += AtributValue;
                    break;
                case Database.InnerPowerTable.AtributeType.MStrike:
                    SkillCriticalStrike += AtributValue;
                    break;
                case Database.InnerPowerTable.AtributeType.PAttack:
                    AddAttack += AtributValue;
                    break;
                case Database.InnerPowerTable.AtributeType.PDefense:
                    Defence += AtributValue;
                    break;
                case Database.InnerPowerTable.AtributeType.PStrike:
                    CriticalStrike += AtributValue;
                    break;
                case Database.InnerPowerTable.AtributeType.Conteraction:
                    Counteraction += AtributValue / 10;
                    break;

            }
        }

        public bool isUnlockedNeiGong(byte stage)
        {
            if (stage > 1)
            {
                Stage mstage = null;
                Stage.NeiGong mgong = null;
                if (TryGetStageAndGong((byte)(stage - 1), out mstage, out mgong))
                    return mgong.Unlocked == true;
            }
            return stage == 1;
        }


        public void AddPotency(byte[] stream, Client.GameClient user, int _Potency)
        {
            this.Potency += _Potency;
            user.Entity.Update((byte)Update.InnerPowerPotency, (uint)Potency, false);
        }
        public bool TryGetStageAndGong(byte ID, out Stage _stage, out Stage.NeiGong _gong)
        {
            foreach (var m_stage in Stages)
            {
                foreach (var m_gong in m_stage.NeiGongs)
                {
                    if (ID == m_gong.ID)
                    {
                        _stage = m_stage;
                        _gong = m_gong;
                        return true;
                    }
                }
            }
            _stage = null;
            _gong = null;
            return false;
        }
        public Stage.NeiGong[] GetNeiGongs()
        {
            List<Stage.NeiGong> arraygongs = new List<Stage.NeiGong>();
            foreach (var stage in Stages)
            {
                if (stage.UnLocked)
                {
                    foreach (var neigong in stage.NeiGongs)
                        if (neigong.Unlocked)
                            arraygongs.Add(neigong);
                }
            }
            return arraygongs.ToArray();
        }
        public int Rank { get; set; }
        public void Deserialize(BinaryReader reader)
        {
            try
            {
                Potency = reader.ReadInt32();
                int xStages = reader.ReadInt32();
                for (int i = 0; i < xStages; i++)
                {
                    var Stage = Stages[i];
                    Stage.ID = reader.ReadUInt16();
                    Stage.UnLocked = reader.ReadByte() == 1;
                    int count_neigongs = reader.ReadInt32();
                    for (int y = 0; y < count_neigongs; y++)
                    {
                        var neigon = Stage.NeiGongs[y];
                        neigon.ID = reader.ReadByte();
                        neigon.Score = reader.ReadByte();
                        neigon.Unlocked = reader.ReadByte() == 1;
                        neigon.level = reader.ReadByte();
                        neigon.Complete = reader.ReadByte() == 1;
                    }
                }
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); }
        }
        public void Serialize(BinaryWriter writer)
        {
            try
            {
                writer.Write(Potency);
                var array = Stages.Where(p => p.UnLocked).  ToArray();
                writer.Write(array.Length);
                foreach (var stage in array)
                {
                    writer.Write(stage.ID);
                    writer.Write((byte)(stage.UnLocked ? 1 : 0));
                    writer.Write(stage.NeiGongs.Length);
                    foreach (var neigong in stage.NeiGongs)
                    {
                        writer.Write(neigong.ID);
                        writer.Write(neigong.Score);
                        writer.Write((byte)(neigong.Unlocked ? 1 : 0));
                        writer.Write(neigong.level);
                        writer.Write((byte)(neigong.Complete ? 1 : 0));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}