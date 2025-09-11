using System;
using System.Collections.Generic;

namespace Nyx.Server.Network.GamePackets
{
    public class GroundMovement : Writer, Interfaces.IPacket
    {
        public const uint Walk = 0,
                          Run = 1,
                          Slide = 9;

        private byte[] Buffer;

        public GroundMovement(bool CreateInstance)
        {
            if (CreateInstance)
            {
                Buffer = new byte[32];
                Write(24, 0, Buffer);
                Write(10005, 2, Buffer);
            }
        }
        public byte[] Encode()
        {
            byte[] ptr = CreateProtocolBuffer((uint)Direction, UID, GroundMovementType, TimeStamp, MapID);
            byte[] Buffer = new byte[11 + ptr.Length];
            Writer.Write((ushort)(Buffer.Length - 8), 0, Buffer);
            Writer.Write(10005, 2, Buffer);
            Array.Copy(ptr, 0, Buffer, 4, ptr.Length);
            return Buffer;
        }
        //public byte[] Encode()
        //{
        //    // 1. Create the protocol-specific payload data (e.g., movement data)
        //    byte[] payloadData = CreateProtocolBuffer((uint)Direction, UID, GroundMovementType, TimeStamp, MapID);

        //    // 2. Calculate the total packet length:
        //    //    Header: 4 bytes (2 for length, 2 for packet ID)
        //    //    Payload: payloadData.Length bytes
        //    //    No trailing seal mentioned, so total is 4 + payloadData.Length
        //    ushort totalPacketLength = (ushort)(4 + payloadData.Length);

        //    // 3. Use MemoryStream and BinaryWriter to construct the packet cleanly
        //    using (MemoryStream ms = new MemoryStream(totalPacketLength))
        //    using (BinaryWriter writer = new BinaryWriter(ms))
        //    {
        //        // Write the packet header
        //        // a) Length field (usually indicates length of the data AFTER this field)
        //        writer.Write((ushort)(totalPacketLength - 2)); // Subtract the size of the length field itself (2 bytes)
        //                                                       // b) Packet ID (10005 from the original code)
        //        writer.Write((ushort)10005);

        //        // Write the payload data
        //        writer.Write(payloadData);
        //        Array.Copy(payloadData, 0, Buffer, 4, payloadData.Length);
        //        // Return the entire contents of the MemoryStream as a byte array
        //        return ms.ToArray();
        //    }
        //}

        public void Deserialize(byte[] buffer)
        {
            // Skip 2-byte length and 2-byte packet id
            var payload = new byte[buffer.Length - 4];
            Array.Copy(buffer, 4, payload, 0, payload.Length);

            // Decode compact movement fields (direction, uid, type, timestamp, mapId)
            var values = Read7BitEncodedValues(payload);
            int index = 0;
            var direction = (values[index++] % 24);
            Direction = (Game.Enums.ConquerAngle)direction;
            UID = values[index++];
            GroundMovementType = values[index++];
            TimeStamp = values[index++];
            MapID = values[index++];
        }
        public static byte[] CreateProtocolBuffer(params uint[] values)
        {
            List<byte> ptr = new List<byte>();
            ptr.Add(8);
            for (int x = 0; x < values.Length; x++)
            {
                uint value = values[x];
                while (value > 0x7F)
                {
                    ptr.Add((byte)((value & 0x7F) | 0x80));
                    value >>= 7;
                }
                ptr.Add((byte)(value & 0x7F));
                ptr.Add((byte)(8 * (x + 2)));
                if (x + 1 == values.Length)
                    break;
            }
            return ptr.ToArray();
        }
        public static uint[] Read7BitEncodedInt(byte[] buffer)
        {
            List<uint> ptr2 = new List<uint>();

            for (int i = 0; i < buffer.Length; )
            {
                if (i + 2 <= buffer.Length)
                {
                    int tmp = buffer[i++];

                    if (tmp % 8 == 0)
                        while (true)
                        {
                            if (i + 1 > buffer.Length) break;
                            tmp = buffer[i++];
                            if (tmp < 128)
                            {
                                ptr2.Add((uint)tmp);
                                break;
                            }
                            else
                            {
                                int result = tmp & 0x7f;
                                if ((tmp = buffer[i++]) < 128)
                                {
                                    result |= tmp << 7;
                                    ptr2.Add((uint)result);
                                    break;
                                }
                                else
                                {
                                    result |= (tmp & 0x7f) << 7;
                                    if ((tmp = buffer[i++]) < 128)
                                    {
                                        result |= tmp << 14;
                                        ptr2.Add((uint)result);
                                        break;
                                    }
                                    else
                                    {
                                        result |= (tmp & 0x7f) << 14;
                                        if ((tmp = buffer[i++]) < 128)
                                        {
                                            result |= tmp << 21;
                                            ptr2.Add((uint)result);
                                            break;
                                        }
                                        else
                                        {
                                            result |= (tmp & 0x7f) << 21;
                                            result |= (tmp = buffer[i++]) << 28;
                                            ptr2.Add((uint)result);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                }
                else break;
            }
            return ptr2.ToArray();
        }
        public static uint[] Read7BitEncodedValues(byte[] buffer)
        {
            // Cleaner, equivalent decoder to Read7BitEncodedInt with explicit varint parsing and separator skipping
            List<uint> values = new List<uint>();
            int i = 0;
            if (buffer.Length == 0)
                return values.ToArray();

            // First byte is a tag (0x08) in this protocol variant
            if (buffer[i] % 8 == 0)
                i++;

            while (i < buffer.Length)
            {
                uint result = 0;
                int shift = 0;
                bool gotValue = false;
                while (i < buffer.Length)
                {
                    int b = buffer[i++];
                    if (b < 128)
                    {
                        result |= (uint)(b << shift);
                        values.Add(result);
                        gotValue = true;
                        break;
                    }
                    result |= (uint)((b & 0x7F) << shift);
                    shift += 7;
                }

                if (!gotValue)
                    break;

                // Skip field separator tag if present
                if (i < buffer.Length)
                {
                    int sep = buffer[i];
                    if (sep % 8 == 0)
                        i++;
                }
            }

            return values.ToArray();
        }
        public Game.Enums.ConquerAngle Direction;
        public uint UID;
        public uint GroundMovementType;
        public uint TimeStamp;
        public uint MapID;
        public void Send(Client.GameClient client)
        {
            client.Send(Buffer);
        }
    }
}
