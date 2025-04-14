using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace AdminPagesAutomation.Utilities
{
    public static class ExtentReportHelper
    {
        public static ExtentReports extent;
        public static ExtentTest test;

        public static void StartReport()
        {
            var htmlReporter = new ExtentHtmlReporter("Reports\\AutomationReport.html");
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
        }

        public static void CreateTest(string testName)
        {
            test = extent.CreateTest(testName);
        }

        public static void LogSuccess(string message)
        {
            test.Pass(message);
        }

        public static void LogFailure(string message, string screenshotPath)
        {
            test.Fail(message).AddScreenCaptureFromPath(screenshotPath);
        }

        public static void EndReport()
        {
            extent.Flush();
        }
    }
}
