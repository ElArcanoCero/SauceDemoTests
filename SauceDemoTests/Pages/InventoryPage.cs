using OpenQA.Selenium;

namespace SauceDemoTests.Pages
{
    /// <summary>
    /// Page Object que representa la página principal de inventario
    /// que aparece después del login exitoso en https://www.saucedemo.com/.
    /// Se usa en el caso UC-3 para validar el texto "Swag Labs".
    /// </summary>
    public class InventoryPage
    {
        private readonly IWebDriver _driver;

        // Locator del título/logo que aparece en la parte superior izquierda
        // Ejemplo: <div class="app_logo">Swag Labs</div>
        private readonly By _appLogo = By.CssSelector(".app_logo");

        /// <summary>
        /// Constructor: recibe la instancia del WebDriver que ya está logueada.
        /// </summary>
        public InventoryPage(IWebDriver driver)
        {
            _driver = driver;
        }

        /// <summary>
        /// Devuelve el texto del logo/título del dashboard.
        /// Ejemplo esperado: "Swag Labs".
        /// </summary>
        public string GetLogoText()
        {
            return _driver.FindElement(_appLogo).Text.Trim();
        }
    }
}
