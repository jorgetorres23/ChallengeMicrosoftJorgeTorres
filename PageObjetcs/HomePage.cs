using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Threading;

namespace ChallengeMicrosoft.PageObjetcs
{
    class HomePage : Utilities
    {
        IWebDriver driver;
        private static string firstPriceStored;
        const string searchWord = "Visual Studio";

        //menu items
        static By menuOfficeId =   By.Id("shellmenu_1");
        static By menuWindowsId =  By.Id("shellmenu_2");
        static By menuSurfaceId =  By.Id("shellmenu_3");
        static By menuXboxId =     By.Id("shellmenu_4");
        static By menuDealsId =    By.Id("shellmenu_5");
        static By menuSupportId =  By.Id("l1_support");
        static By searchButtonId = By.Id("search");
        //search results items
        static By firstResultButton = By.CssSelector("#coreui-productplacement-30l7ywa_dg7gmgf0dst3 > div > a");
        static By searchTextFieldId =    By.Id("cli_shellHeaderSearchInput");
        static By firstResultNameCss =   By.CssSelector("#coreui-productplacement-30l7ywa_0 > h3");
        static By firstResultPriceCss =  By.CssSelector("#coreui-productplacement-30l7ywa_0 > div.c-channel-placement-price > div > span:nth-child(3) > span:nth-child(1)");
        static By secondResultNameCss =  By.CssSelector("#coreui-productplacement-30l7ywa_1 > h3");
        static By secondResultPriceCss = By.CssSelector("#coreui-productplacement-30l7ywa_1 > div.c-channel-placement-price > div > span:nth-child(3) > span:nth-child(1)");
        static By thirdResultNameCss =   By.CssSelector("#coreui-productplacement-30l7ywa_2 > h3");
        static By thirdResultPriceCss =  By.CssSelector("#coreui-productplacement-30l7ywa_2 > div.c-channel-placement-price > div > span:nth-child(3) > span:nth-child(1)");
        //details items
        static By detailPriceCss = By.CssSelector("#ProductPrice_productPrice_PriceContainer > span");
        static By selecRenewCss = By.ClassName("c-select-button");
        static By overviewCss = By.Id("pivot-tab-TechSpecsTab");
        static By addToCartBarId = By.Id("buttonsPageBar_AddToCartButton");
        static By addToCartPanelId = By.Id("buttonPanel_AddToCartButton");
        //cart items
        static By priceByUnitCss =   By.CssSelector("#store-cart-root > div > div > div > section._3a6I5TkT > div > div > div > div > div > div.item-details > div.item-quantity-price > div.item-price > div > span > span:nth-child(3) > span");
        static By priceSubTotalCss = By.CssSelector("#store-cart-root > div > div > div > section._3LWrsBIG > div > div > div:nth-child(2) > div > span:nth-child(1) > span:nth-child(2) > div > span > span:nth-child(3) > span");
        static By priceTotalCss =    By.CssSelector("#store-cart-root > div > div > div > section._3LWrsBIG > div > div > div:nth-child(4) > div > span > span:nth-child(2) > strong > span");
        static By numberOfUnitsCss = By.CssSelector("#store-cart-root > div > div > div > section._3a6I5TkT > div > div > div > div > div > div.item-details > div.item-quantity-price > div.item-quantity > select > option:nth-child(20)");
        const int numItems = 20;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void GoToWindowsMenu()
        {
            driver.FindElement(menuWindowsId).Click();
            Thread.Sleep(3000);
            SaveScreenshot(driver);
        }

        public void GoToSearchButton()
        {
            driver.FindElement(searchButtonId).Click();
            Thread.Sleep(2000);
            SaveScreenshot(driver);
        }

        public void SearchVisualStudio()
        {
            IWebElement searchTextField =  driver.FindElement(searchTextFieldId);
            searchTextField.SendKeys(searchWord);
            searchTextField.SendKeys(Keys.Enter);
            Thread.Sleep(2000);
        }

        public Boolean PrintFirst3Results()
        {
            Boolean result = false;
            try
            {
                PrintToConsole("--------Price Elements from Search Result---------");
                IWebElement firstName = driver.FindElement(firstResultNameCss);
                IWebElement firstPrice = driver.FindElement(firstResultPriceCss);
                IWebElement secondName = driver.FindElement(secondResultNameCss);
                IWebElement secondPrice = driver.FindElement(secondResultPriceCss);
                IWebElement thirdName = driver.FindElement(thirdResultNameCss);
                IWebElement thirdPrice = driver.FindElement(thirdResultPriceCss);
                PrintToConsole(firstName.Text + "  " + firstPrice.Text);
                PrintToConsole(secondName.Text + "  " + secondPrice.Text);
                PrintToConsole(thirdName.Text + "  " + thirdPrice.Text);
                firstPriceStored = firstPrice.Text;
                result = true;
            }
            catch
            {
                PrintToConsole("ERROR when printing the 3 first search results!");
                result = false;
            }
            return result;
        }

