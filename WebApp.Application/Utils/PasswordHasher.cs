using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Application.Utils
{
    public class PasswordHasher
    {
        private const int _salt = 64;
        private const int _hashSize = 128;
        private const int _iteration = 100;

        public string HashPassword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(_salt);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, _iteration, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(_hashSize);
                return Convert.ToBase64String(Combine(salt, hash));
            }
        }

        public bool VerifyPassword(string password, byte[] salt, string hash)
        {
            byte[] hashBytes = Convert.FromBase64String(hash);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, _iteration, HashAlgorithmName.SHA256))
            {
                byte[] computedHash = pbkdf2.GetBytes(_hashSize);
                for (int i = 0; i < _hashSize; i++)
                {
                    if (hashBytes[i + _salt] != computedHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private byte[] Combine(byte[] salt, byte[] hash)
        {
            byte[] result = new byte[salt.Length + hash.Length];
            Buffer.BlockCopy(salt, 0, result, 0, salt.Length);
            Buffer.BlockCopy(hash, 0, result, salt.Length, hash.Length);
            return result;
        }
    }
}



