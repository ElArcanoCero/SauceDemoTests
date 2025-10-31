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
        private IWebDriver? _currentDriver;

        public static DriverManager Instance => _instance ??= new DriverManager();

        private DriverManager() { }

        public IWebDriver CreateDriver(string browserName)
        {
            // Cierra si había uno previo
            QuitDriver();

            Logger.Log($"[DriverManager] Creating driver for browser '{browserName}'");

            switch (browserName.ToLower())
            {
                case "edge":
                    _currentDriver = new EdgeDriver();
                    Logger.Success("[DriverManager] EdgeDriver initialized.");
                    break;

                case "firefox":
                    _currentDriver = new FirefoxDriver();
                    Logger.Success("[DriverManager] FirefoxDriver initialized.");
                    break;

                default:
                    Logger.Error($"[DriverManager] Unsupported browser requested: {browserName}");
                    throw new ArgumentException($"Unsupported browser: {browserName}");
            }

            // Config común del navegador
            _currentDriver.Manage().Window.Maximize();
            _currentDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            _currentDriver.Manage().Cookies.DeleteAllCookies();

            Logger.Log("[DriverManager] Driver configured: window maximized, implicit wait = 5s, cookies cleared.");

            return _currentDriver;
        }

        public void QuitDriver()
        {
            if (_currentDriver != null)
            {
                Logger.Log("[DriverManager] Quitting and disposing current driver.");
                _currentDriver.Quit();
                _currentDriver.Dispose();
                _currentDriver = null;
                Logger.Log("[DriverManager] Driver disposed successfully.");
            }
            else
            {
                Logger.Log("[DriverManager] QuitDriver called, but no active driver to close.");
            }
        }
    }
}
