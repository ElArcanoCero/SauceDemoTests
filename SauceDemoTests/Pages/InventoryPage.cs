using OpenQA.Selenium;

namespace SauceDemoTests.Pages
{

    public class InventoryPage
    {
        private readonly IWebDriver driver;

        private readonly By appLogo = By.CssSelector(".app_logo");

        public InventoryPage(IWebDriver Driver)
        {
            driver = Driver;
        }

        public string GetLogoText()
        {
            return driver.FindElement(appLogo).Text.Trim();
        }
    }
}
