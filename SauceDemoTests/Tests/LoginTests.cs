using System;
using FluentAssertions;
using OpenQA.Selenium;
using SauceDemoTests.Drivers;
using SauceDemoTests.Pages;
using SauceDemoTests.Utils;
using Xunit;

namespace SauceDemoTests.Tests
{
    public class LoginTests : IDisposable
    {
        private readonly DriverManager driverManager = DriverManager.Instance;
        private IWebDriver? driver;

        [Theory]

        [InlineData("edge", "", "", "Epic sadface: Username is required")]
        [InlineData("firefox", "nombre", "clave", "Epic sadface: Username is required")]

        [InlineData("edge", "nombre", "", "Epic sadface: Password is required")]
        [InlineData("firefox", "nombre", "clave2", "Epic sadface: Password is required")]

        [InlineData("edge", "standard_user", "secret_sauce", "Swag Labs")]
        [InlineData("firefox", "standard_user", "secret_sauce", "Swag Labs")]
        public void Login_Tests(string browserName, string username, string password, string expected)
        {
            Logger.Log($"[TEST] Starting Login_Tests on {browserName} " +
                       $"with username='{username}', password='{password}'");

            driver = driverManager.CreateDriver(browserName);

            var loginPage = new LoginPage(driver);
            loginPage.Open();
            Logger.Log("[TEST] Login page opened.");

            if (expected.Contains("Username is required", StringComparison.OrdinalIgnoreCase))
            {
                Logger.Log("[TEST] Executing UC-1 (empty credentials after clear).");
                loginPage.LoginEmptyCredentials_UC1(username, password);

                var actualMessage = loginPage.GetErrorMessage();
                Logger.Log($"[TEST] Error message from UI: '{actualMessage}'");
                actualMessage.Should().Be(expected);
                Logger.Success("[TEST] UC-1 assertion passed.");
            }
            else if (expected.Contains("Password is required", StringComparison.OrdinalIgnoreCase))
            {
                Logger.Log("[TEST] Executing UC-2 (missing password).");
                loginPage.LoginMissingPassword_UC2(username, password);

                var actualMessage = loginPage.GetErrorMessage();
                Logger.Log($"[TEST] Error message from UI: '{actualMessage}'");
                actualMessage.Should().Be(expected);
                Logger.Success("[TEST] UC-2 assertion passed.");
            }
            else
            {
                Logger.Log("[TEST] Executing UC-3 (valid login).");
                loginPage.Login(username, password);

                var inventoryPage = new InventoryPage(driver);
                var actualTitle = inventoryPage.GetLogoText();
                Logger.Log($"[TEST] Inventory page title: '{actualTitle}'");
                actualTitle.Should().Be(expected);
                Logger.Success("[TEST] UC-3 assertion passed.");
            }
        }

        public void Dispose()
        {
            Logger.Log("[TEST] Disposing test. Closing browser.");
            driverManager.QuitDriver();
        }
    }
}

