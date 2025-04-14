using OpenQA.Selenium;
using System;
using System.IO;

namespace AdminPagesAutomation.Utilities
{
    public class ScreenshotHelper
    {
        public static string CaptureScreenshot(IWebDriver driver, string testName)
        {
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports", "Screenshots");
            Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");

            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);

            return filePath;
        }
    }
}

