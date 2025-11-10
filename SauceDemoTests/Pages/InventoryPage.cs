using OpenQA.Selenium;

namespace SauceDemoTests.Pages
{
    /// <summary>
    /// Página de inventario tras login exitoso.
    /// </summary>
    public class InventoryPage : BasePage
    {
        private readonly By appLogo = By.CssSelector(".app_logo");

        public InventoryPage(IWebDriver driver) : base(driver) { }

        /// <summary>Texto del logo (debe ser "Swag Labs").</summary>
        public string GetLogoText() => GetText(appLogo);
    }
}
