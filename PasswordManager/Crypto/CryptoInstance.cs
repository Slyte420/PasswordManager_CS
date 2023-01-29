using System.Security.Cryptography;
using System.Text;

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
            SHAinstance = SHA256.Create();
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
        public void reset()
        {
            RSAInstance.Clear();
            AESInstance.Clear();
            SHAinstance.Clear();
            instance = null;
        }
        public byte[]? hashString(string text)
        {
            if (SHAinstance is not null)
            {
                byte[] textByte = Encoding.UTF8.GetBytes(text);
                SHAinstance.ComputeHash(textByte);
                byte[]? hashB = SHAinstance.Hash;
                if (hashB != null)
                {
                    return hashB;
                }
            }
            return null;
        }
        public void setAesFromPasswordStringForPasswords(string password, byte[] salt, byte[]? IV = null)
        {
            using (Rfc2898DeriveBytes instance = new Rfc2898DeriveBytes(password, salt))
            {
                int keyLength = AESInstance.KeySize / 8;
                AESInstance.Key = instance.GetBytes(keyLength);
                if (IV == null)
                {
                    AESInstance.GenerateIV();
                }
                else
                {
                    AESInstance.IV = IV;
                }
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
            byte[] encryptedIV = RSAInstance.Encrypt(IV, RSAPadding);
            return encryptedIV;
        }
        public byte[] RSADecrypted(byte[] data)
        {
            byte[] decryptedData = RSAInstance.Decrypt(data, RSAPadding);
            return decryptedData;
        }
        public void placeRSAEncryptedFile(string username, string selectedFolder, byte[] passwordHash)
        {
            string fileName = username + ".key";
            string filePath = Path.Combine(selectedFolder, fileName);
            ICryptoTransform transform = AESInstance.CreateEncryptor();
            using (var outF = new FileStream(filePath, FileMode.Create))
            {
                outF.Write(BitConverter.GetBytes(passwordHash.Length), 0, 4);
                outF.Write(passwordHash, 0, passwordHash.Length);
                using (var outStreamEncrypted = new CryptoStream(outF, transform, CryptoStreamMode.Write))
                {
                    outStreamEncrypted.Write(Encoding.UTF8.GetBytes(RSAInstance.ToXmlString(true)));
                    outStreamEncrypted.FlushFinalBlock();
                }
            }
        }
        public bool setRSAwithEncryptedFile(string selectedFile, byte[] passwordHash)
        {
            byte[] data;
            ICryptoTransform transform = AESInstance.CreateDecryptor();
            using (var inF = new FileStream(selectedFile, FileMode.Open))
            {

                byte[] phByte = new byte[4];
                inF.Read(phByte, 0, phByte.Length);
                int passwordHashLength = BitConverter.ToInt32(phByte);
                byte[] currentPasswordHash = new byte[passwordHashLength];
                inF.Read(currentPasswordHash, 0, passwordHashLength);
                if (!currentPasswordHash.SequenceEqual(passwordHash))
                {
                    return false;
                }
                int indexC = 4 + passwordHashLength;
                int lengthC = (int)inF.Length - indexC;
                data = new byte[lengthC];
                using (var inDecryptor = new CryptoStream(inF, transform, CryptoStreamMode.Read))
                {
                    int count = 0;
                    do
                    {
                        count = inDecryptor.Read(data, count, lengthC - count);
                    } while (count > 0);
                }

            }
            if (data.Length <= 0)
            {
                return false;
            }
            string xmlText = Encoding.UTF8.GetString(data).Replace("\0", String.Empty);
            RSAInstance.FromXmlString(xmlText);
            return true;
        }
        public byte[]? getPrivateKeyHash(byte[] salt)
        {

            byte[] privateKeySalt = new byte[RSAInstance.ExportRSAPrivateKey().Length + salt.Length];
            Buffer.BlockCopy(RSAInstance.ExportRSAPrivateKey(), 0, privateKeySalt, 0, RSAInstance.ExportRSAPrivateKey().Length);
            Buffer.BlockCopy(salt, 0, privateKeySalt, RSAInstance.ExportRSAPrivateKey().Length, salt.Length);
            SHAinstance.ComputeHash(privateKeySalt);
            return SHAinstance.Hash;
        }

        public byte[]? hashStringSalt(string text, byte[] salt)
        {
            if (SHAinstance is not null)
            {
                byte[] textByte = Encoding.UTF8.GetBytes(text);
                byte[] hashStringSalt = new byte[textByte.Length + salt.Length];
                Buffer.BlockCopy(textByte, 0, hashStringSalt, 0, textByte.Length);
                Buffer.BlockCopy(salt, 0, hashStringSalt, textByte.Length, salt.Length);
                SHAinstance.ComputeHash(hashStringSalt);
                byte[]? hashB = SHAinstance.Hash;
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
        public string encryptString(string text)
        {
            byte[] encryptedBytes;
            ICryptoTransform cryptoTransform = AESInstance.CreateEncryptor();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream StreamEncrypted = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(StreamEncrypted))
                    {
                        streamWriter.Write(text);
                    }
                }
                encryptedBytes = memoryStream.ToArray();
            }
            return Convert.ToBase64String(encryptedBytes);
        }
        public string decryptString(string text)
        {
            ICryptoTransform cryptoTransform = AESInstance.CreateDecryptor();
            byte[] encryptedBytes = Convert.FromBase64String(text);
            string decryptedString;
            using (MemoryStream memoryStream = new MemoryStream(encryptedBytes))
            {
                using (CryptoStream StreamDecrypted = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader(StreamDecrypted))
                    {
                        decryptedString = streamReader.ReadToEnd();
                    }
                }
                return decryptedString;
            }
        }
    }
}
