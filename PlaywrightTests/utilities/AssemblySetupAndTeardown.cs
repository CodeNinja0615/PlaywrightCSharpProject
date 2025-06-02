// File: PlaywrightTests/utilis/AssemblySetupAndTeardown.cs
// IMPORTANT: For this to be truly assembly-wide, it should ideally be outside any namespace
// or in a root namespace that encompasses all your tests.
// This SetUpFixture is intended for the whole assembly, ensure it's at the top level.
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

// No namespace here for true assembly-level
[SetUpFixture]
public class AssemblySetupAndTeardown
{
    public  static ExtentReports extent;
    [OneTimeSetUp]
    public void GlobalExtentSetup() //---Before Test
    {
        string workingDirectory = Environment.CurrentDirectory;
        string projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.FullName!;
        String reportPath = projectDirectory + "//index.html";
        var htmpRporter = new ExtentSparkReporter(reportPath);
        htmpRporter.Config.Theme = AventStack.ExtentReports.Reporter.Config.Theme.Dark;
        htmpRporter.Config.DocumentTitle = "Playwright Test Automation Report";
        htmpRporter.Config.ReportName = "E2E Test Suite Report";
        extent = new ExtentReports();
        extent.AttachReporter(htmpRporter);
        extent.AddSystemInfo("Host Name", "Local host");
        extent.AddSystemInfo("Environment", "QA");
        extent.AddSystemInfo("UserName", "Sameer Akhtar");
        extent.AddSystemInfo("Framework", "Playwright with NUnit");
    }

    [OneTimeTearDown]
    public void GlobalExtentTearDown() //---After Test
    {
        extent.Flush();
    }
}
