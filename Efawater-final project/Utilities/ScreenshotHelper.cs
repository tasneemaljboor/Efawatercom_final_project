using OpenQA.Selenium;
using System.Drawing.Imaging;  // استيراد ImageFormat
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;

public class ScreenshotHelper
{
    public void CaptureScreenshot(IWebDriver driver, string filePath)
    {
        try
        {
            // أخذ screenshot من الصفحة
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();

            // حفظ الصورة بتنسيق PNG
            screenshot.SaveAsFile(filePath);  // فقط مسار الملف بدون ImageFormat
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error capturing screenshot: " + ex.Message);
        }
    }
}
