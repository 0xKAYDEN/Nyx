using System;
using System.Buffers;
using System.Security.Cryptography;
using CO2_CORE_DLL.Security.Cryptography;

namespace Nyx.Server.Network.Cryptography
{
    /// <summary>
    /// Safe replacement for the unsafe GameCryptography class with proper bounds checking
    /// </summary>
    public class SafeGameCryptography
    {
        private CAST5 _cast5;
        private readonly object _lockObject = new object();

        public SafeGameCryptography(byte[] key)
        {
            if (key == null || key.Length == 0)
                throw new ArgumentException("Key cannot be null or empty", nameof(key));
            
            _cast5 = new CAST5();
            _cast5.GenerateKey(key);
        }

        /// <summary>
        /// Safely decrypt packet data with minimal unsafe code
        /// </summary>
        public bool Decrypt(byte[] packet, int length)
        {
            if (packet == null || length <= 0 || length > packet.Length)
                return false;
            
            try
            {
                lock (_lockObject)
                {
                    unsafe
                    {
                        fixed (byte* ptr = packet)
                        {
                            _cast5.Decrypt(ptr, length);
                        }
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log the error but don't expose sensitive information
                Console.WriteLine($"[ERROR] Decryption failed: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Safely encrypt packet data with minimal unsafe code
        /// </summary>
        public bool Encrypt(byte[] packet, int length)
        {
            if (packet == null || length <= 0 || length > packet.Length)
                return false;
            
            try
            {
                lock (_lockObject)
                {
                    unsafe
                    {
                        fixed (byte* ptr = packet)
                        {
                            _cast5.Encrypt(ptr, length);
                        }
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log the error but don't expose sensitive information
                Console.WriteLine($"[ERROR] Encryption failed: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Set encryption key with validation
        /// </summary>
        public bool SetKey(byte[] key)
        {
            if (key == null || key.Length == 0)
                return false;
            
            try
            {
                lock (_lockObject)
                {
                    _cast5.GenerateKey(key);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Key generation failed: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Set initialization vectors with validation
        /// </summary>
        public bool SetIvs(byte[] iv1, byte[] iv2)
        {
            if (iv1 == null || iv2 == null)
                return false;
            
            try
            {
                lock (_lockObject)
                {
                    _cast5.SetIVs(iv1, iv2);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] IV setting failed: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Validate packet data before encryption/decryption
        /// </summary>
        public bool ValidatePacket(byte[] packet, int length)
        {
            return packet != null && 
                   length > 0 && 
                   length <= packet.Length && 
                   length <= 8192; // Reasonable packet size limit
        }

        /// <summary>
        /// Create a new encrypted packet from data
        /// </summary>
        public byte[] CreateEncryptedPacket(byte[] data)
        {
            if (data == null || data.Length == 0)
                return new byte[0];
            
            try
            {
                var packet = new byte[data.Length];
                Array.Copy(data, 0, packet, 0, data.Length);
                
                if (Encrypt(packet, packet.Length))
                    return packet;
                
                return new byte[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Packet creation failed: {ex.Message}");
                return new byte[0];
            }
        }

        /// <summary>
        /// Create a new decrypted packet from data
        /// </summary>
        public byte[] CreateDecryptedPacket(byte[] data)
        {
            if (data == null || data.Length == 0)
                return new byte[0];
            
            try
            {
                var packet = new byte[data.Length];
                Array.Copy(data, 0, packet, 0, data.Length);
                
                if (Decrypt(packet, packet.Length))
                    return packet;
                
                return new byte[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Packet decryption failed: {ex.Message}");
                return new byte[0];
            }
        }

        /// <summary>
        /// Dispose of cryptographic resources
        /// </summary>
        public void Dispose()
        {
            try
            {
                lock (_lockObject)
                {
                    _cast5 = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Disposal failed: {ex.Message}");
            }
        }
    }
} 