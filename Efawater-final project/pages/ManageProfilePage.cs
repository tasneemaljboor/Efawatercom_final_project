using AventStack.ExtentReports;
using OpenQA.Selenium;
using System;
using System.IO;

namespace Efawatercom_final_project.Pages
{
    public class ManageProfilePage
    {
        private readonly IWebDriver _driver;
        private readonly ExtentTest _test;

        public ManageProfilePage(IWebDriver driver, ExtentTest test)
        {
            _driver = driver;
            _test = test; // تمرير الـ ExtentTest إلى الصفحة
        }

        private IWebElement NameField => _driver.FindElement(By.Id("name"));
        private IWebElement PasswordField => _driver.FindElement(By.Id("password"));
        private IWebElement SaveButton => _driver.FindElement(By.Id("save_button"));
        private IWebElement SuccessMessage => _driver.FindElement(By.Id("success_message"));
        private IWebElement ErrorMessage => _driver.FindElement(By.Id("error_message"));

        public void ChangeName(string newName, string password)
        {
            try
            {
                // إدخال الاسم الجديد وكلمة المرور
                NameField.Clear();
                NameField.SendKeys(newName);
                PasswordField.SendKeys(password);
                SaveButton.Click();

                // التحقق من النتيجة
                string successMessage = GetSuccessMessage();
                if (successMessage.Contains("successfully"))
                {
                    _test.Pass("Name changed successfully");
                }
                else
                {
                    string errorMessage = GetErrorMessage();
                    _test.Fail($"Failed to change name: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                string screenshotPath = CaptureScreenshot("ChangeName_Failed");
                _test.Fail($"Test Failed ❌: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }

        public string GetSuccessMessage()
        {
            try
            {
                return SuccessMessage.Text;
            }
            catch (NoSuchElementException)
            {
                return string.Empty;
            }
        }

        public string GetErrorMessage()
        {
            try
            {
                return ErrorMessage.Text;
            }
            catch (NoSuchElementException)
            {
                return string.Empty;
            }
        }

        // دالة لالتقاط لقطات الشاشة
        public string CaptureScreenshot(string testName)
        {
            ITakesScreenshot screenshotDriver = (ITakesScreenshot)_driver;
            Screenshot screenshot = screenshotDriver.GetScreenshot();

            string screenshotsDir = "Screenshots";
            Directory.CreateDirectory(screenshotsDir);

            string filePath = Path.Combine(screenshotsDir, $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");
            screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);

            return filePath;
        }
    }
}
