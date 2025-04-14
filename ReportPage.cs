using OpenQA.Selenium;
using System.Collections.Generic;

namespace AdminPagesAutomation.Pages
{
    public class ReportPage
    {
        private readonly IWebDriver _driver;

        public ReportPage(IWebDriver driver)
        {
            _driver = driver;
        }

        // عناصر الإدخال
        private IWebElement CategorySearchField => _driver.FindElement(By.Id("category_search"));
        private IWebElement DateFromField => _driver.FindElement(By.Id("date_from"));
        private IWebElement DateToField => _driver.FindElement(By.Id("date_to"));
        private IWebElement SearchButton => _driver.FindElement(By.Id("search_button"));

        // قائمة الفواتير
        private IList<IWebElement> BillRows => _driver.FindElements(By.CssSelector(".bill-row"));

        // وظيفة البحث باستخدام اسم الفئة
        public void SearchByCategory(string categoryName)
        {
            CategorySearchField.SendKeys(categoryName);
            SearchButton.Click();
        }

        // وظيفة البحث باستخدام التاريخ
        public void SearchByDate(string fromDate, string toDate)
        {
            DateFromField.SendKeys(fromDate);
            DateToField.SendKeys(toDate);
            SearchButton.Click();
        }

        // الحصول على تفاصيل الفواتير من الجدول
        public List<string> GetBillDetails()
        {
            var billDetails = new List<string>();

            foreach (var row in BillRows)
            {
                var billName = row.FindElement(By.CssSelector(".bill-name")).Text;
                var billLocation = row.FindElement(By.CssSelector(".bill-location")).Text;
                var billAmount = row.FindElement(By.CssSelector(".bill-amount")).Text;
                billDetails.Add($"{billName} | {billLocation} | {billAmount}");
            }

            return billDetails;
        }
    }
}

