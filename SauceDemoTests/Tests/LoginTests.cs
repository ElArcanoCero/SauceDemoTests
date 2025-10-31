using FluentAssertions;
using OpenQA.Selenium;
using SauceDemoTests.Drivers;
using SauceDemoTests.Pages;
using SauceDemoTests.Utils;
using System;
using Xunit;

namespace SauceDemoTests.Tests
{
    public class LoginTests : IDisposable
    {
        private readonly DriverManager driverManager = DriverManager.Instance;
        private IWebDriver? driver;

        [Theory]
        // esto puede volverse un ciclo para mejorar?
        [InlineData("edge", "nombre", "clave", "Epic sadface: Username is required")]
        [InlineData("firefox", "nombre", "clave", "Epic sadface: Username is required")]

        [InlineData("edge", "nombre", "clave", "Epic sadface: Password is required")]
        [InlineData("firefox", "nombre", "clave", "Epic sadface: Password is required")]

        [InlineData("edge", "standard_user", "secret_sauce", "Swag Labs")]
        [InlineData("firefox", "standard_user", "secret_sauce", "Swag Labs")]
        public void Login_Tests(string browserName, string username, string password, string expected)
        {

            driver = driverManager.CreateDriver(browserName);

            var loginPage = new LoginPage(driver);
            loginPage.Open();

            if (expected.Contains("Username is required", StringComparison.OrdinalIgnoreCase))
            {
                Logger.Log("caso 1");

                loginPage.LoginEmptyCredentials_UC1(username, password);

                var actualMessage = loginPage.GetErrorMessage();
                actualMessage.Should().Be(expected);
            }
            else if (expected.Contains("Password is required", StringComparison.OrdinalIgnoreCase))
            {
                Logger.Log("caso 2");

                loginPage.LoginMissingPassword_UC2(username, password);

                var actualMessage = loginPage.GetErrorMessage();
                actualMessage.Should().Be(expected);
            }
            else
            {

                Logger.Log("caso 3");

                loginPage.Login(username, password);

                var inventoryPage = new InventoryPage(driver);
                var actualTitle = inventoryPage.GetLogoText();
                actualTitle.Should().Be(expected);

            }
        }

        public void Dispose()
        {
            driverManager.QuitDriver();
        }
    }
}

