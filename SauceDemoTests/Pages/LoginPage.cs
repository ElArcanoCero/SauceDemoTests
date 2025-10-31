using OpenQA.Selenium;

namespace SauceDemoTests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        private readonly By _userField = By.Id("user-name");

        private readonly By _passField = By.Id("password");

        private readonly By _loginBtn = By.Id("login-button");

        private readonly By _errorBox = By.CssSelector("[data-test='error']");

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Open()
        {
            _driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        }

        public void Login(string username, string password)
        {
            _driver.FindElement(_userField).Clear();
            _driver.FindElement(_passField).Clear();

            _driver.FindElement(_userField).SendKeys(username);
            _driver.FindElement(_passField).SendKeys(password);

            _driver.FindElement(_loginBtn).Click();
        }


        public string GetErrorMessage()
        {
            return _driver.FindElement(_errorBox).Text.Trim();
        }
    }
}
