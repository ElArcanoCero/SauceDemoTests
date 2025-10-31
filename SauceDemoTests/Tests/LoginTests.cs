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
        private readonly DriverManager _driverManager = DriverManager.Instance;
        private IWebDriver? _driver;

        [Theory]
        [InlineData("edge", "", "", "Epic sadface: Username is required")]
        [InlineData("firefox", "", "", "Epic sadface: Username is required")]
        [InlineData("edge", "standard_user", "", "Epic sadface: Password is required")]
        [InlineData("firefox", "standard_user", "", "Epic sadface: Password is required")]
        public void InvalidLogin_ShowsExpectedErrorMessage(
            string browserName,
            string username,
            string password,
            string expectedMessage)
        {
            Logger.Log($"[TEST] Starting InvalidLogin_ShowsExpectedErrorMessage on {browserName} " +
                       $"with user='{username}', pass='{password}'");

            _driver = _driverManager.CreateDriver(browserName);

            var loginPage = new LoginPage(_driver);
            loginPage.Open();
            Logger.Log("[TEST] Login page opened.");

            loginPage.Login(username, password);
            Logger.Log("[TEST] Login attempted.");

            var actualMessage = loginPage.GetErrorMessage();
            Logger.Log($"[TEST] Error message from UI: '{actualMessage}'");

            actualMessage.Should().Be(expectedMessage,
                because: "the application should display the proper validation message");

            Logger.Success("[TEST] Assertion passed for InvalidLogin_ShowsExpectedErrorMessage.");
        }

        [Theory]
        [InlineData("edge", "standard_user", "secret_sauce", "Swag Labs")]
        [InlineData("firefox", "standard_user", "secret_sauce", "Swag Labs")]
        public void ValidLogin_ShowsSwagLabsTitle(
            string browserName,
            string username,
            string password,
            string expectedDashboardTitle)
        {
            Logger.Log($"[TEST] Starting ValidLogin_ShowsSwagLabsTitle on {browserName} " +
                       $"with user='{username}'");

            _driver = _driverManager.CreateDriver(browserName);

            var loginPage = new LoginPage(_driver);
            loginPage.Open();
            Logger.Log("[TEST] Login page opened.");

            loginPage.Login(username, password);
            Logger.Log("[TEST] Login submitted.");

            var inventoryPage = new InventoryPage(_driver);
            var actualTitle = inventoryPage.GetLogoText();
            Logger.Log($"[TEST] Inventory page title is '{actualTitle}'");

            actualTitle.Should().Be(expectedDashboardTitle,
                because: "successful login should navigate to inventory page labeled 'Swag Labs'");

            Logger.Success("[TEST] Assertion passed for ValidLogin_ShowsSwagLabsTitle.");
        }

        public void Dispose()
        {
            Logger.Log("[TEST] Disposing test context, closing driver.");
            _driverManager.QuitDriver();
        }
    }
}
