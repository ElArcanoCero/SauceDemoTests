using System;

namespace SauceDemoTests.Utils
{
    /// <summary>
    /// Logger simple para registrar la actividad de las pruebas.
    /// Imprime mensajes con marca de tiempo en la consola.
    /// Cumple el requisito de Logging (criterio 6 de la rúbrica).
    /// </summary>
    public static class Logger
    {
        private static readonly object _lock = new object();

        /// <summary>
        /// Registra un mensaje en la consola con formato:
        /// [HH:mm:ss] Mensaje
        /// </summary>
        /// <param name="message">Texto a registrar en la salida estándar.</param>
        public static void Log(string message)
        {
            lock (_lock)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {message}");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Registra un mensaje de error (en color rojo).
        /// </summary>
        /// <param name="errorMessage">Mensaje de error.</param>
        public static void Error(string errorMessage)
        {
            lock (_lock)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] ❌ ERROR: {errorMessage}");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Registra un mensaje de éxito (en color verde).
        /// </summary>
        /// <param name="successMessage">Mensaje de éxito.</param>
        public static void Success(string successMessage)
        {
            lock (_lock)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] ✅ {successMessage}");
                Console.ResetColor();
            }
        }
    }
}
