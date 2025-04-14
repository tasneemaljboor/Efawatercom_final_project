using OpenQA.Selenium;

namespace Efawatercom_final_project.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private IWebElement UsernameField => _driver.FindElement(By.Id("username"));
        private IWebElement PasswordField => _driver.FindElement(By.Id("password"));
        private IWebElement LoginButton => _driver.FindElement(By.Id("login_button"));
        private IWebElement ErrorMessage => _driver.FindElement(By.Id("error_message"));

        public void Login(string username, string password)
        {
            UsernameField.Clear();  // التأكد من مسح الحقول قبل الكتابة
            UsernameField.SendKeys(username);
            PasswordField.Clear();
            PasswordField.SendKeys(password);
            LoginButton.Click();
        }

        public string GetErrorMessage()
        {
            return ErrorMessage.Text;
        }
    }
}
