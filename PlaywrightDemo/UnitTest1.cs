using NUnit.Framework;
using Microsoft.Playwright;
using System.Threading.Tasks;
using PlaywrightDemo.Pages;

namespace PlaywrightDemo
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public async Task Test1()
        {
            using var playwright = await Playwright.CreateAsync();
            
            //browser
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { 
                Headless = false
            });

            //Page
            var page = await browser.NewPageAsync();

            await page.GotoAsync("http://www.eaapp.somee.com");
            await page.ClickAsync("text='Login'");

            await page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = "EAApp.jpg"
            });

            await page.FillAsync("#UserName", "admin");
            await page.FillAsync("#Password", "password");
            await page.ClickAsync("text='Log in'");
            bool isExist = await page.Locator("text='Employee Details'").IsVisibleAsync();
            Assert.IsTrue(isExist);
        }

        [Test]
        public async Task TestWithPOM()
        {
            using var playwright = await Playwright.CreateAsync();
            
            //browser
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { 
                Headless = false
            });

            //Page
            var page = await browser.NewPageAsync();

            await page.GotoAsync("http://www.eaapp.somee.com");
            
            //LoginPage loginPage = new LoginPage(page);
            var loginPage = new LoginPageUpgraded(page);
            await loginPage.ClickLogin();
            await loginPage.Login("admin","password");

            var isExist = await loginPage.IsEmployeeDetailsExists();
            Assert.IsTrue(isExist);
        }
    }
}