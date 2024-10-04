using System.Security.Cryptography;
using System.Text;

namespace UserWebApi
{
    public class StringEncryptor
    {
        private readonly string _encryptionKey = string.Empty;

        public StringEncryptor(string encryptionKey)
        {
            if (string.IsNullOrEmpty(encryptionKey) || (encryptionKey.Length != 16 && encryptionKey.Length != 24 && encryptionKey.Length != 32))
            {
                throw new ArgumentException("The encryption key should be 16. 24. or 32 character long.");
            }
            _encryptionKey = encryptionKey;
        }

        public string Encrypt(string plainText)
        {
            using var aesAlg = Aes.Create();
            aesAlg.Key = Encoding.UTF8.GetBytes(_encryptionKey);
            aesAlg.GenerateIV();
            var iv = aesAlg.IV;

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, iv);

            using var ms = new MemoryStream();

            ms.Write(iv, 0, iv.Length);

            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            {
                using var sw = new StreamWriter(cs);
                {
                    sw.Write(plainText);
                }
            }

            return Convert.ToBase64String(ms.ToArray());
        }
    }

}
