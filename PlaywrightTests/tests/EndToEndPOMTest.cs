using System.Threading.Tasks;
using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using Microsoft.Playwright;
using PlaywrightTests.PageObject;
using PlaywrightTests.Utilities;

namespace PlaywrightTests.Tests;

[TestFixture]
[AllureEpic("Playwright Epic")]
[AllureFeature("EndToEnd Ecommerce Test")]
public class EndToEndPOMTest : BaseTest
{
    [Test, TestCaseSource(nameof(AddTestDataConfig))]
    [AllureSeverity(SeverityLevel.critical)]
    [AllureTag("Regression")]
    [AllureOwner("Sameer")]
    public async Task E2EecommerceTest(string email, string password, string[] products)
    {
        // Arrange-Act-Assert (AAA)

        // Arrange
        // string email = "akhtarsameer743@gmail.com";
        // string[] products = { "ZARA COAT 3", "IPHONE 13 PRO" };
        IList<string> productsList = products.ToList();

        // Act
        IPage page = await Context.NewPageAsync();
        var loginPage = new LoginPage(page);
        await loginPage.GoToAsync();
        await loginPage.SignInAsync(email, password);

        var productPage = new ProductPage(page);
        await productPage.AddProductAsync(productsList: productsList);

        var cartPage = new CartPage(page);
        await cartPage.VerifyCartProductsAsync(productsList: productsList);

        var checkoutPage = new CheckoutPage(page);
        await checkoutPage.SelectCountryAsync();
        await checkoutPage.PlaceOrderAsync();

        var confirmationPage = new ConfirmationPage(page);

        // Assert
        await Expect(confirmationPage.GetConfirmationMsg()).ToContainTextAsync("Thankyou for the order.");
        await confirmationPage.ConfirmOrderAsync();

    }

    public static IEnumerable<TestCaseData> AddTestDataConfig()
    {
        yield return new TestCaseData(GetDataParser().ExtractData("email"), GetDataParser().ExtractData("password"), GetDataParser().ExtractDataArray("products"));
        yield return new TestCaseData(GetDataParser().ExtractData("wrong_email"), GetDataParser().ExtractData("wrong_password"), GetDataParser().ExtractDataArray("products"));
        yield return new TestCaseData(GetDataParser().ExtractData("email"), GetDataParser().ExtractData("password"), GetDataParser().ExtractDataArray("products"));
    }
}
// HEADED=1 dotnet test --filter "FullyQualifiedName~EndToEndPOMTest"
// PWDEBUG=1 dotnet test --filter "FullyQualifiedName~EndToEndPOMTest"

