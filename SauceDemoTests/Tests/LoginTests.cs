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
        private IWebDriver driver;

        public LoginTests()
        {
            driver = DriverManager.Instance.CreateDriver("edge");
        }

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
            Logger.Log($"[START] InvalidLogin on {browserName} with user='{username}' pass='{password}'");

            driver = DriverManager.Instance.CreateDriver(browserName);

            var loginPage = new LoginPage(driver);
            loginPage.Open();
            loginPage.Login(username, password);

            var actualMessage = loginPage.GetErrorMessage();

            actualMessage.Should().Be(expectedMessage,
                because: "the application should show the proper validation message");

            Logger.Success($"[PASS] Got expected message '{actualMessage}' on {browserName}");
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
            Logger.Log($"[START] ValidLogin on {browserName} with user='{username}'");

            driver = DriverManager.Instance.CreateDriver(browserName);

            var loginPage = new LoginPage(driver);
            loginPage.Open();
            loginPage.Login(username, password);

            var inventoryPage = new InventoryPage(driver);
            var actualTitle = inventoryPage.GetLogoText();

            actualTitle.Should().Be(expectedDashboardTitle,
                because: "successful login should navigate to the Swag Labs inventory page");

            Logger.Success($"[PASS] Logged in on {browserName} and saw '{actualTitle}'");
        }

        public void Dispose()
        {
            DriverManager.Instance.QuitDriver();
        }
    }
}
