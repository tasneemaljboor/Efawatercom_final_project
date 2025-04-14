using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;

namespace Efawater_final_project.pages
{
    public class CreateBillPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private ExtentTest test;

        public CreateBillPage(IWebDriver driver, ExtentTest test)
        {
            this.driver = driver;
            this.test = test;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        // عناصر الصفحة
        private IWebElement BillNameInput => wait.Until(d => d.FindElement(By.Id("billName")));
        private IWebElement EmailInput => driver.FindElement(By.Id("email"));
        private IWebElement LocationInput => driver.FindElement(By.Id("location"));
        private IWebElement CategoryDropdown => driver.FindElement(By.Id("categoryDropdown"));
        private IWebElement CreateButton => driver.FindElement(By.Id("createButton"));

        // إدخال اسم الفاتورة
        public void EnterBillName(string name)
        {
            try
            {
                BillNameInput.Clear();
                BillNameInput.SendKeys(name);
                test.Pass("Bill name entered successfully ✅");
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("EnterBillName_Failed");
                test.Fail($"Failed to enter bill name: {ex.Message} ❌")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }

        // إدخال البريد الإلكتروني
        public void EnterEmail(string email)
        {
            try
            {
                EmailInput.Clear();
                EmailInput.SendKeys(email);
                test.Pass("Email entered successfully ✅");
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("EnterEmail_Failed");
                test.Fail($"Failed to enter email: {ex.Message} ❌")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }

        // إدخال الموقع
        public void EnterLocation(string location)
        {
            try
            {
                LocationInput.Clear();
                LocationInput.SendKeys(location);
                test.Pass("Location entered successfully ✅");
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("EnterLocation_Failed");
                test.Fail($"Failed to enter location: {ex.Message} ❌")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }

        // اختيار الفئة تلقائيًا (عشوائيًا إذا لم يتم تحديدها)
        public void SelectCategoryAuto(string? categoryName = null)
        {
            try
            {
                var select = new SelectElement(CategoryDropdown);

                if (string.IsNullOrWhiteSpace(categoryName)) // تصحيح السطر
                {
                    var random = new Random();
                    int index = random.Next(0, select.Options.Count);
                    var selectedOption = select.Options[index].Text;
                    Console.WriteLine($"[INFO] Random category selected: {selectedOption}");
                    select.SelectByIndex(index);
                    test.Pass("Random category selected successfully ✅");
                }
                else
                {
                    select.SelectByText(categoryName);
                    test.Pass($"Category '{categoryName}' selected successfully ✅");
                }
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("SelectCategory_Failed");
                test.Fail($"Failed to select category: {ex.Message} ❌")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }

        // الضغط على زر إنشاء الفاتورة
        public void ClickCreateButton()
        {
            try
            {
                CreateButton.Click();
                test.Pass("Create button clicked successfully ✅");
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("ClickCreateButton_Failed");
                test.Fail($"Failed to click create button: {ex.Message} ❌")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }

        // دالة لأخذ لقطات الشاشة
        public string CaptureScreenshot(string testName)
        {
            ITakesScreenshot screenshotDriver = (ITakesScreenshot)driver;
            Screenshot screenshot = screenshotDriver.GetScreenshot();

            string screenshotsDir = "Screenshots";
            Directory.CreateDirectory(screenshotsDir);

            string filePath = Path.Combine(screenshotsDir, $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");
            screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);

            return filePath;
        }
    }
}
