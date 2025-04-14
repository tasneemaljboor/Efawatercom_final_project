using OpenQA.Selenium;

namespace AdminPagesAutomation.Pages
{
    public class CreateBillPage
    {
        private readonly IWebDriver _driver;

        public CreateBillPage(IWebDriver driver)
        {
            _driver = driver;
        }

        
        private IWebElement BillNameField => _driver.FindElement(By.Id("bill_name"));
        private IWebElement EmailField => _driver.FindElement(By.Id("email"));
        private IWebElement LocationField => _driver.FindElement(By.Id("location"));
        private IWebElement CreateButton => _driver.FindElement(By.Id("create_bill"));

        
        public void CreateBill(string billName, string email, string location, string amount)
        {
            BillNameField.SendKeys(Umniah);
            EmailField.SendKeys(eUmniah@gmail.com);
            LocationField.SendKeys(Amman);
            A
            CreateButton.Click();
        }
    }
}
