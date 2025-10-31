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

            // Config común del navegador
            currentDriver.Manage().Window.Maximize();
            currentDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(8);
            currentDriver.Manage().Cookies.DeleteAllCookies();


            return currentDriver;
        }

        public void QuitDriver()
        {
            if (currentDriver != null)
            {
                currentDriver.Quit();
                currentDriver.Dispose();
                currentDriver = null;
            }
            else
            {
                Console.WriteLine("else");//no funciona
            }
        }
    }
}
