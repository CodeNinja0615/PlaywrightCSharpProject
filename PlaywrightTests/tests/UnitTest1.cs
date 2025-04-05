using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using PlaywrightTests.utilities;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
[AllureEpic("Playwright Epic")]
[AllureFeature("EndToEnd Test")]
public class ExampleTest : BaseTest
{
    [Test]
    [AllureSeverity(SeverityLevel.normal)]
    [AllureTag("regression")]
    [AllureOwner("Sameer")]
    public async Task HasTitle()
    {
        await Page.GotoAsync("https://playwright.dev");

        // Expect a title "to contain" a substring.
        await Expect(Page).ToHaveTitleAsync(new Regex("Playwright"));

    }

    [Test]
    [AllureSeverity(SeverityLevel.normal)]
    [AllureTag("smoke")]
    [AllureOwner("Sameer")]
    public async Task GetStartedLink()
    {
        await Page.GotoAsync("https://playwright.dev");

        // Click the get started link.
        await Page.GetByRole(AriaRole.Link, new() { NameString = "Get started" }).ClickAsync();

        // Expects page to have a heading with the name of Installation.
        await Expect(Page.GetByRole(AriaRole.Heading, new() { NameString = "Installation" })).ToBeVisibleAsync();
    }
}