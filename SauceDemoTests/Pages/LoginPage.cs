using OpenQA.Selenium;

namespace SauceDemoTests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver driver;

        private readonly By usernameInput = By.Id("user-name");
        private readonly By passwordInput = By.Id("password");
        private readonly By loginButton = By.Id("login-button");
        private readonly By errorMessage = By.CssSelector("h3[data-test='error']");

        public LoginPage(IWebDriver Driver)
        {
            driver = Driver;
        }

        public void Open()
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        }
        private void ForceClearWithJs(By locator)
        {
            var element = driver.FindElement(locator);
            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].value = '';", element);
        }

        public void LoginEmptyCredentials_UC1(string username, string password)
        {
            var user = driver.FindElement(usernameInput);
            var pass = driver.FindElement(passwordInput);

            user.Clear();
            pass.Clear();

            user.SendKeys(username);
            pass.SendKeys(password);

            user.Clear();
            pass.Clear();

            driver.FindElement(loginButton).Click();
        }

        private void ForceClearWithJs(IWebElement pass)
        {
            throw new NotImplementedException();
        }

        public void LoginMissingPassword_UC2(string username, string password)
        {
            var user = driver.FindElement(usernameInput);
            var pass = driver.FindElement(passwordInput);

            user.Clear();
            pass.Clear();

            user.SendKeys(username);
            pass.SendKeys(password);

            pass.Clear();

            driver.FindElement(loginButton).Click();
        }

        public void Login(string username, string password)
        {
            var user = driver.FindElement(usernameInput);
            var pass = driver.FindElement(passwordInput);

            user.Clear();
            pass.Clear();

            user.SendKeys(username);
            pass.SendKeys(password);

            driver.FindElement(loginButton).Click();
        }

        public string GetErrorMessage()
        {
            return driver.FindElement(errorMessage).Text.Trim();
        }
    }
}

