using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PassManagerMiniPorject
{
    public static class PasswordEncryption
    {
        public static string EncryptPassword(string password, string encryptionKey)
        {
            // Use encryptionKey to perform encryption
            // Example: AES encryption
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Convert.FromBase64String(encryptionKey);
                aesAlg.IV = new byte[16]; // Initialization Vector (IV) should also be generated securely

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(password);
                        }
                    }

                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }
    }
}

        //public static string DecryptPassword(string encryptedPassword, string encryptionKey)
        //{
        //    // Ensure the encryption key is not null or empty
        //    if (string.IsNullOrEmpty(encryptionKey))
        //    {
        //        throw new ArgumentException("Encryption key is missing or invalid.");
        //    }

        //    // Use encryptionKey to perform decryption
        //    using (Aes aesAlg = Aes.Create())
        //    {
        //        aesAlg.Key = Convert.FromBase64String(encryptionKey);
        //        aesAlg.IV = new byte[16]; // Use the same IV used for encryption

        //        using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(encryptedPassword)))
        //        {
        //            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aesAlg.CreateDecryptor(), CryptoStreamMode.Read))
        //            {
        //                using (StreamReader streamReader = new StreamReader(cryptoStream))
        //                {
        //                    return streamReader.ReadToEnd();
        //                }
        //            }
        //        }
        //    }
        //}
        //    public static string DecryptPassword(string encryptedPassword, string encryptionKey)
        //    {
        //        // Use encryptionKey to perform decryption
        //        // Example: AES decryption
        //        using (Aes aesAlg = Aes.Create())
        //        {
        //            aesAlg.Key = Convert.FromBase64String(encryptionKey);
        //            aesAlg.IV = new byte[16]; // Use the same IV used for encryption

        //            using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(encryptedPassword)))
        //            {
        //                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aesAlg.CreateDecryptor(), CryptoStreamMode.Read))
        //                {
        //                    using (StreamReader streamReader = new StreamReader(cryptoStream))
        //                    {
        //                        return streamReader.ReadToEnd();
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
    
