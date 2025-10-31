using OpenQA.Selenium;

namespace SauceDemoTests.Pages
{

    public class InventoryPage
    {
        private readonly IWebDriver _driver;

        private readonly By _appLogo = By.CssSelector(".app_logo");

        public InventoryPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public string GetLogoText()
        {
            return _driver.FindElement(_appLogo).Text.Trim();
        }
    }
}
