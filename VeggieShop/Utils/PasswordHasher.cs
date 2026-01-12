using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;
using System.Text;

namespace VeggieShop.Utils
{
    public static class PasswordHasher
    {
        public static byte[] ComputeHash(string password, byte[] salt)
        {
            byte[] pw = Encoding.Unicode.GetBytes(password);
            byte[] input = new byte[salt.Length + pw.Length];

            Buffer.BlockCopy(salt, 0, input, 0, salt.Length);
            Buffer.BlockCopy(pw, 0, input, salt.Length, pw.Length);

            using (var sha = SHA256.Create())
            {
                return sha.ComputeHash(input);
            }
        }

        // So sánh bytes kiểu "ít lộ timing" (thay cho CryptographicOperations.FixedTimeEquals)
        private static bool FixedTimeEquals(byte[] a, byte[] b)
        {
            if (a == null || b == null) return false;
            if (a.Length != b.Length) return false;

            int diff = 0;
            for (int i = 0; i < a.Length; i++)
                diff |= a[i] ^ b[i];

            return diff == 0;
        }

        public static bool Verify(string password, byte[] salt, byte[] expectedHash)
        {
            var hash = ComputeHash(password, salt);
            return FixedTimeEquals(hash, expectedHash);
        }

        public static bool VerifyPassword(string password, byte[] hash, byte[] salt)
            => Verify(password, salt, hash);

        public static void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(salt);

            hash = ComputeHash(password, salt);
        }
    }
}
