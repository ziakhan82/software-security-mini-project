using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassManagerMiniPorject
{
    public static class SetMasterPassword
    {
        static string storedSalt;
        static string storedHashedPassword;
        public static void SetTheMasterPassword() { 
        {
            // Prompt the user to set their master password
            Console.Write("Create your master password: ");
            string masterPassword = Console.ReadLine();

        // Prompt the user to confirm the master password
        Console.Write("Confirm your master password: ");
            string confirmPassword = Console.ReadLine();

            // Check if the passwords match
            if (masterPassword != confirmPassword)
            {
                Console.WriteLine("Passwords do not match. Please try again.");
                return;
            }

            // Check password strength (you can customize this function)
            if (!StrogPassword.IsStrongPassword(masterPassword))
            {
                Console.WriteLine("Your password is not strong enough. Please use a stronger password.");
                return;
            }

             // Generate a salt
            string salt = BCrypt.Net.BCrypt.GenerateSalt();

            // Hash the master password with the generated salt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(masterPassword, salt);

           // Store the salt and hashed password securely (in memory)
            storedSalt = salt;
            storedHashedPassword = hashedPassword;

             Console.WriteLine("Master password set successfully!");

            // Now you can implement the access control logic with the storedSalt and storedHashedPassword
        }

        }
    }
}
