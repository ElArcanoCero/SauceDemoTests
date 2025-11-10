using FluentAssertions;
using OpenQA.Selenium;
using SauceDemoTests.Drivers;
using SauceDemoTests.Pages;
using SauceDemoTests.Utils;
using Xunit;

namespace SauceDemoTests.Tests
{
    /// <summary>
    /// Suite de pruebas de login (UC-1, UC-2, UC-3), ejecutadas en Edge y Firefox.
    /// Cada test es atómico y valida una sola cosa.
    /// </summary>
    public class LoginTests : IDisposable
    {
        private readonly DriverManager driverManager = DriverManager.Instance;
        private IWebDriver? driver;

        // UC-1: Username is required
        [Theory]
        [InlineData("edge", "nombre", "clave")]
        [InlineData("firefox", "nombre", "clave")]
        public void LoginEmptyCredentials_UC1(string browser, string user, string pass)
        {
            Logger.Log($"[UC-1] Browser: {browser}");
            driver = driverManager.CreateDriver(browser);

            var loginPage = new LoginPage(driver);
            loginPage.Open();

            loginPage.LoginEmptyCredentials_UC1(user, pass);

            var actual = loginPage.GetErrorMessage();
            actual.Should().Be("Epic sadface: Username is required");
        }

        // UC-2: Password is required
        [Theory]
        [InlineData("edge", "nombre", "clave")]
        [InlineData("firefox", "nombre", "clave")]
        public void LoginMissingPassword_UC2(string browser, string user, string tmpPass)
        {
            Logger.Log($"[UC-2] Browser: {browser}");
            driver = driverManager.CreateDriver(browser);

            var loginPage = new LoginPage(driver);
            loginPage.Open();

            loginPage.LoginMissingPassword_UC2(user, tmpPass);

            var actual = loginPage.GetErrorMessage();
            actual.Should().Be("Epic sadface: Password is required");
        }

        // UC-3: Login válido
        [Theory]
        [InlineData("edge", "standard_user", "secret_sauce")]
        [InlineData("firefox", "standard_user", "secret_sauce")]
        public void Login_CU3(string browser, string user, string pass)
        {
            Logger.Log($"[UC-3] Browser: {browser}");
            driver = driverManager.CreateDriver(browser);

            var loginPage = new LoginPage(driver);
            loginPage.Open();

            loginPage.Login(user, pass);

            var inventory = new InventoryPage(driver);
            inventory.GetLogoText().Should().Be("Swag Labs");
        }

        /// <summary>Limpia el driver después de cada test.</summary>
        public void Dispose()
        {
            driverManager.QuitDriver();
        }
    }
}


