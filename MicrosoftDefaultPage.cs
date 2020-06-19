using ChallengeMicrosoft.PageObjetcs;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace ChallengeMicrosoft
{
    class MicrosoftDefaultPage : Utilities
    {
        static IWebDriver driver;

        public MicrosoftDefaultPage() {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--lang=en-US.");
            driver = new ChromeDriver(options); 
        }
        

        [SetUp]
        public void Setup()
        {
            PrintToConsole("----------Start Setup--------------");
            string urlConfig = LoadConfig();
            driver.Navigate().GoToUrl(urlConfig);
            Thread.Sleep(3000);
            PrintToConsole("-----------Start of test-----------");
        }

        [TearDown]
        public void TeadrDown()
        {
            PrintToConsole("-----------End of test-----------");
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test]
        public void MenusInDefaultPage()
        {
            var homePage = new HomePage(driver);
            Assert.True(homePage.MenuIsDisplayed());
        }

        [Test]
        public void Windows10MenuInWindowsPage()
        {
            var homePage = new HomePage(driver);
            homePage.GoToWindowsMenu();
            var windowsPage = new WindowsPage(driver);
            windowsPage.Windows10MenuClick();
            Assert.True(windowsPage.PrintAllWindows10DropdownList());
        }

        [Test]
        public void SeachAndAddToCartVisualStudio()
        {
            var homePage = new HomePage(driver);
            homePage.GoToSearchButton();
            homePage.SearchVisualStudio();
            Assert.True(homePage.PrintFirst3Results());
            homePage.ClickOnFirstResult();
            Assert.True(homePage.VerifyDetailPriceMatch());
            homePage.AddToCart();
            Assert.True(homePage.Verify3PricesAtCart());
            homePage.IncreaseNumberOfItems();
            Assert.True(homePage.VerifyTotalPriceNumOfUnits());
        }

        public override Boolean MenuIsDisplayed()
        {
            Boolean result = false;
            //for futher implementations;
            return result;
        }
    }
}
