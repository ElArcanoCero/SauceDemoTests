using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using SauceDemoTests.Utils;

namespace SauceDemoTests.Drivers
{
    public sealed class DriverManager
    {
        private static DriverManager? instance;
        private IWebDriver? currentDriver;

        public static DriverManager Instance => instance ??= new DriverManager();

        private DriverManager() { }

        public IWebDriver CreateDriver(string browserName)
        {
            QuitDriver();

            Logger.Log($"[DriverManager] Creating driver for browser '{browserName}'");

            switch (browserName.ToLower())
            {
                case "edge":
                    currentDriver = new EdgeDriver();
                    Logger.Success("[DriverManager] EdgeDriver initialized.");
                    break;

                case "firefox":
                    currentDriver = new FirefoxDriver();
                    Logger.Success("[DriverManager] FirefoxDriver initialized.");
                    break;

                default:
                    Logger.Error($"[DriverManager] Unsupported browser requested: {browserName}");
                    throw new ArgumentException($"Unsupported browser: {browserName}");
            }

            // Config común del navegador
            currentDriver.Manage().Window.Maximize();
            currentDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            currentDriver.Manage().Cookies.DeleteAllCookies();

            Logger.Log("[DriverManager] Driver configured: window maximized, implicit wait = 5s, cookies cleared.");

            return currentDriver;
        }

        public void QuitDriver()
        {
            if (currentDriver != null)
            {
                Logger.Log("[DriverManager] Quitting and disposing current driver.");
                currentDriver.Quit();
                currentDriver.Dispose();
                currentDriver = null;
                Logger.Log("[DriverManager] Driver disposed successfully.");
            }
            else
            {
                Logger.Log("[DriverManager] QuitDriver called, but no active driver to close.");
            }
        }
    }
}
