using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace SauceDemoTests.Drivers
{
    /// <summary>
    /// Administra la vida del WebDriver (Singleton)
    /// Crea driver por navegador y garantiza cierre limpio tras cada test.
    /// </summary>
    public sealed class DriverManager
    {
        private static DriverManager? instance;
        private IWebDriver? currentDriver;

        /// <summary>Instancia única</summary>
        public static DriverManager Instance => instance ??= new DriverManager();

        private DriverManager() { }

        /// <summary>
        /// Crea un nuevo driver para el navegador solicitado
        /// aplica configuración común y limpia cookies.
        /// </summary>
        public IWebDriver CreateDriver(string browserName)
        {
            QuitDriver();

            switch (browserName.ToLower())
            {
                case "edge":
                    currentDriver = new EdgeDriver();
                    break;
                case "firefox":
                    currentDriver = new FirefoxDriver();
                    break;
                default:
                    throw new ArgumentException($"Unsupported browser: {browserName}");
            }

            currentDriver.Manage().Window.Maximize();
            currentDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(8);
            currentDriver.Manage().Cookies.DeleteAllCookies();
            return currentDriver;
        }

        /// <summary>Cierra y dispone el driver actual</summary>
        public void QuitDriver()
        {
            if (currentDriver == null) return;
            try
            {
                currentDriver.Quit();
                currentDriver.Dispose();
            }
            finally
            {
                currentDriver = null;
            }
        }
    }
}

