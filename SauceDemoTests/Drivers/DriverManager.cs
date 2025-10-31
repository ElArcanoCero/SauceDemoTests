using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using SauceDemoTests.Utils;

namespace SauceDemoTests.Drivers
{
    /// <summary>
    /// DriverManager implementa el patrón Singleton y Factory.
    /// Se encarga de crear, configurar y limpiar las instancias de WebDriver
    /// para los navegadores Edge y Firefox.
    /// </summary>
    public sealed class DriverManager
    {
        private static DriverManager _instance;
        private IWebDriver _currentDriver;

        /// <summary>
        /// Constructor privado: asegura la implementación Singleton.
        /// </summary>
        private DriverManager() { }

        /// <summary>
        /// Instancia única del DriverManager.
        /// </summary>
        public static DriverManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DriverManager();
                return _instance;
            }
        }

        /// <summary>
        /// Crea y configura un nuevo WebDriver para el navegador especificado.
        /// Implementa el patrón Factory para soportar múltiples navegadores.
        /// </summary>
        /// <param name="browserName">Nombre del navegador: "edge" o "firefox".</param>
        /// <returns>Instancia configurada de IWebDriver.</returns>
        public IWebDriver CreateDriver(string browserName)
        {
            // Si ya existe un driver activo, se reutiliza
            if (_currentDriver != null)
                return _currentDriver;

            try
            {
                switch (browserName.ToLower())
                {
                    case "edge":
                        var edgeOptions = new EdgeOptions();
                        _currentDriver = new EdgeDriver(edgeOptions);
                        break;

                    case "firefox":
                        var firefoxOptions = new FirefoxOptions();
                        _currentDriver = new FirefoxDriver(firefoxOptions);
                        break;

                    default:
                        throw new ArgumentException($"Unsupported browser: {browserName}");
                }

                // Configuración general
                _currentDriver.Manage().Window.Maximize();
                _currentDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                Logger.Log($"✅ {browserName} driver initialized successfully.");
            }
            catch (Exception ex)
            {
                Logger.Log($"❌ Failed to initialize {browserName} driver: {ex.Message}");
                throw;
            }

            return _currentDriver;
        }

        /// <summary>
        /// Cierra el navegador, limpia cookies y libera el recurso WebDriver.
        /// </summary>
        public void QuitDriver()
        {
            if (_currentDriver != null)
            {
                try
                {
                    _currentDriver.Manage().Cookies.DeleteAllCookies();
                    _currentDriver.Quit();
                    Logger.Log("🧹 Driver closed and resources cleaned up.");
                }
                catch (Exception ex)
                {
                    Logger.Log($"⚠️ Error during driver cleanup: {ex.Message}");
                }
                finally
                {
                    _currentDriver = null;
                }
            }
        }
    }
}
