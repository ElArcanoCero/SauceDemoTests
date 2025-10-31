using System;
using FluentAssertions;
using OpenQA.Selenium;
using SauceDemoTests.Drivers;
using SauceDemoTests.Pages;
using SauceDemoTests.Utils;
using Xunit;

namespace SauceDemoTests.Tests
{
    /// <summary>
    /// Pruebas funcionales basadas en los casos de uso del enunciado:
    /// 
    /// UC-1: Sin credenciales -> "Epic sadface: Username is required"
    /// UC-2: Username sin password -> "Epic sadface: Password is required"
    /// UC-3: Credenciales válidas -> Se muestra "Swag Labs"
    /// 
    /// Además:
    /// - Múltiples navegadores ("edge", "firefox")
    /// - Data Provider con [Theory] + [InlineData]
    /// - Page Object Model (LoginPage, InventoryPage)
    /// - Logger (tus logs con timestamps)
    /// - Limpieza del driver al terminar
    /// </summary>
    public class LoginTests : IDisposable
    {
        private IWebDriver _driver;

        /// <summary>
        /// xUnit llama al constructor antes de cada test.
        /// No podemos recibir parámetros aquí desde InlineData,
        /// así que inicializamos algo por defecto. Cada test
        /// volverá a crear el driver con el browser que necesita.
        /// </summary>
        public LoginTests()
        {
            _driver = DriverManager.Instance.CreateDriver("edge");
        }

        /// <summary>
        /// UC-1 y UC-2 juntos en un Data Provider:
        /// - user="" pass="" -> "Username is required"
        /// - user="standard_user" pass="" -> "Password is required"
        /// Probamos tanto en Edge como en Firefox.
        /// </summary>
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

            // Creamos / reutilizamos driver para el browser pedido
            _driver = DriverManager.Instance.CreateDriver(browserName);

            var loginPage = new LoginPage(_driver);
            loginPage.Open();
            loginPage.Login(username, password);

            var actualMessage = loginPage.GetErrorMessage();

            // Aserción con FluentAssertions
            actualMessage.Should().Be(expectedMessage,
                because: "the application should show the proper validation message");

            Logger.Success($"[PASS] Got expected message '{actualMessage}' on {browserName}");
        }

        /// <summary>
        /// UC-3:
        /// Login válido con usuario correcto y password "secret_sauce".
        /// Luego validamos que la página de inventario muestre "Swag Labs".
        /// También lo corremos en Edge y en Firefox.
        /// </summary>
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

            _driver = DriverManager.Instance.CreateDriver(browserName);

            var loginPage = new LoginPage(_driver);
            loginPage.Open();
            loginPage.Login(username, password);

            // Ahora estamos en el inventario
            var inventoryPage = new InventoryPage(_driver);
            var actualTitle = inventoryPage.GetLogoText();

            actualTitle.Should().Be(expectedDashboardTitle,
                because: "successful login should navigate to the Swag Labs inventory page");

            Logger.Success($"[PASS] Logged in on {browserName} and saw '{actualTitle}'");
        }

        /// <summary>
        /// xUnit llama Dispose() después de cada test.
        /// Esto actúa como nuestro TearDown.
        /// Cerramos el driver y limpiamos recursos.
        /// </summary>
        public void Dispose()
        {
            DriverManager.Instance.QuitDriver();
        }
    }
}
