using System;

namespace SauceDemoTests.Utils
{
    public static class Logger
    {
        private static readonly object _lock = new object();

        public static void Log(string message)
        {
            lock (_lock)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {message}");
                Console.ResetColor();
            }
        }

        public static void Error(string errorMessage)
        {
            lock (_lock)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] ERROR: {errorMessage}");
                Console.ResetColor();
            }
        }

        public static void Success(string successMessage)
        {
            lock (_lock)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {successMessage}");
                Console.ResetColor();
            }
        }
    }
}
