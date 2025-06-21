using Isopoh.Cryptography.Argon2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dickplom1.Class
{
    public class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            return Argon2.Hash(password);
        }

        public static bool VerifyPassword(string password, string hash)
        {
            if (hash != null)
                return Argon2.Verify(hash, password);
            else
                return false;
        }

        public static string GenerateTemporaryPassword(int length = 10)
        {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz23456789";
            var data = new byte[length];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(data);
            }

            var result = new StringBuilder(length);
            foreach (byte b in data)
            {
                result.Append(chars[b % chars.Length]);
            }
            return result.ToString();
        }
    }
}
