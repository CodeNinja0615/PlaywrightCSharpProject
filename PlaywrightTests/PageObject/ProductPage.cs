using Microsoft.Playwright;

namespace PlaywrightTests.PageObject;

public class ProductPage
{
    private readonly IPage _page;
    private readonly ILocator _cardBody;
    private readonly ILocator _cartPage;
    public ProductPage(IPage page)
    {
        _page = page;
        _cardBody = page.Locator(".card-body");
        _cartPage = page.Locator("[routerlink*='/dashboard/cart']");
    }

    public async Task AddProductAsync(IList<string> productsList)
    {

        await _page.WaitForSelectorAsync(".card-body");
        var productsEle = await _cardBody.Locator("b").AllTextContentsAsync();
        foreach (var product in productsEle)
        {
            if (productsList.Contains(product))
            {
                await _cardBody.Filter(new LocatorFilterOptions { HasText = product }).Locator("[class*='cart']").ClickAsync();
            }
            else
            {
                continue;
            }
        }
        await _cartPage.ClickAsync();
    }
}
