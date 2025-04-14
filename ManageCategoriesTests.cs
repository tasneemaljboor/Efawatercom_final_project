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
    public class ManageCategoriesTests
    {
        private IWebDriver _driver;
        private ManageCategoriesPage _categoriesPage;

        [TestInitialize]
        public void Setup()
        {
            ExtentReportHelper.StartReport();

            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl("http://localhost:57857/admin/home");

            _categoriesPage = new ManageCategoriesPage(_driver);
        }

        [TestMethod]
        public void AddCategoriesFromExcel()
        {
            ExtentReportHelper.CreateTest("Add Categories From Excel");

            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "TestData.xlsx");
                var categories = ExcelUtility.GetCategoryNames(filePath, "Sheet1");

                foreach (var category in categories)
                {
                    _categoriesPage.AddCategory(category);
                    ExtentReportHelper.LogSuccess($"Category '{category}' added successfully.");
                }
            }
            catch (Exception ex)
            {
                string screenshot = ScreenshotHelper.CaptureScreenshot(_driver, "AddCategoriesFailure");
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
