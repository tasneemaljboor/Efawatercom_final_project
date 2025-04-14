using Efawatercom_final_project.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Efawatercom_final_project.Tests
{
    [TestClass]
    public class LoginTest
    {
        private IWebDriver driver;
        private LoginPage loginPage;

        [TestInitialize]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:4200/admin/login");
            loginPage = new LoginPage(driver);
        }

        [TestMethod]
        public void TestLogin()
        {
            loginPage.Login("Admin", "123456");
            string errorMessage = loginPage.GetErrorMessage();

            Assert.AreEqual("Expected error message", errorMessage);
        }

        [TestCleanup]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
