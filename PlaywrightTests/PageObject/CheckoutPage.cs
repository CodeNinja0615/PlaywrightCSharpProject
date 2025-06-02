using Microsoft.Playwright;

namespace PlaywrightTests.PageObject;

public class CheckoutPage
{
    private readonly IPage _page;
    private readonly ILocator _autoSugDrdo;
    private readonly ILocator _country;
    private readonly ILocator _placeOrder;
    public CheckoutPage(IPage page)
    {
        _page = page;
        _autoSugDrdo = page.Locator("[placeholder='Select Country']");
        _country = page.Locator("span.ng-star-inserted");
        _placeOrder = page.Locator("text='Place Order'");
    }

    public async Task SelectCountryAsync()
    {
        await _autoSugDrdo.PressSequentiallyAsync("India");
        await _page.WaitForSelectorAsync("span.ng-star-inserted");
        await _country.Filter(new LocatorFilterOptions { HasText = " India" }).Nth(1).ClickAsync();
    }

    public async Task PlaceOrderAsync()
    {
        await _placeOrder.ClickAsync();
    }
}
