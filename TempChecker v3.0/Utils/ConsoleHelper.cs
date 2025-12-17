using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TempChecker.Utils
{
    public static class ConsoleHelper
    {
        public static void DisplayHeader(string title)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(new string('=', title.Length + 4));
            Console.WriteLine($"  {title}  ");
            Console.WriteLine(new string('=', title.Length + 4));
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void DisplaySuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✓ {message}");
            Console.ResetColor();
        }

        public static void DisplayError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"✗ {message}");
            Console.ResetColor();
        }

        public static void DisplayWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"⚠ {message}");
            Console.ResetColor();
        }

        public static void DisplayProgress(string message, int percentage = 0)
        {
            if (percentage > 0)
            {
                Console.Write($"\r{message} [{new string('#', percentage / 5)}{new string('.', 20 - percentage / 5)}] {percentage}%");
            }
            else
            {
                Console.WriteLine($"  {message}");
            }
        }

        public static string ReadLineWithPrompt(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine() ?? string.Empty;
        }
    }
}