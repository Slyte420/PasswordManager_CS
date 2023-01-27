using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Crypto
{
    internal class CryptoInstance
    {
        private static CryptoInstance? instance;
        SHA256 SHAinstance = null!;
        RSA RSAInstance = null!;
        Aes AESInstance = null!;
        private CryptoInstance()
        {
            SHAinstance= SHA256.Create();
            RSAInstance = RSA.Create();
            AESInstance = Aes.Create();
        }
        public static CryptoInstance GetInstance()
        {
            if (instance == null)
            {
                instance = new CryptoInstance();
            }
            return instance;
        }

        public string? hashString(string text)
        {
            byte[] textByte = Encoding.ASCII.GetBytes(text);
            SHAinstance.ComputeHash(textByte);
            byte[] hashB = SHAinstance.Hash;
            if(hashB != null) {
                string hashS = Encoding.ASCII.GetString(hashB);
                return hashS;
            }
            return null;
        }
        public string? hashStringSalt(string text, byte[] salt) {
            byte[] textByte = Encoding.ASCII.GetBytes(text);
            byte[] hashStringSalt = new byte[textByte.Length + salt.Length];
            Buffer.BlockCopy(textByte, 0, hashStringSalt, 0, textByte.Length);
            Buffer.BlockCopy(salt, 0, hashStringSalt, textByte.Length, salt.Length);
            SHAinstance.ComputeHash(hashStringSalt);
            byte[] hashB = SHAinstance.Hash;
            if (hashB != null)
            {
                string hashS = Encoding.ASCII.GetString(hashB);
                return hashS;
            }
            return null;
        }
        public byte[] generateSalt(int length)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(length);
            return salt;
        }
    }
}
