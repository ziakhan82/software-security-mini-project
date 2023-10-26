using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassManagerMiniPorject
{
    public static class StrogPassword
    {
        // You can customize this function to define your password strength criteria
       public static bool IsStrongPassword(string password)
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
    }

}
