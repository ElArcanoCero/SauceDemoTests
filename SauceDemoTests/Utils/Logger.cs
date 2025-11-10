namespace SauceDemoTests.Utils
{
    public static class Logger
    {
        private static readonly object Lock = new object();

        /// <summary>Mensaje informativo</summary>
        public static void Log(string message)
        {
            lock (Lock)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {message}");
                Console.ResetColor();
            }
        }
        /// <summary>Mensaje de error</summary>
        public static void Error(string errorMessage)
        {
            lock (Lock)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] ERROR: {errorMessage}");
                Console.ResetColor();
            }
        }

        /// <summary>Mensaje de ejecucion exitosa</summary>
        public static void Success(string successMessage)
        {
            lock (Lock)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {successMessage}");
                Console.ResetColor();
            }
        }
    }
}
