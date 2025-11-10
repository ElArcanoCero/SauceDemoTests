using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SauceDemoTests.Pages
{
    /// <summary>
    /// Clase base para páginas clic, tipeo, lectura y waits recomendado en la retroalimentacion
    /// </summary>
    public abstract class BasePage
    {
        protected readonly IWebDriver driver;
        protected readonly WebDriverWait wait;

        protected BasePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(new SystemClock(), driver, TimeSpan.FromSeconds(8), TimeSpan.FromMilliseconds(200));
        }

        /// <summary>Hace clic seguro sobre un locator.</summary>
        protected void Click(By locator)
        {
            try
            {
                wait.Until(d => d.FindElement(locator).Displayed);
                driver.FindElement(locator).Click();
            }
            catch (Exception ex)
            {
                SauceDemoTests.Utils.Logger.Error($"Click fallo en {locator}: {ex.Message}");
                throw;
            }
        }

        /// <summary>Escribe texto tras limpiar el campo.</summary>
        protected void Type(By locator, string text)
        {
            try
            {
                var el = wait.Until(d => d.FindElement(locator));
                el.Clear();
                el.SendKeys(text);
            }
            catch (Exception ex)
            {
                SauceDemoTests.Utils.Logger.Error($"Type fallo en {locator}: {ex.Message}");
                throw;
            }
        }

        /// <summary>Obtiene el texto visible del locator.</summary>
        protected string GetText(By locator)
        {
            try
            {
                return wait.Until(d => d.FindElement(locator)).Text.Trim();
            }
            catch (Exception ex)
            {
                SauceDemoTests.Utils.Logger.Error($"GetText fallo en {locator}: {ex.Message}");
                throw;
            }
        }

        /// <summary>Limpieza agresiva: Ctrl+A + Backspace (Edge-safe).</summary>
        protected void SmartClear(By locator)
        {
            try
            {
                var el = wait.Until(d => d.FindElement(locator));
                el.SendKeys(Keys.Control + "a");
                el.SendKeys(Keys.Backspace);
            }
            catch (Exception ex)
            {
                SauceDemoTests.Utils.Logger.Error($"SmartClear fallo en {locator}: {ex.Message}");
                throw;
            }
        }
    }
}

