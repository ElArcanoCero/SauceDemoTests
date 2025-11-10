using OpenQA.Selenium;

namespace SauceDemoTests.Pages
{
    /// <summary>
    /// Page Object de la página de Login (https://www.saucedemo.com/)
    /// Implementa UC-1, UC-2 y login válido (UC-3).
    /// </summary>
    public class LoginPage : BasePage
    {
        private readonly By usernameInput = By.Id("user-name");
        private readonly By passwordInput = By.Id("password");
        private readonly By loginButton = By.Id("login-button");
        private readonly By errorMessage = By.CssSelector("h3[data-test='error']");

        public LoginPage(IWebDriver driver) : base(driver) { }

        /// <summary>Abre la página de login.</summary>
        public void Open() => driver.Navigate().GoToUrl("https://www.saucedemo.com/");

        /// <summary>
        /// UC-1: Escribir credenciales, limpiar ambos campos y hacer login.
        /// Esperado: "Epic sadface: Username is required".
        /// </summary>
        public void LoginEmptyCredentials_UC1(string username, string password)
        {
            Type(usernameInput, username);
            Type(passwordInput, password);

            SmartClear(usernameInput);
            SmartClear(passwordInput);

            Click(loginButton);
        }

        /// <summary>
        /// UC-2: Username válido, password vacía al enviar.
        /// Esperado: "Epic sadface: Password is required".
        /// </summary>
        public void LoginMissingPassword_UC2(string username, string passwordTemp)
        {
            Type(usernameInput, username);
            Type(passwordInput, passwordTemp);
            SmartClear(passwordInput);
            Click(loginButton);
        }

        /// <summary>
        /// UC-3: Login válido normal (standard_user / secret_sauce).
        /// </summary>
        public void Login(string username, string password)
        {
            Type(usernameInput, username);
            Type(passwordInput, password);
            Click(loginButton);
        }

        /// <summary>Devuelve el mensaje de error mostrado tras login inválido.</summary>
        public string GetErrorMessage() => GetText(errorMessage);
    }
}