        public void ClickOnFirstResult()
        {
            CancelRedirect(driver);
            driver.FindElement(firstResultButton).Click();
            Thread.Sleep(3000);
            CancelSingUpAlert(driver);
            Thread.Sleep(4000);
            SaveScreenshot(driver);
        }

        public Boolean VerifyDetailPriceMatch()
        {
            Boolean result = false;
            string detailtPrice = driver.FindElement(detailPriceCss).Text;
            PrintToConsole("First price: " + firstPriceStored);
            PrintToConsole("Details price: " + detailtPrice);
            if (detailtPrice.Equals(firstPriceStored))
            {
                result = true;
            }
            Thread.Sleep(2000);
            SaveScreenshot(driver);
            return result;
        }

        public void AddToCart()
        {
            driver.FindElement(selecRenewCss).Click();
            Thread.Sleep(2000);
            driver.FindElement(overviewCss).Click();
            Thread.Sleep(2000);
            SaveScreenshot(driver);
            try
            {
                driver.FindElement(addToCartBarId).Click();
            }catch{
                try
                {
                    PrintToConsole("Error when click on add to cart Bar");
                    driver.FindElement(addToCartBarId).Click();
                }
                catch
                {
                    PrintToConsole("Error when click on add to cart Panel");
                }
            }
            Thread.Sleep(4000);
        }

        public Boolean Verify3PricesAtCart()
        {
            Boolean result = false;
            IWebElement priceByUnit = driver.FindElement(priceByUnitCss);
            IWebElement priceSubtotal = driver.FindElement(priceSubTotalCss);
            IWebElement priceTotal = driver.FindElement(priceTotalCss);

            PrintToConsole("--------Prices at Cart---------");
            PrintToConsole("Price by Unit: " + priceByUnit.Text);
            PrintToConsole("Price Subtotal: " + priceSubtotal.Text);
            PrintToConsole("Price Total: " + priceTotal.Text);

            if(priceByUnit.Text.Equals(priceSubtotal.Text))
            {
                if(priceSubtotal.Text.Equals(priceTotal.Text))
                {
                    PrintToConsole("Prices match.");
                    result = true;
                }
                else
                {
                    PrintToConsole("ERROR, Price does not match.");
                }
            }else
            {
                PrintToConsole("ERROR, Price does not match.");
            }
            Thread.Sleep(3000);
            return result;
        }

        public void IncreaseNumberOfItems()
        {
            driver.FindElement(numberOfUnitsCss).Click();
            Thread.Sleep(3000);
        }

        public Boolean VerifyTotalPriceNumOfUnits()
        {
            Boolean result = false;
            IWebElement priceByUnit = driver.FindElement(priceByUnitCss);
            IWebElement priceSubtotal = driver.FindElement(priceSubTotalCss);
            IWebElement priceTotal = driver.FindElement(priceTotalCss);

            PrintToConsole("--------Prices for 20 Items at Cart---------");
            PrintToConsole("Price by Unit: " + priceByUnit.Text);
            PrintToConsole("Price Subtotal: " + priceSubtotal.Text);
            PrintToConsole("Price Total: " + priceTotal.Text);

            float calculatedValue = float.Parse((priceByUnit.Text).Substring(1));
            calculatedValue *= numItems;

            float actualValue = float.Parse((priceTotal.Text).Substring(1));

            PrintToConsole("--------20 Items Total---------");
            PrintToConsole("Expected: $" + calculatedValue.ToString("0.00"));
            PrintToConsole("Actual: $" + actualValue.ToString("0.00"));
            if ((priceByUnit.Text).Equals(firstPriceStored))
            {
                if ((priceSubtotal.Text).Equals(priceTotal.Text))
                {
                    if (actualValue == calculatedValue)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public override Boolean MenuIsDisplayed()
        {
            Boolean result = false;
            IWebElement menuOffice = driver.FindElement(menuOfficeId);
            IWebElement menuWindows = driver.FindElement(menuWindowsId);
            IWebElement menuSurface = driver.FindElement(menuSurfaceId);
            IWebElement menuXbox = driver.FindElement(menuXboxId);
            IWebElement menuDeals = driver.FindElement(menuDealsId);
            IWebElement menuSupport = driver.FindElement(menuSupportId);
            if (menuOffice.Displayed && menuWindows.Displayed && menuSurface.Displayed &&
                menuXbox.Displayed && menuDeals.Displayed && menuSupport.Displayed)
            {
                result = true;
            }
            else
            {
                PrintToConsole("At least one element of the menu is not Displayed!");
            }
            return result;
        }
    }
}
