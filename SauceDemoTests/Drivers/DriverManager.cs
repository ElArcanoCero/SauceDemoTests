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


        private DriverManager() { }

        public static DriverManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new DriverManager();
                return instance;
            }
        }

        public IWebDriver CreateDriver(string browserName)
        {
            if (currentDriver != null)
                return currentDriver;

            try
            {
                switch (browserName.ToLower())
                {
                    case "edge":
                        var edgeOptions = new EdgeOptions();
                        currentDriver = new EdgeDriver(edgeOptions);
                        break;

                    case "firefox":
                        var firefoxOptions = new FirefoxOptions();
                        currentDriver = new FirefoxDriver(firefoxOptions);
                        break;

                    default:
                        throw new ArgumentException($"Unsupported browser: {browserName}");
                }

                currentDriver.Manage().Window.Maximize();
                currentDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                Logger.Log($"{browserName} driver initialized successfully.");
            }
            catch (Exception ex)
            {
                Logger.Log($"Failed to initialize {browserName} driver: {ex.Message}");
                throw;
            }

            return currentDriver;
        }

        public void QuitDriver()
        {
            if (currentDriver != null)
            {
                try
                {
                    currentDriver.Manage().Cookies.DeleteAllCookies();
                    currentDriver.Quit();
                    Logger.Log("Driver closed and resources cleaned up.");
                }
                catch (Exception ex)
                {
                    Logger.Log($"Error during driver cleanup: {ex.Message}");
                }
                finally
                {
                    currentDriver = null;
                }
            }
        }
    }
}
