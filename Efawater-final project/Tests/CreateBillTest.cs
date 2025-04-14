using AventStack.ExtentReports;
using Efawater_final_project.pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Efawater_final_project.Tests
{
    [TestClass]
    public class ManageBillTest
    {
        private IWebDriver driver;
        private CreateBillPage billPage;
        private ExtentReports extent;
        private ExtentTest test;

        [TestInitialize]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:4200/admin/createbill");

            // إعداد تقرير Extent
            var htmlReporter = new ExtentHtmlReporter(@"ExtentReports\CreateBillTest.html");
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

            // إنشاء اختبار جديد
            test = extent.CreateTest("Create Bill Test");

            // تمرير test إلى صفحة إنشاء الفاتورة
            billPage = new CreateBillPage(driver, test);
        }

        [TestMethod]
        public void TestCreateBill()
        {
            try
            {
                billPage.EnterBillName("Test Bill");
                billPage.EnterEmail("test@example.com");
                billPage.EnterLocation("Test Location");
                billPage.SelectCategoryAuto(); // أو يمكن تمرير فئة معينة
                billPage.ClickCreateButton();

                // تحقق من ظهور الرسالة أو أي مؤشر آخر على النجاح
                // (افترض أنه بعد الضغط على "إنشاء" تظهر رسالة أو نافذة منبثقة)
                test.Pass("Bill created successfully ✅");
            }
            catch (Exception ex)
            {
                string screenshotPath = billPage.CaptureScreenshot("TestCreateBill_Failed");
                test.Fail($"Test Failed: {ex.Message} ❌")
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
