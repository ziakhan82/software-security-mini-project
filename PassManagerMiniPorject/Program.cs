//// See https://aka.ms/new-console-template for more information


using Newtonsoft.Json;
using PassManagerMiniPorject;
using System.Security.Cryptography;
using System.Text;

public class Program
{
    static string storedSalt = null;
    static string storedHashedPassword = null;
    static bool isMasterPasswordSet = false; // Flag to track if the master password is set
    static string encryptionKey = null;

    // Set your file paths
    static string isMasterPasswordSetFilePath = "C:\\Users\\ziaul\\Desktop\\PassMan\\PassManagerMiniPorject\\isMasterPassworddSet.json";
    static string passwordDataFilePath = "C:\\Users\\ziaul\\Desktop\\PassMan\\PassManagerMiniPorject\\passwords.json";

    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Password Manager!");

        // Check if the master password is set
        isMasterPasswordSet = IsMasterPasswordSet();
        if (IsMasterPasswordSet())
        {
            // The master password is already set, so prompt for login
            bool loginSuccessful = false;
            while (!loginSuccessful)
            {
                Console.Write("Enter your master password: ");
                string enteredPassword = Console.ReadLine();

                if (VerifyMasterPassword(enteredPassword))
                {
                    Console.WriteLine("Login successful. Access granted.");
                    encryptionKey = DeriveEncryptionKey(enteredPassword, storedSalt);
                    // Implement password manager features here
                    ShowMenu(); // Call the menu after a successful login
                }
                else
                {
                    Console.WriteLine("Incorrect master password. Access denied.");
                }
            }
        }
        else
        {
            // The master password is not set, so set it
            SetMasterPassword();
        }
    }
    static void ShowMenu()
    {
        if (isMasterPasswordSet)
        {
            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("Press 'q' to quit the program");
                Console.WriteLine("Press 'g' to generate a password for a service");


                char choice = Console.ReadKey().KeyChar;
                Console.WriteLine(); // Move to the next line

                switch (choice)
                {
                    case 'q':
                        // Quit the program
                        Environment.Exit(0);
                        break;
                    case 'g':
                        // Generate a password for a service
                        GeneratePasswordForService();
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        else
        {
            Console.WriteLine("You are not logged in. Please log in to access the menu.");
        }
    }
    static void GeneratePasswordForService()
    {
        if (!isMasterPasswordSet)
        {
            Console.WriteLine("You need to log in to generate a password.");
            return;
        }
        // Implement the password generation logic here, and store it in a file
        // You can prompt the user for service name, user name, and generate a strong password
        Console.Write("Enter the service name: ");
        string serviceName = Console.ReadLine();

        Console.Write("Enter the user name: ");
        string userName = Console.ReadLine();

        // Generate a strong password (you can implement this)
        string generatedPassword = GeneratingPassword.GenerateStrongPassword(12);
        Console.WriteLine("Generated password: " + generatedPassword); // Print the generated password

        // Encrypt and store the generated password
        StoreEncryptedPassword(serviceName, userName, generatedPassword);
      //PasswordEncryption.EncryptPassword(generatedPassword,)

        
    }
    static void StoreEncryptedPassword(string serviceName, string userName, string encryptedPassword)
    {
        // Create a new PasswordEntry and store it
        var entry = new PasswordEntry
        {
            ServiceName = serviceName,
            Username = userName,
            EncryptedPassword = encryptedPassword
        };

        // Call the PasswordService to store the entry
       // PasswordEncryption.EncryptPassword(entry, encryptionKey);
        PasswordService.StorePasswordEntry(entry, encryptionKey);
        Console.WriteLine("Password generated and stored successfully.");
    }
    static void SetMasterPassword()
    {
        Console.Write("Create your master password: ");
        string masterPassword = Console.ReadLine();

        Console.Write("Confirm your master password: ");
        string confirmPassword = Console.ReadLine();

        if (masterPassword != confirmPassword)
        {
            Console.WriteLine("Passwords do not match. Please try again.");
            return;
        }

        if (!IsStrongPassword(masterPassword))
        {
            Console.WriteLine("Your password is not strong enough. Please use a stronger password.");
            return;
        }
       

        string salt = BCrypt.Net.BCrypt.GenerateSalt();
       
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(masterPassword, salt);

        storedSalt = salt;
        storedHashedPassword = hashedPassword;
        isMasterPasswordSet = true;
        SaveIsMasterPasswordSet(true);
        SavePasswordData(salt, hashedPassword);

        Console.WriteLine("Master password set successfully!");
    }
    static string DeriveEncryptionKey(string masterPassword, string salt)
    {
        
        int iterations = 10000; // Adjust the number of iterations as needed
        using (Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(masterPassword, Encoding.UTF8.GetBytes(salt), iterations))
        {
            encryptionKey = Convert.ToBase64String(deriveBytes.GetBytes(256 / 8)); // Generate a 256-bit key
        }
        return encryptionKey;
    }
    static bool VerifyMasterPassword(string enteredPassword)
    {
        if (string.IsNullOrEmpty(enteredPassword))
        {
            Console.WriteLine("Please enter a valid password.");
            return false;
        }

        if (string.IsNullOrEmpty(storedHashedPassword))
        {
            Console.WriteLine("Master password is not set.");
            return false;
        }

        return BCrypt.Net.BCrypt.Verify(enteredPassword, storedHashedPassword);
    }

    static bool IsStrongPassword(string password)
    {
        // Example: Require at least 8 characters and a mix of upper, lower, numbers, and special characters.
        if (password.Length < 8) return false;
        if (!ContainsUpper(password)) return false;
        if (!ContainsLower(password)) return false;
        if (!ContainsNumber(password)) return false;
        if (!ContainsSpecialCharacter(password)) return false;
        return true;
    }

    static bool ContainsUpper(string s) => s.Any(char.IsUpper);
    static bool ContainsLower(string s) => s.Any(char.IsLower);
    static bool ContainsNumber(string s) => s.Any(char.IsDigit);
    static bool ContainsSpecialCharacter(string s) => s.Any(c => !char.IsLetterOrDigit(c));

    static bool IsMasterPasswordSet()
    {
        if (File.Exists(isMasterPasswordSetFilePath))
        {
            string flag = File.ReadAllText(isMasterPasswordSetFilePath);
            if (flag.Trim().Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                // If the flag indicates the master password is set, load the stored password data
                if (File.Exists(passwordDataFilePath))
                {
                    var jsonData = File.ReadAllText(passwordDataFilePath);
                    var passwordData = JsonConvert.DeserializeObject<PasswordData>(jsonData);
                    storedSalt = passwordData.Salt;
                    storedHashedPassword = passwordData.HashedPassword;
                }
                return true;
            }
        }
        return false;
    }

    static void SaveIsMasterPasswordSet(bool value)
    {
        File.WriteAllText(isMasterPasswordSetFilePath, value.ToString());
    }

    static void SavePasswordData(string salt, string hashedPassword)
    {
        var passwordData = new { Salt = salt, HashedPassword = hashedPassword };
        string jsonData = JsonConvert.SerializeObject(passwordData);
        File.WriteAllText(passwordDataFilePath, jsonData);
    }
}