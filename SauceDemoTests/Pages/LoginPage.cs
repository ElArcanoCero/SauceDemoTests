using OpenQA.Selenium;

namespace SauceDemoTests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver driver;

        private readonly By userField = By.Id("user-name");

        private readonly By passField = By.Id("password");

        private readonly By loginBtn = By.Id("login-button");

        private readonly By errorBox = By.CssSelector("[data-test='error']");

        public LoginPage(IWebDriver Driver)
        {
            driver = Driver;
        }

        public void Open()
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        }

        public void Login(string username, string password)
        {
            driver.FindElement(userField).Clear();
            driver.FindElement(passField).Clear();

            driver.FindElement(userField).SendKeys(username);
            driver.FindElement(passField).SendKeys(password);

            driver.FindElement(loginBtn).Click();
        }


        public string GetErrorMessage()
        {
            return driver.FindElement(errorBox).Text.Trim();
        }
    }
}
