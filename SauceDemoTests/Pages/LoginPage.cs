using OpenQA.Selenium;

namespace SauceDemoTests.Pages
{
    /// <summary>
    /// Page Object para la página de login de https://www.saucedemo.com/
    /// Expone acciones (Open, Login) y lecturas (GetErrorMessage).
    /// Esta clase representa SOLO la pantalla de login.
    /// </summary>
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        // ======================
        // Selectores / Locators
        // ======================

        // Campo Username
        private readonly By _userField = By.Id("user-name");

        // Campo Password
        private readonly By _passField = By.Id("password");

        // Botón Login
        private readonly By _loginBtn = By.Id("login-button");

        // Caja de error que muestra mensajes como
        // "Epic sadface: Username is required"
        // "Epic sadface: Password is required"
        private readonly By _errorBox = By.CssSelector("[data-test='error']");


        /// <summary>
        /// El constructor recibe el WebDriver que ya fue creado por DriverManager.
        /// </summary>
        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        /// <summary>
        /// Navega a la URL base de la app.
        /// </summary>
        public void Open()
        {
            _driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        }

        /// <summary>
        /// Intenta iniciar sesión con las credenciales dadas.
        /// No valida el resultado, solo ejecuta la acción.
        /// </summary>
        public void Login(string username, string password)
        {
            // Limpiar campos primero (esto sigue el escenario UC-1 y UC-2)
            _driver.FindElement(_userField).Clear();
            _driver.FindElement(_passField).Clear();

            // Escribir valores en los inputs
            _driver.FindElement(_userField).SendKeys(username);
            _driver.FindElement(_passField).SendKeys(password);

            // Click en el botón Login
            _driver.FindElement(_loginBtn).Click();
        }

        /// <summary>
        /// Devuelve el mensaje de error mostrado debajo del login,
        /// por ejemplo:
        /// "Epic sadface: Username is required"
        /// "Epic sadface: Password is required"
        /// </summary>
        public string GetErrorMessage()
        {
            return _driver.FindElement(_errorBox).Text.Trim();
        }
    }
}
