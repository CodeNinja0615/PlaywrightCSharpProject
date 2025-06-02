using Microsoft.Playwright;

namespace PlaywrightTests.PageObject;

public class CartPage
{
    private readonly IPage _page;
    private readonly ILocator _cartProducts;
    private readonly ILocator _checkout;
    public CartPage(IPage page)
    {
        _page = page;
        _cartProducts = page.Locator(".cartSection h3");
        _checkout = page.Locator("text=Checkout");
    }

    public async Task VerifyCartProductsAsync(IList<string> productsList)
    {
        await _page.WaitForSelectorAsync(".cartSection h3");
        int cartProdCount = await _cartProducts.CountAsync();
        Assert.That(cartProdCount, Is.EqualTo(productsList.Count));
        await _checkout.ClickAsync();
    }
}
