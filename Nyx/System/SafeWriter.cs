using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace Nyx.Server.Network
{
    /// <summary>
    /// Safe replacement for the unsafe Writer class with proper bounds checking and validation
    /// </summary>
    public static class SafeWriter
    {
        /// <summary>
        /// Write a string to buffer with proper bounds checking
        /// </summary>
        public static bool Write(string arg, int offset, byte[] buffer)
        {
            if (buffer == null || arg == null)
                return false;
            
            if (offset < 0 || offset >= buffer.Length)
                return false;
            
            try
            {
                var argEncoded = Program.Encoding.GetBytes(arg);
                var maxLength = Math.Min(argEncoded.Length, buffer.Length - offset);
                
                if (maxLength > 0)
                {
                    Array.Copy(argEncoded, 0, buffer, offset, maxLength);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Write a byte to buffer with bounds checking
        /// </summary>
        public static bool Write(byte arg, int offset, byte[] buffer)
        {
            if (buffer == null)
                return false;
            
            if (offset < 0 || offset >= buffer.Length)
                return false;
            
            try
            {
                buffer[offset] = arg;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Write a boolean to buffer with bounds checking
        /// </summary>
        public static bool Write(bool arg, int offset, byte[] buffer)
        {
            return Write(arg ? (byte)1 : (byte)0, offset, buffer);
        }

        /// <summary>
        /// Write a 16-bit unsigned integer to buffer with bounds checking
        /// </summary>
        public static bool Write(ushort arg, int offset, byte[] buffer)
        {
            if (buffer == null)
                return false;
            
            if (offset < 0 || offset + sizeof(ushort) > buffer.Length)
                return false;
            
            try
            {
                // Use BitConverter for safe conversion
                var bytes = BitConverter.GetBytes(arg);
                Array.Copy(bytes, 0, buffer, offset, sizeof(ushort));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Write a 32-bit unsigned integer to buffer with bounds checking
        /// </summary>
        public static bool Write(uint arg, int offset, byte[] buffer)
        {
            if (buffer == null)
                return false;
            
            if (offset < 0 || offset + sizeof(uint) > buffer.Length)
                return false;
            
            try
            {
                // Use BitConverter for safe conversion
                var bytes = BitConverter.GetBytes(arg);
                Array.Copy(bytes, 0, buffer, offset, sizeof(uint));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Write a 64-bit unsigned integer to buffer with bounds checking
        /// </summary>
        public static bool Write(ulong arg, int offset, byte[] buffer)
        {
            if (buffer == null)
                return false;
            
            if (offset < 0 || offset + sizeof(ulong) > buffer.Length)
                return false;
            
            try
            {
                // Use BitConverter for safe conversion
                var bytes = BitConverter.GetBytes(arg);
                Array.Copy(bytes, 0, buffer, offset, sizeof(ulong));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Write a 32-bit signed integer to buffer with bounds checking
        /// </summary>
        public static bool Write(int arg, int offset, byte[] buffer)
        {
            if (buffer == null)
                return false;
            
            if (offset < 0 || offset + sizeof(int) > buffer.Length)
                return false;
            
            try
            {
                // Use BitConverter for safe conversion
                var bytes = BitConverter.GetBytes(arg);
                Array.Copy(bytes, 0, buffer, offset, sizeof(int));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Write a decimal to buffer with bounds checking
        /// </summary>
        public static bool Write(decimal arg, int offset, byte[] buffer)
        {
            if (buffer == null)
                return false;
            
            if (offset < 0 || offset + sizeof(decimal) > buffer.Length)
                return false;
            
            try
            {
                // Use BitConverter for safe conversion
                var bytes = new byte[sizeof(decimal)];
                var bits = decimal.GetBits(arg);
                
                for (int i = 0; i < 4; i++)
                {
                    var intBytes = BitConverter.GetBytes(bits[i]);
                    Array.Copy(intBytes, 0, bytes, i * sizeof(int), sizeof(int));
                }
                
                Array.Copy(bytes, 0, buffer, offset, sizeof(decimal));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Write a list of strings to buffer with bounds checking
        /// </summary>
        public static bool Write(List<string> arg, int offset, byte[] buffer)
        {
            if (arg == null || buffer == null)
                return false;
            
            if (offset < 0 || offset >= buffer.Length)
                return false;
            
            try
            {
                var currentOffset = offset;
                
                // Write count
                if (!Write((byte)arg.Count, currentOffset, buffer))
                    return false;
                currentOffset++;
                
                // Write each string
                foreach (var str in arg)
                {
                    if (currentOffset >= buffer.Length)
                        return false;
                    
                    if (!Write((byte)str.Length, currentOffset, buffer))
                        return false;
                    currentOffset++;
                    
                    if (!Write(str, currentOffset, buffer))
                        return false;
                    currentOffset += str.Length;
                }
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Write an array of strings to buffer with bounds checking
        /// </summary>
        public static bool Write(string[] arg, int offset, byte[] buffer)
        {
            if (arg == null || buffer == null)
                return false;
            
            if (offset < 0 || offset >= buffer.Length)
                return false;
            
            try
            {
                var currentOffset = offset;
                
                // Write count
                if (!Write((byte)arg.Length, currentOffset, buffer))
                    return false;
                currentOffset++;
                
                // Write each string
                foreach (var str in arg)
                {
                    if (currentOffset >= buffer.Length)
                        return false;
                    
                    if (!Write((byte)str.Length, currentOffset, buffer))
                        return false;
                    currentOffset++;
                    
                    if (!Write(str, currentOffset, buffer))
                        return false;
                    currentOffset += str.Length;
                }
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Write a string with length prefix to buffer with bounds checking
        /// </summary>
        public static bool WriteWithLength(string arg, int offset, byte[] buffer)
        {
            if (buffer == null || arg == null)
                return false;
            
            if (offset < 0 || offset >= buffer.Length)
                return false;
            
            try
            {
                var currentOffset = offset;
                
                // Write length
                if (!Write((byte)arg.Length, currentOffset, buffer))
                    return false;
                currentOffset++;
                
                // Write string
                if (!Write(arg, currentOffset, buffer))
                    return false;
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Write a string with fixed length to buffer with bounds checking
        /// </summary>
        public static bool WriteString(string arg, int offset, byte[] buffer, int fixedLength = 0)
        {
            if (buffer == null || arg == null)
                return false;
            
            if (offset < 0 || offset >= buffer.Length)
                return false;
            
            try
            {
                var argEncoded = Program.Encoding.GetBytes(arg);
                var writeLength = fixedLength > 0 ? Math.Min(fixedLength, buffer.Length - offset) : Math.Min(argEncoded.Length, buffer.Length - offset);
                
                if (writeLength > 0)
                {
                    Array.Copy(argEncoded, 0, buffer, offset, writeLength);
                    
                    // Pad with zeros if fixed length is specified
                    if (fixedLength > 0 && writeLength < fixedLength)
                    {
                        var remainingLength = Math.Min(fixedLength - writeLength, buffer.Length - offset - writeLength);
                        if (remainingLength > 0)
                        {
                            Array.Clear(buffer, offset + writeLength, remainingLength);
                        }
                    }
                    
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Write a byte array to buffer with bounds checking
        /// </summary>
        public static bool WriteBytes(byte[] arg, int offset, byte[] buffer)
        {
            if (buffer == null || arg == null)
                return false;
            
            if (offset < 0 || offset >= buffer.Length)
                return false;
            
            try
            {
                var writeLength = Math.Min(arg.Length, buffer.Length - offset);
                
                if (writeLength > 0)
                {
                    Array.Copy(arg, 0, buffer, offset, writeLength);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Create a new buffer with the specified size and write data to it
        /// </summary>
        public static byte[] CreateBuffer(int size, Action<byte[], int> writeAction)
        {
            if (size <= 0)
                return new byte[0];
            
            try
            {
                var buffer = new byte[size];
                writeAction?.Invoke(buffer, 0);
                return buffer;
            }
            catch (Exception)
            {
                return new byte[0];
            }
        }

        /// <summary>
        /// Validate buffer bounds before writing
        /// </summary>
        public static bool ValidateBounds(int offset, int dataSize, byte[] buffer)
        {
            return buffer != null && 
                   offset >= 0 && 
                   dataSize >= 0 && 
                   offset + dataSize <= buffer.Length;
        }

        /// <summary>
        /// Get the maximum safe write length for a given offset and buffer
        /// </summary>
        public static int GetMaxWriteLength(int offset, byte[] buffer)
        {
            if (buffer == null || offset < 0 || offset >= buffer.Length)
                return 0;
            
            return buffer.Length - offset;
        }
    }
} 