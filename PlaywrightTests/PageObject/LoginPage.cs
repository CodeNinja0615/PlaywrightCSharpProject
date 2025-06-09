using Microsoft.Playwright;

namespace PlaywrightTests.PageObject;

public class LoginPage
{
    private readonly IPage _page;
    private readonly ILocator _email;
    private readonly ILocator _password;
    private readonly ILocator _login;
    public LoginPage(IPage page)
    {
        _page = page;
        _email = page.Locator("#userEmail");
        _password = page.Locator("#userPassword");
        _login = _page.Locator("#login");

    }
    public async Task GoToAsync()
    {
        await _page.GotoAsync("https://rahulshettyacademy.com/client/");
    }
    public async Task SignInAsync(string email, string password)
    {
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await _email.FillAsync(email);
        await _password.FillAsync(password);
        await _login.ClickAsync();
    }
}
