using AventStack.ExtentReports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using Efawatercom_final_project.Pages;


namespace Eawatercom_final_project.Pages
{
    [TestClass]
    public class ManageProfileTest
    {
        private IWebDriver driver;
        private ManageProfilePage profilePage;
        private ExtentReports extent;
        private ExtentTest test;

        [TestInitialize]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:4200/admin/account");

            // إعداد Extent Report
            var htmlReporter = new ExtentHtmlReporter(@"ExtentReports\ManageProfileTest.html");
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

            // إنشاء اختبار جديد
            test = extent.CreateTest("Manage Profile Test");

            // تمرير الـ test إلى صفحة إدارة الملف الشخصي
            profilePage = new ManageProfilePage(driver, test);
        }

        [TestMethod]
        public void TestChangeName()
        {
            string newName = "New Admin Name";
            string password = "correctPassword";

            try
            {
                profilePage.ChangeName(newName, password);
                string successMessage = profilePage.GetSuccessMessage();

                // تحقق من ظهور رسالة النجاح بعد تغيير الاسم
                Assert.AreEqual("Your name has been successfully updated.", successMessage);
                test.Pass("Name changed successfully ✅");
            }
            catch (Exception ex)
            {
                string screenshotPath = profilePage.CaptureScreenshot("TestChangeName_Failed");
                test.Fail($"Test Failed ❌: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }

        [TestMethod]
        public void TestChangeNameWithIncorrectPassword()
        {
            string newName = "New Admin Name";
            string wrongPassword = "incorrectPassword";

            try
            {
                profilePage.ChangeName(newName, wrongPassword);
                string errorMessage = profilePage.GetErrorMessage();

                // تحقق من ظهور رسالة الخطأ في حال كان كلمة المرور غير صحيحة
                Assert.AreEqual("Incorrect password.", errorMessage);
                test.Pass("Error message for incorrect password displayed ✅");
            }
            catch (Exception ex)
            {
                string screenshotPath = profilePage.CaptureScreenshot("TestChangeNameWithIncorrectPassword_Failed");
                test.Fail($"Test Failed ❌: {ex.Message}")
                    .AddScreenCaptureFromPath(screenshotPath);
                throw;
            }
        }

        [TestCleanup]
        public void TearDown()
        {
            extent.Flush(); // إنشاء التقرير بعد التنفيذ
            driver.Quit();  // إغلاق المتصفح
        }
    }
}
