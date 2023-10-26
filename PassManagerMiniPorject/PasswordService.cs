using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassManagerMiniPorject
{
    public class PasswordService
    {
        // Path to the file where service passwords are stored
        private static string passwordDataFilePath = "C:\\Users\\ziaul\\Desktop\\PassMan\\PassManagerMiniPorject\\Servicespasswords.json";

        public static void StorePasswordEntry(PasswordEntry entry, string encryptionKey)
        {
            // Ensure that the encryptionKey is not null or empty
            if (string.IsNullOrEmpty(encryptionKey))
            {
                throw new ArgumentException("Encryption key is missing or invalid.");
            }

            // Encrypt the password before storing it
            entry.EncryptedPassword = PasswordEncryption.EncryptPassword(entry.EncryptedPassword, encryptionKey);

            // Load existing password entries from the file
            List<PasswordEntry> existingEntries = new List<PasswordEntry>();

            if (File.Exists(passwordDataFilePath))
            {
                string jsonData = File.ReadAllText(passwordDataFilePath);
                existingEntries = JsonConvert.DeserializeObject<List<PasswordEntry>>(jsonData);
            }

            // Add the new password entry
            existingEntries.Add(entry);

            // Serialize the updated list of entries to JSON
            string updatedJson = JsonConvert.SerializeObject(existingEntries);

            // Write the JSON back to the file
            File.WriteAllText(passwordDataFilePath, updatedJson);
        }


    }
}