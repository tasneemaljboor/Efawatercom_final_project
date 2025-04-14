using OpenQA.Selenium;

namespace AdminPagesAutomation.Pages
{
    public class ManageCategoriesPage
    {
        private readonly IWebDriver _driver;

        public ManageCategoriesPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private IWebElement AddCategoryButton => _driver.FindElement(By.Id("add_category"));
        private IWebElement CategoryNameField => _driver.FindElement(By.Id("category_name"));
        private IWebElement SubmitButton => _driver.FindElement(By.Id("submit_button"));

        public void AddCategory(string categoryName)
        {
            AddCategoryButton.Click();
            CategoryNameField.Clear();
            CategoryNameField.SendKeys(categoryName);
            SubmitButton.Click();
        }
    }
}
