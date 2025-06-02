using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using Microsoft.Playwright;
using PlaywrightTests.utilities;
using System.Text.RegularExpressions;

namespace PlaywrightTests.tests;

[TestFixture]
[AllureEpic("Playwright Epic")]
[AllureFeature("EndToEnd Test")]
public class EndToEndTest : BaseTest
{
    [Test]
    [AllureSeverity(SeverityLevel.normal)]
    [AllureTag("smoke")]
    [AllureOwner("Sameer")]
    public async Task E2EecommerceTest()
    {
        string email = "akhtarsameer743@gmail.com";
        string[] products = { "ZARA COAT 3", "IPHONE 13 PRO" };
        IList<string> productsList = products.ToList();
        IPage page = await Context.NewPageAsync();
        await page.GotoAsync("https://rahulshettyacademy.com/client/");
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await page.Locator("#userEmail").FillAsync(email);
        await page.Locator("#userPassword").FillAsync("Sameerking01!");
        await page.Locator("#login").ClickAsync();
        await page.WaitForSelectorAsync(".card-body");
        ILocator cardBody = page.Locator(".card-body");
        var productsEle = await cardBody.Locator("b").AllTextContentsAsync();
        foreach (var product in productsEle)
        {
            if (productsList.Contains(product))
            {
                await cardBody.Filter(new LocatorFilterOptions { HasText = product }).Locator("[class*='cart']").ClickAsync();
            }
            else
            {
                continue;
            }
        }
        await page.Locator("[routerlink*='/dashboard/cart']").ClickAsync();
        await page.WaitForSelectorAsync(".cartSection h3");
        int cartProdCount = await page.Locator(".cartSection h3").CountAsync();
        Assert.That(cartProdCount, Is.EqualTo(productsList.Count));
        await page.Locator("text=Checkout").ClickAsync();
        await page.Locator("[placeholder='Select Country']").PressSequentiallyAsync("India");
        await page.WaitForSelectorAsync("span.ng-star-inserted");
        await page.Locator("span.ng-star-inserted").Filter(new LocatorFilterOptions { HasText = " India" }).Nth(1).ClickAsync();
        await page.Locator("text='Place Order'").ClickAsync();
        ILocator confirmationMsg = page.Locator(".hero-primary");
        await Expect(confirmationMsg).ToContainTextAsync("Thankyou for the order.");
        await page.Locator("button[routerlink*='/dashboard/myorders']").ClickAsync();
    }
}
// HEADED=1 dotnet test --filter "FullyQualifiedName~EndToEndTest"