using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using AdminPagesAutomation.Pages;
using AdminPagesAutomation.Utilities;
using System;
using System.IO;

namespace AdminPagesAutomation.Tests
{
    [TestClass]
    public class ManageBillsTests
    {
        private IWebDriver _driver;
        private CreateBillPage _createBillPage;

        [TestInitialize]
        public void Setup()
        {
            ExtentReportHelper.StartReport();

            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl("http://localhost:57857/admin/home");

            _createBillPage = new CreateBillPage(_driver);
        }

        [TestMethod]
        public void AddBillAndVerify()
        {
            ExtentReportHelper.CreateTest("Add Bill and Verify");

            try
            {
                // إضافة فاتورة جديدة
                _createBillPage.CreateBill("New Water Bill", "newwater@gmail.com", "Amman");

                // التحقق من الفواتير الموجودة
                var bills = _createBillPage.GetBills();

                bool billFound = false;
                foreach (var bill in bills)
                {
                    if (bill.Contains("New Water Bill"))
                    {
                        billFound = true;
                        break;
                    }
                }

                Assert.IsTrue(billFound, "The new bill was not found in the list.");
                ExtentReportHelper.LogSuccess("Bill added and verified successfully.");
            }
            catch (Exception ex)
            {
                string screenshot = ScreenshotHelper.CaptureScreenshot(_driver, "AddBillFailure");
                ExtentReportHelper.LogFailure("Test failed: " + ex.Message, screenshot);
                Assert.Fail(ex.Message);
            }
        }

        [TestCleanup]
        public void TearDown()
        {
            _driver.Quit();
            ExtentReportHelper.EndReport();
        }
    }
}
