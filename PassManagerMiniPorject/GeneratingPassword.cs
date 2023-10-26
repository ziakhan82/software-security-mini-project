using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PassManagerMiniPorject
{
    public static class GeneratingPassword
    {
        public static string GenerateStrongPassword(int length)
        {
            const string uppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowercaseChars = "abcdefghijklmnopqrstuvwxyz";
            const string numericChars = "0123456789";
            const string specialChars = "!@#$%^&*";

            string allChars = uppercaseChars + lowercaseChars + numericChars + specialChars;
            StringBuilder password = new StringBuilder();

            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(0, allChars.Length);
                password.Append(allChars[index]);
            }

            return password.ToString();
        }
    }
}