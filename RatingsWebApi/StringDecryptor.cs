using System.Security.Cryptography;
using System.Text;

namespace RatingsWebApi
{
    public class StringDecryptor
    {
        private readonly string _encryptionKey;
        public StringDecryptor(string encryptionKey)
        {
            if (string.IsNullOrEmpty(encryptionKey) || (encryptionKey.Length != 16 && encryptionKey.Length != 24 && encryptionKey.Length != 32))
            {
                throw new ArgumentException("The encryption key should be 16. 24. or 32 character long.");
            }
            _encryptionKey = encryptionKey;
        }

        public string Decrypt(string cipherText)
        {   
            var fullCipher = Convert.FromBase64String(cipherText); 
            
            using var aesAlg = Aes.Create();

            var iv = new byte[16];

            Array.Copy(fullCipher, 0, iv, 0, iv.Length);

            var cipherBytes = new byte[fullCipher.Length - iv.Length];
            Array.Copy(fullCipher, iv.Length, cipherBytes, 0, cipherBytes.Length);

            aesAlg.Key = Encoding.UTF8.GetBytes(_encryptionKey); 
            aesAlg.IV = iv;

            var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using var msDecrypt = new MemoryStream(cipherBytes);
            using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);  
            using var srDecrypt = new StreamReader(csDecrypt);
            
            return srDecrypt.ReadToEnd();

        }
    }
}
