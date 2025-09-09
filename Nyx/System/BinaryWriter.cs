using Nyx.Server.Network.GamePackets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Nyx.Server.Network
{
    public unsafe abstract class Structure
    {
        private MemoryStream buffer;
        #region Native Function Calls
        public const string MSVCRT = "msvcrt.dll";
        [DllImport(MSVCRT, CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        private static extern void* memcpy(void* dst, string src, int length);
        [DllImport(MSVCRT, CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        private static extern void* memcpy(void* dst, byte[] src, int length);
        [DllImport(MSVCRT, CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        private static extern void* memcpy(byte[] dst, void* src, int length);
        [DllImport(MSVCRT, CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        public static extern void* memset(void* dst, byte fill, int length);
        #endregion
        public Structure(byte[] packet)
        {
            buffer = new MemoryStream(packet);
        }
        public Structure(int length)
        {
            buffer = new MemoryStream(new byte[length]);
        }
        #region Writing Method
        public void WriteUint(uint value, int offest)
        {
            using (var Writer = new BinaryWriter(buffer))
            {
                Writer.BaseStream.Position = offest;
                Writer.Write(value);
            }
        }
        public void WriteInt(int value, int offest)
        {
            using (var Writer = new BinaryWriter(buffer))
            {
                Writer.BaseStream.Position = offest;
                Writer.Write(value);
            }
        }
        public void WriteString(string value, int offest)
        {
            using (var Writer = new BinaryWriter(buffer))
            {
                Writer.BaseStream.Position = offest - 1;
                Writer.Write(value.Length);
                Writer.BaseStream.Position = offest;
                Writer.Write(value);
            }
        }
        public void WriteChar(char value, int offest)
        {
            using (var Writer = new BinaryWriter(buffer))
            {
                Writer.BaseStream.Position = offest;
                Writer.Write(value);
            }
        }
        public void WriteByte(byte value, int offest)
        {
            using (var Writer = new BinaryWriter(buffer))
            {
                Writer.BaseStream.Position = offest;
                Writer.Write(value);
            }
        }
        public void WriteSByte(sbyte value, int offest)
        {
            using (var Writer = new BinaryWriter(buffer))
            {
                Writer.BaseStream.Position = offest;
                Writer.Write(value);
            }
        }
        public void WriteBoolean(bool value, int offest)
        {
            using (var Writer = new BinaryWriter(buffer))
            {
                Writer.BaseStream.Position = offest;
                Writer.Write((byte)(value ? 1 : 0));
            }
        }
        public void WriteFloat(float value, int offest)
        {
            using (var Writer = new BinaryWriter(buffer))
            {
                Writer.BaseStream.Position = offest;
                Writer.Write(value);
            }
        }
        public void Writelong(long value, int offest)
        {
            using (var Writer = new BinaryWriter(buffer))
            {
                Writer.BaseStream.Position = offest;
                Writer.Write(value);
            }
        }
        public void WriteUlong(ulong value, int offest)
        {
            using (var Writer = new BinaryWriter(buffer))
            {
                Writer.BaseStream.Position = offest;
                Writer.Write(value);
            }
        }
        public void Writeshort(short value, int offest)
        {
            using (var Writer = new BinaryWriter(buffer))
            {
                Writer.BaseStream.Position = offest;
                Writer.Write(value);
            }
        }
        public void WriteUshort(ushort value, int offest)
        {
            using (var Writer = new BinaryWriter(buffer))
            {
                Writer.BaseStream.Position = offest;
                Writer.Write(value);
            }
        }
        public void WriteDecimal(decimal value, int offest)
        {
            using (var Writer = new BinaryWriter(buffer))
            {
                Writer.BaseStream.Position = offest;
                Writer.Write(value);
            }
        }
        #endregion
        #region Reading Method
        public int ReadInt(int offest)
        {
            using (var Reader = new BinaryReader(buffer))
            {
                Reader.BaseStream.Position = offest;
                return Reader.ReadInt32();
            }
        }
        public uint ReadUInt(int offest)
        {
            using (var Reader = new BinaryReader(buffer))
            {
                Reader.BaseStream.Position = offest;
                return Reader.ReadUInt32();
            }
        }
        public ushort ReadUshort(int offest)
        {
            using (var Reader = new BinaryReader(buffer))
            {
                Reader.BaseStream.Position = offest;
                return Reader.ReadUInt16();
            }
        }
        public short ReadShort(int offest)
        {
            using (var Reader = new BinaryReader(buffer))
            {
                Reader.BaseStream.Position = offest;
                return Reader.ReadInt16();
            }
        }
        public long ReadLong(int offest)
        {
            using (var Reader = new BinaryReader(buffer))
            {
                Reader.BaseStream.Position = offest;
                return Reader.ReadInt64();
            }
        }
        public ulong ReadULong(int offest)
        {
            using (var Reader = new BinaryReader(buffer))
            {
                Reader.BaseStream.Position = offest;
                return Reader.ReadUInt64();
            }
        }
        public byte ReadByte(int offest)
        {
            using (var Reader = new BinaryReader(buffer))
            {
                Reader.BaseStream.Position = offest;
                return Reader.ReadByte();
            }
        }
        public sbyte ReadSByte(int offest)
        {
            using (var Reader = new BinaryReader(buffer))
            {
                Reader.BaseStream.Position = offest;
                return Reader.ReadSByte();
            }
        }
        public bool ReadBoolean(int offest)
        {
            using (var Reader = new BinaryReader(buffer))
            {
                Reader.BaseStream.Position = offest;
                return Reader.ReadBoolean();
            }
        }
        public float ReadFloat(int offest)
        {
            using (var Reader = new BinaryReader(buffer))
            {
                Reader.BaseStream.Position = offest;
                return Reader.ReadSingle();
            }
        }
        public string ReadString(int offest)
        {
            using (var Reader = new BinaryReader(buffer))
            {
                Reader.BaseStream.Position = offest;
                return Reader.ReadString();
            }
        }
        public char ReadChar(int offest)
        {
            using (var Reader = new BinaryReader(buffer))
            {
                Reader.BaseStream.Position = offest;
                return Reader.ReadChar();
            }
        }
        #endregion
        #region Build Method
        protected virtual byte[] Build()
        {
            return buffer.ToArray();
        }
        public static implicit operator byte[](Structure structure)
        {
            return structure.Build();
        }
        public void AppendArray()
        {
            PacketHandler.PrintPacket(buffer.ToArray());
        }
        #endregion
    }
}
