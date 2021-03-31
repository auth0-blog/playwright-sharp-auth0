using System.Threading.Tasks;
using NUnit.Framework;
using PlaywrightSharp;

namespace PlaywrightSharpAuth0
{
    public class Tests
    {
        [Test]
        public async Task Auth0RegularWebAppASPNETCoreLinkWorks()
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(headless: true);

            // Go to Auth0 home page
            var page = await browser.NewPageAsync();
            await page.GoToAsync("https://auth0.com/");

            // Click on the cookie policy acceptance button if it exists
            if ((await page.QuerySelectorAsync("#truste-consent-button")) != null)
            {
                await page.ClickAsync("#truste-consent-button");
            }

            // Hover on Developers
            await page.HoverAsync("li[aria-labelledby='nav-item-Developers']");

            // Click on Quickstarts
            await page.ClickAsync("a[href='https://auth0.com/docs/quickstarts/']");

            // Hover on Regular Web App
            await page.HoverAsync("div[data-type='webapp']");

            // Click on Launch Quickstart
            await page.ClickAsync("div[data-type='webapp'] button.btn-success");

            // Search for ASP.NET Core
            await page.TypeAsync("div.quickstart-search-input input", "ASP.NET Core");

            // Click on ASP.NET Core logo
            await page.ClickAsync("div[data-name='aspnet-core']");

            // Look for "View on Github" button and get the href property
            var gitHubLink = await page.EvalOnSelectorAsync<string>("a.btn-transparent.btn-block", "e => e.getAttribute('href')");

            Assert.AreEqual(gitHubLink, "https://github.com/auth0-samples/auth0-aspnetcore-mvc-samples/tree/master/Quickstart/01-Login");
        }
    }
}
