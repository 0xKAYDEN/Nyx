using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Nyx.Server
{
    /// <summary>
    /// Comprehensive security validator for detecting and preventing common vulnerabilities
    /// </summary>
    public static class SecurityValidator
    {
        private static readonly Dictionary<string, SecurityIssue> _securityIssues = new Dictionary<string, SecurityIssue>();
        private static readonly object _lockObject = new object();

        /// <summary>
        /// Security issue information
        /// </summary>
        public class SecurityIssue
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public SecuritySeverity Severity { get; set; }
            public string FilePath { get; set; }
            public int LineNumber { get; set; }
            public string Code { get; set; }
            public DateTime Detected { get; set; }
            public bool IsResolved { get; set; }
        }

        /// <summary>
        /// Security severity levels
        /// </summary>
        public enum SecuritySeverity
        {
            Low,
            Medium,
            High,
            Critical
        }

        /// <summary>
        /// Validate input data for common security issues
        /// </summary>
        public static bool ValidateInput(string input, InputValidationType validationType)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            try
            {
                switch (validationType)
                {
                    case InputValidationType.Username:
                        return ValidateUsername(input);
                    case InputValidationType.Password:
                        return ValidatePassword(input);
                    case InputValidationType.Email:
                        return ValidateEmail(input);
                    case InputValidationType.FilePath:
                        return ValidateFilePath(input);
                    case InputValidationType.SQL:
                        return ValidateSQLInjection(input);
                    case InputValidationType.XSS:
                        return ValidateXSS(input);
                    case InputValidationType.Command:
                        return ValidateCommandInjection(input);
                    default:
                        return true;
                }
            }
            catch (Exception ex)
            {
                LogSecurityIssue("INPUT_VALIDATION_ERROR", "Input validation error", ex.Message, SecuritySeverity.High);
                return false;
            }
        }

        /// <summary>
        /// Validate username input
        /// </summary>
        private static bool ValidateUsername(string username)
        {
            // Username should be 3-20 characters, alphanumeric and underscore only
            var pattern = @"^[a-zA-Z0-9_]{3,20}$";
            return Regex.IsMatch(username, pattern);
        }

        /// <summary>
        /// Validate password input
        /// </summary>
        private static bool ValidatePassword(string password)
        {
            // Password should be at least 8 characters with mixed case and numbers
            if (password.Length < 8)
                return false;

            var hasUpper = password.Any(char.IsUpper);
            var hasLower = password.Any(char.IsLower);
            var hasDigit = password.Any(char.IsDigit);

            return hasUpper && hasLower && hasDigit;
        }

        /// <summary>
        /// Validate email input
        /// </summary>
        private static bool ValidateEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Validate file path input
        /// </summary>
        private static bool ValidateFilePath(string path)
        {
            // Prevent path traversal attacks
            if (path.Contains("..") || path.Contains("\\") || path.Contains("/"))
                return false;

            // Check for invalid characters
            var invalidChars = Path.GetInvalidPathChars();
            return !path.Any(c => invalidChars.Contains(c));
        }

        /// <summary>
        /// Validate for SQL injection
        /// </summary>
        private static bool ValidateSQLInjection(string input)
        {
            var sqlPatterns = new[]
            {
                @"(\b(SELECT|INSERT|UPDATE|DELETE|DROP|CREATE|ALTER|EXEC|EXECUTE)\b)",
                @"(\b(UNION|OR|AND)\b)",
                @"(--|/\*|\*/)",
                @"(\b(WAITFOR|DELAY)\b)",
                @"(\b(CHAR|ASCII|SUBSTRING)\b)"
            };

            return !sqlPatterns.Any(pattern => Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase));
        }

        /// <summary>
        /// Validate for XSS attacks
        /// </summary>
        private static bool ValidateXSS(string input)
        {
            var xssPatterns = new[]
            {
                @"<script[^>]*>.*?</script>",
                @"javascript:",
                @"on\w+\s*=",
                @"<iframe[^>]*>",
                @"<object[^>]*>",
                @"<embed[^>]*>"
            };

            return !xssPatterns.Any(pattern => Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase));
        }

        /// <summary>
        /// Validate for command injection
        /// </summary>
        private static bool ValidateCommandInjection(string input)
        {
            var commandPatterns = new[]
            {
                @"[;&|`$()]",
                @"\b(cmd|powershell|bash|sh)\b",
                @"\b(dir|ls|cat|type|more|less)\b",
                @"\b(net|ipconfig|ifconfig)\b"
            };

            return !commandPatterns.Any(pattern => Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase));
        }

        /// <summary>
        /// Validate buffer bounds to prevent buffer overflow
        /// </summary>
        public static bool ValidateBufferBounds(int offset, int length, int bufferSize)
        {
            return offset >= 0 && 
                   length >= 0 && 
                   offset + length <= bufferSize &&
                   length <= 8192; // Reasonable size limit
        }

        /// <summary>
        /// Validate array bounds to prevent index out of range
        /// </summary>
        public static bool ValidateArrayBounds(int index, int arrayLength)
        {
            return index >= 0 && index < arrayLength;
        }

        /// <summary>
        /// Validate file size to prevent DoS attacks
        /// </summary>
        public static bool ValidateFileSize(long fileSize, long maxSize = 10 * 1024 * 1024) // 10MB default
        {
            return fileSize >= 0 && fileSize <= maxSize;
        }

        /// <summary>
        /// Validate packet size to prevent DoS attacks
        /// </summary>
        public static bool ValidatePacketSize(int packetSize, int maxSize = 8192) // 8KB default
        {
            return packetSize > 0 && packetSize <= maxSize;
        }

        /// <summary>
        /// Sanitize string input
        /// </summary>
        public static string SanitizeString(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            // Remove null characters
            input = input.Replace("\0", "");
            
            // Remove control characters except newlines and tabs
            input = Regex.Replace(input, @"[\x00-\x08\x0B\x0C\x0E-\x1F\x7F]", "");
            
            // Trim whitespace
            input = input.Trim();
            
            return input;
        }

        /// <summary>
        /// Sanitize file path
        /// </summary>
        public static string SanitizeFilePath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            // Remove path traversal attempts
            path = path.Replace("..", "");
            path = path.Replace("\\", "");
            path = path.Replace("/", "");
            
            // Remove invalid characters
            var invalidChars = Path.GetInvalidFileNameChars();
            foreach (var c in invalidChars)
            {
                path = path.Replace(c.ToString(), "");
            }
            
            return path.Trim();
        }

        /// <summary>
        /// Generate secure random bytes
        /// </summary>
        public static byte[] GenerateSecureRandomBytes(int length)
        {
            if (length <= 0)
                return new byte[0];

            var bytes = new byte[length];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }
            return bytes;
        }

        /// <summary>
        /// Generate secure random string
        /// </summary>
        public static string GenerateSecureRandomString(int length)
        {
            if (length <= 0)
                return string.Empty;

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Hash password securely
        /// </summary>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return string.Empty;

            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        /// <summary>
        /// Verify password hash
        /// </summary>
        public static bool VerifyPassword(string password, string hash)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hash))
                return false;

            var passwordHash = HashPassword(password);
            return passwordHash == hash;
        }

        /// <summary>
        /// Log security issue
        /// </summary>
        public static void LogSecurityIssue(string id, string title, string description, SecuritySeverity severity, string filePath = "", int lineNumber = 0, string code = "")
        {
            lock (_lockObject)
            {
                var issue = new SecurityIssue
                {
                    Id = id,
                    Title = title,
                    Description = description,
                    Severity = severity,
                    FilePath = filePath,
                    LineNumber = lineNumber,
                    Code = code,
                    Detected = DateTime.UtcNow,
                    IsResolved = false
                };

                _securityIssues[id] = issue;

                // Log to console
                var severityColor = severity switch
                {
                    SecuritySeverity.Critical => "RED",
                    SecuritySeverity.High => "YELLOW",
                    SecuritySeverity.Medium => "CYAN",
                    SecuritySeverity.Low => "WHITE",
                    _ => "WHITE"
                };

                Console.WriteLine($"[SECURITY][{severityColor}] {title}: {description}");
                if (!string.IsNullOrEmpty(filePath))
                {
                    Console.WriteLine($"[SECURITY] Location: {filePath}:{lineNumber}");
                }
            }
        }

        /// <summary>
        /// Get all security issues
        /// </summary>
        public static List<SecurityIssue> GetSecurityIssues()
        {
            lock (_lockObject)
            {
                return _securityIssues.Values.ToList();
            }
        }

        /// <summary>
        /// Get security issues by severity
        /// </summary>
        public static List<SecurityIssue> GetSecurityIssuesBySeverity(SecuritySeverity severity)
        {
            lock (_lockObject)
            {
                return _securityIssues.Values.Where(i => i.Severity == severity).ToList();
            }
        }

        /// <summary>
        /// Mark security issue as resolved
        /// </summary>
        public static void MarkIssueResolved(string id)
        {
            lock (_lockObject)
            {
                if (_securityIssues.ContainsKey(id))
                {
                    _securityIssues[id].IsResolved = true;
                }
            }
        }

        /// <summary>
        /// Clear resolved security issues
        /// </summary>
        public static void ClearResolvedIssues()
        {
            lock (_lockObject)
            {
                var resolvedIds = _securityIssues.Where(kvp => kvp.Value.IsResolved).Select(kvp => kvp.Key).ToList();
                foreach (var id in resolvedIds)
                {
                    _securityIssues.Remove(id);
                }
            }
        }

        /// <summary>
        /// Get security summary
        /// </summary>
        public static SecuritySummary GetSecuritySummary()
        {
            lock (_lockObject)
            {
                var issues = _securityIssues.Values;
                return new SecuritySummary
                {
                    TotalIssues = issues.Count(),
                    CriticalIssues = issues.Count(i => i.Severity == SecuritySeverity.Critical),
                    HighIssues = issues.Count(i => i.Severity == SecuritySeverity.High),
                    MediumIssues = issues.Count(i => i.Severity == SecuritySeverity.Medium),
                    LowIssues = issues.Count(i => i.Severity == SecuritySeverity.Low),
                    ResolvedIssues = issues.Count(i => i.IsResolved),
                    LastUpdated = DateTime.UtcNow
                };
            }
        }

        /// <summary>
        /// Security summary
        /// </summary>
        public class SecuritySummary
        {
            public int TotalIssues { get; set; }
            public int CriticalIssues { get; set; }
            public int HighIssues { get; set; }
            public int MediumIssues { get; set; }
            public int LowIssues { get; set; }
            public int ResolvedIssues { get; set; }
            public DateTime LastUpdated { get; set; }

            public override string ToString()
            {
                return $"Security: {TotalIssues} total, {CriticalIssues} critical, {HighIssues} high, {ResolvedIssues} resolved";
            }
        }

        /// <summary>
        /// Input validation types
        /// </summary>
        public enum InputValidationType
        {
            Username,
            Password,
            Email,
            FilePath,
            SQL,
            XSS,
            Command
        }
    }
} 