using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using SauceDemoTests.Utils;

namespace SauceDemoTests.Drivers
{

    public sealed class DriverManager
    {
        private static DriverManager? _instance;
        private IWebDriver _currentDriver;


        private DriverManager() { }

        public static DriverManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DriverManager();
                return _instance;
            }
        }

        public IWebDriver CreateDriver(string browserName)
        {
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
