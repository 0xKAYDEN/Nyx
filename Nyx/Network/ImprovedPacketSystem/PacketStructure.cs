using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Nyx.Server.Network.ImprovedPacketSystem
{
    /// <summary>
    /// Represents the structure of a packet with improved parsing and serialization
    /// </summary>
    public readonly struct PacketStructure
    {
        public readonly ushort Length;
        public readonly ushort ID;
        public readonly ushort Type;
        public readonly ushort Offset;
        public readonly ReadOnlyMemory<byte> Data;
        public readonly ulong Seal;

        public PacketStructure(ushort length, ushort id, ushort type, ushort offset, ReadOnlyMemory<byte> data, ulong seal)
        {
            Length = length;
            ID = id;
            Type = type;
            Offset = offset;
            Data = data;
            Seal = seal;
        }

        /// <summary>
        /// Parse a packet from raw bytes
        /// </summary>
        public static PacketStructure Parse(byte[] buffer)
        {
            if (buffer == null || buffer.Length < 8)
                throw new ArgumentException("Invalid packet buffer");

            var length = BitConverter.ToUInt16(buffer, 0);
            var id = BitConverter.ToUInt16(buffer, 2);
            var type = BitConverter.ToUInt16(buffer, 4);
            var offset = BitConverter.ToUInt16(buffer, 6);

            // Extract data portion (everything after header)
            var dataLength = buffer.Length - 8;
            var data = dataLength > 0 ? new ReadOnlyMemory<byte>(buffer, 8, dataLength) : ReadOnlyMemory<byte>.Empty;

            // Extract seal (last 8 bytes)
            ulong seal = 0;
            if (buffer.Length >= 8)
            {
                var sealStart = Math.Max(0, buffer.Length - 8);
                seal = BitConverter.ToUInt64(buffer, sealStart);
            }

            return new PacketStructure(length, id, type, offset, data, seal);
        }

        /// <summary>
        /// Create a packet from components
        /// </summary>
        public static byte[] Create(ushort id, ushort type, ushort offset, byte[] data)
        {
            var totalLength = 8 + (data?.Length ?? 0) + 8; // Header + Data + Seal
            var buffer = new byte[totalLength];

            // Write header
            BitConverter.GetBytes((ushort)(totalLength - 8)).CopyTo(buffer, 0);
            BitConverter.GetBytes(id).CopyTo(buffer, 2);
            BitConverter.GetBytes(type).CopyTo(buffer, 4);
            BitConverter.GetBytes(offset).CopyTo(buffer, 6);

            // Write data
            if (data != null && data.Length > 0)
            {
                data.CopyTo(buffer, 8);
            }

            // Write seal
            var seal = BitConverter.ToUInt64(Encoding.Default.GetBytes("TQClient"), 0);
            BitConverter.GetBytes(seal).CopyTo(buffer, totalLength - 8);

            return buffer;
        }

        /// <summary>
        /// Validate packet structure
        /// </summary>
        public bool IsValid(ulong expectedSeal)
        {
            return Seal == expectedSeal && Length > 0 && ID > 0;
        }

        /// <summary>
        /// Get packet info for logging
        /// </summary>
        public string GetPacketInfo()
        {
            return $"Packet[ID:{ID}, Length:{Length}, Type:{Type}, Offset:{Offset}, DataSize:{Data.Length}]";
        }
    }

    /// <summary>
    /// Improved packet reader with better error handling and performance
    /// </summary>
    public ref struct PacketReader
    {
        private readonly ReadOnlySpan<byte> _data;
        private int _position;

        public PacketReader(ReadOnlySpan<byte> data)
        {
            _data = data;
            _position = 0;
        }

        public bool HasMoreData => _position < _data.Length;

        /// <summary>
        /// Read a 7-bit encoded integer
        /// </summary>
        public uint Read7BitEncodedInt()
        {
            uint result = 0;
            int shift = 0;

            while (HasMoreData)
            {
                byte b = _data[_position++];
                result |= (uint)(b & 0x7F) << shift;
                
                if ((b & 0x80) == 0)
                    break;
                    
                shift += 7;
            }

            return result;
        }

        /// <summary>
        /// Read a string with specified length
        /// </summary>
        public string ReadString(int length)
        {
            if (_position + length > _data.Length)
                throw new ArgumentException("Not enough data to read string");

            var result = Encoding.Default.GetString(_data.Slice(_position, length));
            _position += length;
            
            return result.Replace("\0", "").Replace("\r", "");
        }

        /// <summary>
        /// Read a 16-bit integer
        /// </summary>
        public ushort ReadUInt16()
        {
            if (_position + 2 > _data.Length)
                throw new ArgumentException("Not enough data to read UInt16");

            var result = BitConverter.ToUInt16(_data.Slice(_position, 2));
            _position += 2;
            return result;
        }

        /// <summary>
        /// Read a 32-bit integer
        /// </summary>
        public uint ReadUInt32()
        {
            if (_position + 4 > _data.Length)
                throw new ArgumentException("Not enough data to read UInt32");

            var result = BitConverter.ToUInt32(_data.Slice(_position, 4));
            _position += 4;
            return result;
        }

        /// <summary>
        /// Read a 64-bit integer
        /// </summary>
        public ulong ReadUInt64()
        {
            if (_position + 8 > _data.Length)
                throw new ArgumentException("Not enough data to read UInt64");

            var result = BitConverter.ToUInt64(_data.Slice(_position, 8));
            _position += 8;
            return result;
        }

        /// <summary>
        /// Read a byte
        /// </summary>
        public byte ReadByte()
        {
            if (_position >= _data.Length)
                throw new ArgumentException("Not enough data to read byte");

            return _data[_position++];
        }

        /// <summary>
        /// Read a byte array
        /// </summary>
        public byte[] ReadBytes(int count)
        {
            if (_position + count > _data.Length)
                throw new ArgumentException("Not enough data to read bytes");

            var result = _data.Slice(_position, count).ToArray();
            _position += count;
            return result;
        }

        /// <summary>
        /// Skip bytes
        /// </summary>
        public void Skip(int count)
        {
            if (_position + count > _data.Length)
                throw new ArgumentException("Cannot skip beyond data bounds");

            _position += count;
        }

        /// <summary>
        /// Get remaining data
        /// </summary>
        public ReadOnlySpan<byte> GetRemainingData()
        {
            return _data.Slice(_position);
        }
    }

    /// <summary>
    /// Improved packet writer with better performance
    /// </summary>
    public class PacketWriter
    {
        private readonly List<byte> _buffer;
        private readonly ArrayPool<byte> _pool;

        public PacketWriter(int initialCapacity = 64)
        {
            _buffer = new List<byte>(initialCapacity);
            _pool = ArrayPool<byte>.Shared;
        }

        /// <summary>
        /// Write a 7-bit encoded integer
        /// </summary>
        public PacketWriter Write7BitEncodedInt(uint value)
        {
            while (value > 0x7F)
            {
                _buffer.Add((byte)((value & 0x7F) | 0x80));
                value >>= 7;
            }
            _buffer.Add((byte)(value & 0x7F));
            return this;
        }

        /// <summary>
        /// Write a string
        /// </summary>
        public PacketWriter WriteString(string value, int fixedLength = 0)
        {
            var bytes = Encoding.Default.GetBytes(value ?? "");
            
            if (fixedLength > 0)
            {
                var paddedBytes = new byte[fixedLength];
                Array.Copy(bytes, 0, paddedBytes, 0, Math.Min(bytes.Length, fixedLength));
                _buffer.AddRange(paddedBytes);
            }
            else
            {
                _buffer.AddRange(bytes);
            }
            
            return this;
        }

        /// <summary>
        /// Write a 16-bit integer
        /// </summary>
        public PacketWriter WriteUInt16(ushort value)
        {
            _buffer.AddRange(BitConverter.GetBytes(value));
            return this;
        }

        /// <summary>
        /// Write a 32-bit integer
        /// </summary>
        public PacketWriter WriteUInt32(uint value)
        {
            _buffer.AddRange(BitConverter.GetBytes(value));
            return this;
        }

        /// <summary>
        /// Write a 64-bit integer
        /// </summary>
        public PacketWriter WriteUInt64(ulong value)
        {
            _buffer.AddRange(BitConverter.GetBytes(value));
            return this;
        }

        /// <summary>
        /// Write a byte
        /// </summary>
        public PacketWriter WriteByte(byte value)
        {
            _buffer.Add(value);
            return this;
        }

        /// <summary>
        /// Write a byte array
        /// </summary>
        public PacketWriter WriteBytes(byte[] value)
        {
            if (value != null)
                _buffer.AddRange(value);
            return this;
        }

        /// <summary>
        /// Write multiple 7-bit encoded integers in protocol format
        /// </summary>
        public PacketWriter WriteProtocolBuffer(params uint[] values)
        {
            _buffer.Add(8); // Protocol marker
            
            for (int i = 0; i < values.Length; i++)
            {
                Write7BitEncodedInt(values[i]);
                _buffer.Add((byte)(8 * (i + 2))); // Offset marker
            }
            
            return this;
        }

        /// <summary>
        /// Get the final packet buffer
        /// </summary>
        public byte[] Encode()
        {
            return _buffer.ToArray();
        }

        /// <summary>
        /// Clear the buffer
        /// </summary>
        public void Clear()
        {
            _buffer.Clear();
        }

        /// <summary>
        /// Get current buffer size
        /// </summary>
        public int Count => _buffer.Count;
    }

    /// <summary>
    /// Packet factory for creating common packet types
    /// </summary>
    public static class PacketFactory
    {
        private static readonly ulong ClientSeal = BitConverter.ToUInt64(Encoding.Default.GetBytes("TQClient"), 0);

        /// <summary>
        /// Create a packet with the given ID and data
        /// </summary>
        public static byte[] CreatePacket(ushort id, byte[] data = null)
        {
            return PacketStructure.Create(id, 0, 0, data);
        }

        /// <summary>
        /// Create a packet with the given ID and writer content
        /// </summary>
        public static byte[] CreatePacket(ushort id, Action<PacketWriter> writeAction)
        {
            var writer = new PacketWriter();
            writeAction?.Invoke(writer);
            return PacketStructure.Create(id, 0, 0, writer.Encode());
        }

        /// <summary>
        /// Create a protocol buffer packet
        /// </summary>
        public static byte[] CreateProtocolPacket(ushort id, params uint[] values)
        {
            var writer = new PacketWriter();
            writer.WriteProtocolBuffer(values);
            return PacketStructure.Create(id, 0, 0, writer.Encode());
        }

        /// <summary>
        /// Validate a packet seal
        /// </summary>
        public static bool ValidateSeal(byte[] packet)
        {
            if (packet == null || packet.Length < 8)
                return false;

            var sealStart = Math.Max(0, packet.Length - 8);
            var seal = BitConverter.ToUInt64(packet, sealStart);
            return seal == ClientSeal;
        }
    }
} 