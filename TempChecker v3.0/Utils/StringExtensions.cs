using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempChecker.Utils
{
    public static class StringExtensions
    {
        public static bool IsNumeric(this string input)
        {
            return !string.IsNullOrWhiteSpace(input) && input.All(char.IsDigit);
        }

        public static string PadMeltingNumber(this int number, int length = 9)
        {
            return number.ToString($"D{length}");
        }

        public static string Truncate(this string input, int maxLength)
        {
            if (string.IsNullOrEmpty(input) || input.Length <= maxLength)
                return input;

            return input.Substring(0, maxLength - 3) + "...";
        }
    }
}