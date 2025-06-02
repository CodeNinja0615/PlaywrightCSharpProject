using Microsoft.Playwright;

namespace PlaywrightTests.PageObject;

public class ConfirmationPage
{
    private readonly IPage _page;
    private readonly ILocator _confirmationMsg;
    private readonly ILocator _orderPage;
    public ConfirmationPage(IPage page)
    {
        _page = page;
        _confirmationMsg = page.Locator(".hero-primary");
        _orderPage = page.Locator("button[routerlink*='/dashboard/myorders']");
    }

    public ILocator GetConfirmationMsg()
    {
        return _confirmationMsg;
    }
    public async Task ConfirmOrderAsync()
    {
        await _orderPage.ClickAsync();
    }
}
