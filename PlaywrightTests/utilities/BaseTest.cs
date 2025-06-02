using System;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using AventStack.ExtentReports.Reporter;
using Microsoft.Playwright;
using NUnit.Framework.Interfaces;
using Media = AventStack.ExtentReports.Model.Media;

namespace PlaywrightTests.utilities
{
    [AllureNUnit]
    [AllureSuite("E2E Test Suite")]
    public class BaseTest : PageTest
    {
        public ExtentReports extent;
        public ExtentTest test;

        [OneTimeSetUp]
        [AllureOwner("Sameer")]
        public void ExtentSetup()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.FullName!;
            String reportPath = projectDirectory + "//index.html";
            var htmpRporter = new ExtentSparkReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmpRporter);
            extent.AddSystemInfo("Host Name", "Local host");
            extent.AddSystemInfo("Environment", "QA");
            extent.AddSystemInfo("UserName", "Sameer Akhtar");
        }

        [SetUp]
        public async Task Setup()
        {
            Context.SetDefaultTimeout(60000); // Set timeout to 60 seconds
            await Context.GrantPermissionsAsync(new List<string>
            {
                "geolocation",
                "notifications",
                "camera",
                "microphone",
                "clipboard-read",
                "clipboard-write",
                "payment-handler",
                "accelerometer",
                "ambient-light-sensor",
                "midi",
                "background-sync"
            });
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            await Context.Tracing.StartAsync(new()
            {
                Title = $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}",
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });
        }

        public override BrowserNewContextOptions ContextOptions()
        {
            return new BrowserNewContextOptions()
            {
                ColorScheme = ColorScheme.Light,
                ViewportSize = new()
                {
                    Width = 1920,
                    Height = 1080
                },
                BaseURL = "https://github.com",
                IgnoreHTTPSErrors = true, // 👈 This line allows invalid SSL certs
                // Permissions = new[]
                // {
                //     "geolocation",
                //     "notifications",
                //     "camera",
                //     "microphone",
                //     "clipboard-read",
                //     "clipboard-write",
                //     "payment-handler",
                //     "accelerometer",
                //     "ambient-light-sensor",
                //     "midi",
                //     "accessibility-events",
                //     "background-sync"
                // },
            };
        }

        [TearDown]
        public async Task Teardown()
        {
            // Only stop tracing if it was started
            if (Context.Tracing != null)
            {
                try
                {
                    await Context.Tracing.StopAsync(new()
                    {
                        Path = Path.Combine(
                        TestContext.CurrentContext.WorkDirectory,
                        "playwright-traces",
                        $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}.zip"
                    )
                    });
                }
                catch (PlaywrightException)
                {
                    // Tracing was not started, ignore
                }
            }
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = TestContext.CurrentContext.Result.StackTrace;
            string fileName = "Screenshot_" + DateTime.Now.ToString("HH_mm_ss") + ".png";

            if (status == TestStatus.Failed)
            {
                var media = await CaptureScreenShot(Page, fileName);
                test.Fail("Test Failed", media);
                test.Log(Status.Fail, $"Test failed with logtrace: {stacktrace}");
            }
            else if (status == TestStatus.Passed)
            {
                test.Pass("Test Passed");
            }
        }
        public async Task<Media> CaptureScreenShot(IPage page, string screenshotName)
        {
            var screenshotBytes = await page.ScreenshotAsync();
            string base64String = Convert.ToBase64String(screenshotBytes);

            Media media = MediaEntityBuilder
                            .CreateScreenCaptureFromBase64String(base64String, screenshotName)
                            .Build();

            return media;
        }

        [OneTimeTearDown]
        public void ExtentTearDown()
        {
            extent.Flush();
        }
    }
}