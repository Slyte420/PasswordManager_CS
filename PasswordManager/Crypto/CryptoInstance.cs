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
        RSAEncryptionPadding RSAPadding = null!;
        private CryptoInstance()
        {
            SHAinstance= SHA256.Create();
            RSAInstance = RSA.Create();
            AESInstance = Aes.Create();
            RSAPadding = RSAEncryptionPadding.OaepSHA256;
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
            if(SHAinstance is not null) { 
            byte[] textByte = Encoding.ASCII.GetBytes(text);
            SHAinstance.ComputeHash(textByte);
            byte[]? hashB = SHAinstance.Hash;
                if(hashB != null) 
                {
                    string hashS = Encoding.ASCII.GetString(hashB);
                    return hashS;
                }
            }
            return null;
        }
        public void setAesFromPasswordStringForPasswords(string password, byte[]salt)
        {
            using(Rfc2898DeriveBytes instance = new Rfc2898DeriveBytes(password,salt))
            {
                int keyLength = AESInstance.KeySize / 8;
                AESInstance.Key = instance.GetBytes(keyLength);
                AESInstance.GenerateIV();
            }
        }
        public void setAesFromPasswordStringForKey(string password, byte[] salt)
        {
            using (Rfc2898DeriveBytes instance = new Rfc2898DeriveBytes(password, salt))
            {
                int keyLength = AESInstance.KeySize / 8;
                int IVLength = AESInstance.BlockSize / 8;
                AESInstance.Key = instance.GetBytes(keyLength);
                AESInstance.IV = instance.GetBytes(IVLength);
            }
        }
        public byte[] getIVEncrypted()
        {
            byte[] IV = AESInstance.IV;
            byte[] encryptedIV = RSAInstance.Encrypt(IV,RSAPadding);
            return encryptedIV;
        }
        public void getRSAEncryptedFile(string username,string selectedFolder,byte[] passwordHash)
        {
            string fileName = username + ".key";
            string filePath = Path.Combine(selectedFolder,fileName);
            ICryptoTransform transform = AESInstance.CreateEncryptor();
            using(var outF = new FileStream(filePath, FileMode.Create))
            {
                outF.Write(BitConverter.GetBytes(passwordHash.Length), 0, 4);
                outF.Write(passwordHash,0, passwordHash.Length);
                using(var outStreamEncrypted = new CryptoStream(outF, transform, CryptoStreamMode.Write)) 
                {
                    outStreamEncrypted.Write(Encoding.UTF8.GetBytes(RSAInstance.ToXmlString(true)));
                    outStreamEncrypted.FlushFinalBlock();
                }
            }
        }

        public byte[]? getPrivateKeyHash(byte[] salt)
        {
        
            byte[] privateKeySalt = new byte[RSAInstance.ExportRSAPrivateKey().Length + salt.Length];
            Buffer.BlockCopy(RSAInstance.ExportRSAPrivateKey(), 0, privateKeySalt, 0, RSAInstance.ExportRSAPrivateKey().Length);
            Buffer.BlockCopy(salt, 0, privateKeySalt, RSAInstance.ExportRSAPrivateKey().Length, salt.Length);
            SHAinstance.ComputeHash(privateKeySalt);
            return SHAinstance.Hash;
        }

        public byte[]? hashStringSalt(string text, byte[] salt) {
            if(SHAinstance is not null) { 
            byte[] textByte = Encoding.ASCII.GetBytes(text);
            byte[] hashStringSalt = new byte[textByte.Length + salt.Length];
            Buffer.BlockCopy(textByte, 0, hashStringSalt, 0, textByte.Length);
            Buffer.BlockCopy(salt, 0, hashStringSalt, textByte.Length, salt.Length);
            SHAinstance.ComputeHash(hashStringSalt);
            byte[] ?hashB = SHAinstance.Hash;
                if (hashB != null)
                {
                    return hashB;
                }
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
