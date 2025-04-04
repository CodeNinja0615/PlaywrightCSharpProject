using System;
using Microsoft.Playwright;
using NUnit.Framework.Interfaces;

namespace PlaywrightTests.testComponents
{
    public class BaseTest : PageTest
    {
        [SetUp]
        public async Task Setup()
        {
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
            };
        }

        [TearDown]
        public async Task Teardown()
        {
            await Context.Tracing.StopAsync(new()
            {
                Path = Path.Combine(
                            TestContext.CurrentContext.WorkDirectory,
                            "playwright-traces",
                            $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}.zip"
                        )
            });
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var screenshot = await Page.ScreenshotAsync();
                using var stream = new MemoryStream(screenshot);
                var tempFilePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.png");
                await File.WriteAllBytesAsync(tempFilePath, screenshot);
            }

        }
    }
}