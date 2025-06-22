using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Konscious.Security.Cryptography;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Dickplom1.Class
{
    public class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            var passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
            var argon2 = new Konscious.Security.Cryptography.Argon2id(passwordBytes)
            {
                DegreeOfParallelism = 4, // количество потоков
                MemorySize = 65536, // 64 MB
                Iterations = 4
            };

            var hashBytes = argon2.GetBytes(32); // длина хэша 32 байта (256 бит)
            return System.Convert.ToBase64String(hashBytes);
        }

        public static bool VerifyPassword(string password, string storedHash)
        {
            var computedHash = HashPassword(password);
            var computedBytes = System.Text.Encoding.UTF8.GetBytes(computedHash);
            var storedBytes = System.Text.Encoding.UTF8.GetBytes(storedHash);

            if (computedBytes.Length != storedBytes.Length)
                return false;

            int result = 0;
            for (int i = 0; i < computedBytes.Length; i++)
            {
                result |= computedBytes[i] ^ storedBytes[i];
            }

            return result == 0;
        }

        public static string GenerateTemporaryPassword(int length = 10)
        {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz23456789";
            var data = new byte[length];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(data);

            var result = new System.Text.StringBuilder(length);
            foreach (var b in data)
            {
                result.Append(chars[b % chars.Length]);
            }

            return result.ToString();
        }
    }
}
