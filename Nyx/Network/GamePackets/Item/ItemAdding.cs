using System;

namespace Nyx.Server.Network.GamePackets
{
    public class ItemAdding : Writer, Interfaces.IPacket
    {
        public struct Purification_
        {
            public bool Available;
            public uint ItemUID;
            public uint PurificationItemID;
            public uint PurificationLevel;
            /// <summary>
            /// In minutes.
            /// </summary>
            public uint PurificationDuration;
            public DateTime AddedOn;
        }
        public struct Refinery_
        {
            public bool Available;
            public uint ItemUID;
            public uint EffectID;
            public uint EffectLevel;
            public uint EffectPercent;
            /// <summary>
            /// In minutes.
            /// </summary>
            public uint EffectDuration;
            public DateTime AddedOn;
        }

        public const uint ExtraEffect = 2, PurificationAdding = 6;
        public const uint PermanentRefinery = 3, StabilizationEffectRefined = 4, PurificationEffect = 5, ExpireTime = 7, StabilizationEffect = 9, Warehause = 10;


        byte[] Buffer;
        const byte minBufferSize = 8;
        public ItemAdding(bool Create)
        {
            if (Create)
            {
                Buffer = new byte[minBufferSize + 8];
                Writer.Write(minBufferSize, 0, Buffer);
                Writer.Write(2077, 2, Buffer);
            }
        }

        public uint Type
        {
            get { return BitConverter.ToUInt32(Buffer, 12); }
            set { Write(value, 12, Buffer); }
        }

        public uint UpdateCount
        {
            get { return BitConverter.ToUInt32(Buffer, 4); }
            set
            {
                byte[] buffer = new byte[minBufferSize + 8 + 32 * value];
                int count = buffer.Length;
                if (count > Buffer.Length)
                    count = Buffer.Length;
                System.Buffer.BlockCopy(Buffer, 0, buffer, 0, count);
                Writer.Write((ushort)(minBufferSize + 32 * value), 0, buffer);
                Buffer = buffer;
                Write(value, 4, Buffer);
            }
        }
        public bool Append(Purification_ purify)
        {
            UpdateCount = UpdateCount + 1;
            ushort offset = (ushort)(8 + (UpdateCount - 1) * 32);
            Write(purify.ItemUID, offset, Buffer);
            Write(PurificationAdding, offset + 4, Buffer);
            Write(purify.PurificationItemID, offset + 8, Buffer);
            Write(purify.PurificationLevel, offset + 12, Buffer);
            if (purify.PurificationDuration != 0)
            {
                TimeSpan span1 = new TimeSpan(purify.AddedOn.AddSeconds(purify.PurificationDuration).Ticks);
                TimeSpan span2 = new TimeSpan(DateTime.Now.Ticks);
                int secondsleft = (int)(span1.TotalSeconds - span2.TotalSeconds);
                if (secondsleft <= 0)
                {
                    purify.Available = false;
                    UpdateCount = UpdateCount - 1;
                    return false;
                }
                Write((uint)secondsleft, offset + 24, Buffer);
            }
            return true;
        }

        public bool Append(Refinery_ effect)
        {
            UpdateCount = UpdateCount + 1;
            ushort offset = (ushort)(8 + (UpdateCount - 1) * 32);
            Write(effect.ItemUID, offset, Buffer);
            if (effect.EffectDuration == 0)
                Write(PermanentRefinery, offset + 4, Buffer);
            else
                Write(ExtraEffect, offset + 4, Buffer);
            Write(effect.EffectID, offset + 8, Buffer);
            Write(effect.EffectLevel, offset + 12, Buffer);
            Write(effect.EffectPercent, offset + 16, Buffer);
            if (effect.EffectPercent != 0)
            {
                TimeSpan span1 = new TimeSpan(effect.AddedOn.AddSeconds(effect.EffectDuration).Ticks);
                TimeSpan span2 = new TimeSpan(DateTime.Now.Ticks);
                int secondsleft = (int)(span1.TotalSeconds - span2.TotalSeconds);
                if (secondsleft <= 0 && effect.EffectDuration != 0)
                {
                    effect.Available = false;
                    UpdateCount -= 1;
                    return false;
                }
                Write((uint)secondsleft, offset + 24, Buffer);
            }
            return true;
        }

        public bool Append2(Purification_ purify)
        {
            this.UpdateCount++;
            ushort offset = (ushort)(8 + ((this.UpdateCount - 1) * 32));
            Writer.Write(purify.ItemUID, offset, this.Buffer);
            Writer.Write(Type, offset + 4, this.Buffer);
            Writer.Write(purify.PurificationItemID, offset + 8, this.Buffer);
            Writer.Write(purify.PurificationLevel, offset + 12, this.Buffer);
            if (purify.PurificationDuration != 0)
            {
                TimeSpan span = new TimeSpan(purify.AddedOn.AddSeconds((double)purify.PurificationDuration).Ticks);
                TimeSpan span2 = new TimeSpan(DateTime.Now.Ticks);
                int num2 = (int)(span.TotalSeconds - span2.TotalSeconds);
                if (num2 <= 0)
                {
                    purify.Available = false;
                    this.UpdateCount--;
                    return false;
                }
                Writer.Write((uint)num2, offset + 24, this.Buffer);
            }
            return true;
        }
        public bool Append2(Refinery_ effect)
        {
            this.UpdateCount++;
            ushort offset = (ushort)(8 + ((this.UpdateCount - 1) * 32));
            Writer.Write(effect.ItemUID, offset, this.Buffer);
            Writer.Write(Type, offset + 4, this.Buffer);
            Writer.Write(effect.EffectID, offset + 8, this.Buffer);
            Writer.Write(effect.EffectLevel, offset + 12, this.Buffer);
            Writer.Write(effect.EffectPercent, offset + 0x10, this.Buffer);
            if (effect.EffectPercent != 0)
            {
                TimeSpan span = new TimeSpan(effect.AddedOn.AddSeconds((double)effect.EffectDuration).Ticks);
                TimeSpan span2 = new TimeSpan(DateTime.Now.Ticks);
                int num2 = (int)(span.TotalSeconds - span2.TotalSeconds);
                if (num2 <= 0 && effect.EffectDuration != 0)
                {
                    effect.Available = false;
                    this.UpdateCount--;
                    return false;
                }
                Writer.Write((uint)num2, offset + 24, this.Buffer);
            }
            return true;
        }

        public byte[] Encode()
        {
            return Buffer;
        }

        public void Deserialize(byte[] buffer)
        {
            Buffer = buffer;
        }

        public void Send(Client.GameClient client)
        {
            client.Send(Buffer);
        }
    }
}