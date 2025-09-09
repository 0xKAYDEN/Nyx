using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Nyx.Server.Network
{
    /// <summary>
    /// Safe replacement for the unsafe Structure class with proper bounds checking and validation
    /// </summary>
    public abstract class SafeStructure
    {
        private readonly MemoryStream _buffer;
        private readonly object _lockObject = new object();

        protected SafeStructure(byte[] packet)
        {
            if (packet == null)
                throw new ArgumentNullException(nameof(packet));
            
            _buffer = new MemoryStream(packet);
        }

        protected SafeStructure(int length)
        {
            if (length <= 0)
                throw new ArgumentException("Length must be positive", nameof(length));
            
            _buffer = new MemoryStream(new byte[length]);
        }

        #region Safe Writing Methods

        /// <summary>
        /// Safely write a 32-bit unsigned integer
        /// </summary>
        protected bool WriteUint(uint value, int offset)
        {
            return SafeWrite(value, offset, sizeof(uint));
        }

        /// <summary>
        /// Safely write a 32-bit signed integer
        /// </summary>
        protected bool WriteInt(int value, int offset)
        {
            return SafeWrite(value, offset, sizeof(int));
        }

        /// <summary>
        /// Safely write a string with length prefix
        /// </summary>
        protected bool WriteString(string value, int offset)
        {
            if (value == null)
                return false;
            
            try
            {
                lock (_lockObject)
                {
                    _buffer.Position = offset - 1;
                    using (var writer = new BinaryWriter(_buffer, Encoding.Default, true))
                    {
                        writer.Write(value.Length);
                        _buffer.Position = offset;
                        writer.Write(value);
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] String write failed: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Safely write a character
        /// </summary>
        protected bool WriteChar(char value, int offset)
        {
            return SafeWrite(value, offset, sizeof(char));
        }

        /// <summary>
        /// Safely write a byte
        /// </summary>
        protected bool WriteByte(byte value, int offset)
        {
            return SafeWrite(value, offset, sizeof(byte));
        }

        /// <summary>
        /// Safely write a signed byte
        /// </summary>
        protected bool WriteSByte(sbyte value, int offset)
        {
            return SafeWrite(value, offset, sizeof(sbyte));
        }

        /// <summary>
        /// Safely write a boolean
        /// </summary>
        protected bool WriteBoolean(bool value, int offset)
        {
            return SafeWrite((byte)(value ? 1 : 0), offset, sizeof(byte));
        }

        /// <summary>
        /// Safely write a float
        /// </summary>
        protected bool WriteFloat(float value, int offset)
        {
            return SafeWrite(value, offset, sizeof(float));
        }

        /// <summary>
        /// Safely write a 64-bit signed integer
        /// </summary>
        protected bool WriteLong(long value, int offset)
        {
            return SafeWrite(value, offset, sizeof(long));
        }

        /// <summary>
        /// Safely write a 64-bit unsigned integer
        /// </summary>
        protected bool WriteUlong(ulong value, int offset)
        {
            return SafeWrite(value, offset, sizeof(ulong));
        }

        /// <summary>
        /// Safely write a 16-bit signed integer
        /// </summary>
        protected bool WriteShort(short value, int offset)
        {
            return SafeWrite(value, offset, sizeof(short));
        }

        /// <summary>
        /// Safely write a 16-bit unsigned integer
        /// </summary>
        protected bool WriteUshort(ushort value, int offset)
        {
            return SafeWrite(value, offset, sizeof(ushort));
        }

        /// <summary>
        /// Safely write a decimal
        /// </summary>
        protected bool WriteDecimal(decimal value, int offset)
        {
            return SafeWrite(value, offset, sizeof(decimal));
        }

        #endregion

        #region Safe Reading Methods

        /// <summary>
        /// Safely read a 32-bit signed integer
        /// </summary>
        protected int ReadInt(int offset)
        {
            return SafeRead<int>(offset, sizeof(int));
        }

        /// <summary>
        /// Safely read a 32-bit unsigned integer
        /// </summary>
        protected uint ReadUInt(int offset)
        {
            return SafeRead<uint>(offset, sizeof(uint));
        }

        /// <summary>
        /// Safely read a 16-bit unsigned integer
        /// </summary>
        protected ushort ReadUshort(int offset)
        {
            return SafeRead<ushort>(offset, sizeof(ushort));
        }

        /// <summary>
        /// Safely read a 16-bit signed integer
        /// </summary>
        protected short ReadShort(int offset)
        {
            return SafeRead<short>(offset, sizeof(short));
        }

        /// <summary>
        /// Safely read a 64-bit signed integer
        /// </summary>
        protected long ReadLong(int offset)
        {
            return SafeRead<long>(offset, sizeof(long));
        }

        /// <summary>
        /// Safely read a 64-bit unsigned integer
        /// </summary>
        protected ulong ReadULong(int offset)
        {
            return SafeRead<ulong>(offset, sizeof(ulong));
        }

        /// <summary>
        /// Safely read a byte
        /// </summary>
        protected byte ReadByte(int offset)
        {
            return SafeRead<byte>(offset, sizeof(byte));
        }

        /// <summary>
        /// Safely read a signed byte
        /// </summary>
        protected sbyte ReadSByte(int offset)
        {
            return SafeRead<sbyte>(offset, sizeof(sbyte));
        }

        /// <summary>
        /// Safely read a boolean
        /// </summary>
        protected bool ReadBoolean(int offset)
        {
            return SafeRead<byte>(offset, sizeof(byte)) != 0;
        }

        /// <summary>
        /// Safely read a float
        /// </summary>
        protected float ReadFloat(int offset)
        {
            return SafeRead<float>(offset, sizeof(float));
        }

        /// <summary>
        /// Safely read a string
        /// </summary>
        protected string ReadString(int offset)
        {
            try
            {
                lock (_lockObject)
                {
                    _buffer.Position = offset;
                    using (var reader = new BinaryReader(_buffer, Encoding.Default, true))
                    {
                        return reader.ReadString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] String read failed: {ex.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        /// Safely read a character
        /// </summary>
        protected char ReadChar(int offset)
        {
            return SafeRead<char>(offset, sizeof(char));
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Generic safe write method
        /// </summary>
        private bool SafeWrite<T>(T value, int offset, int size) where T : struct
        {
            try
            {
                lock (_lockObject)
                {
                    if (offset < 0 || offset + size > _buffer.Length)
                        return false;
                    
                    _buffer.Position = offset;
                    using (var writer = new BinaryWriter(_buffer, Encoding.Default, true))
                    {
                        switch (value)
                        {
                            case uint u: writer.Write(u); break;
                            case int i: writer.Write(i); break;
                            case char c: writer.Write(c); break;
                            case byte b: writer.Write(b); break;
                            case sbyte sb: writer.Write(sb); break;
                            case float f: writer.Write(f); break;
                            case long l: writer.Write(l); break;
                            case ulong ul: writer.Write(ul); break;
                            case short s: writer.Write(s); break;
                            case ushort us: writer.Write(us); break;
                            case decimal d: writer.Write(d); break;
                            default: return false;
                        }
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Write failed: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Generic safe read method
        /// </summary>
        private T SafeRead<T>(int offset, int size) where T : struct
        {
            try
            {
                lock (_lockObject)
                {
                    if (offset < 0 || offset + size > _buffer.Length)
                        return default(T);
                    
                    _buffer.Position = offset;
                    using (var reader = new BinaryReader(_buffer, Encoding.Default, true))
                    {
                        if (typeof(T) == typeof(uint)) return (T)(object)reader.ReadUInt32();
                        if (typeof(T) == typeof(int)) return (T)(object)reader.ReadInt32();
                        if (typeof(T) == typeof(ushort)) return (T)(object)reader.ReadUInt16();
                        if (typeof(T) == typeof(short)) return (T)(object)reader.ReadInt16();
                        if (typeof(T) == typeof(long)) return (T)(object)reader.ReadInt64();
                        if (typeof(T) == typeof(ulong)) return (T)(object)reader.ReadUInt64();
                        if (typeof(T) == typeof(byte)) return (T)(object)reader.ReadByte();
                        if (typeof(T) == typeof(sbyte)) return (T)(object)reader.ReadSByte();
                        if (typeof(T) == typeof(float)) return (T)(object)reader.ReadSingle();
                        if (typeof(T) == typeof(char)) return (T)(object)reader.ReadChar();
                        if (typeof(T) == typeof(decimal)) return (T)(object)reader.ReadDecimal();
                        
                        return default(T);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Read failed: {ex.Message}");
                return default(T);
            }
        }

        /// <summary>
        /// Build the final buffer
        /// </summary>
        protected virtual byte[] Build()
        {
            try
            {
                lock (_lockObject)
                {
                    _buffer.Position = 0;
                    return _buffer.ToArray();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Build failed: {ex.Message}");
                return new byte[0];
            }
        }

        /// <summary>
        /// Validate buffer bounds
        /// </summary>
        protected bool ValidateBounds(int offset, int size)
        {
            return offset >= 0 && offset + size <= _buffer.Length;
        }

        /// <summary>
        /// Get buffer length
        /// </summary>
        protected long BufferLength => _buffer.Length;

        /// <summary>
        /// Get current position
        /// </summary>
        protected long Position
        {
            get => _buffer.Position;
            set
            {
                if (value >= 0 && value <= _buffer.Length)
                    _buffer.Position = value;
            }
        }

        #endregion

        /// <summary>
        /// Dispose of resources
        /// </summary>
        public void Dispose()
        {
            try
            {
                _buffer?.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Disposal failed: {ex.Message}");
            }
        }
    }
} 