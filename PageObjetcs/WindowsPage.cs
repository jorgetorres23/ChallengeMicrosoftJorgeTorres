using OpenQA.Selenium;
using System;
using System.Threading;

namespace ChallengeMicrosoft.PageObjetcs
{
    class WindowsPage : Utilities
    {
        IWebDriver driver;
        static int win10SelectorLenght = 10;
        static By menuWindows10Id = By.Id("c-shellmenu_50");
        static By[] menuW10SelectorId = new By[win10SelectorLenght];

        public Boolean MenuIsDisplayed(IWebElement element)
        {
            Boolean result = false;
            if (element.Displayed)
            {
                result = true;
            }
            else
            {
                PrintToConsole("Element is not Displayed!");
            }
            return result;
        }

        public WindowsPage(IWebDriver driver)
        {
            this.driver = driver;

            int index = 51;
            for(int i = 0; i < win10SelectorLenght; i++)
            {
                //selectors id from "c-shellmenu_51" to "c-shellmenu_60"
                menuW10SelectorId[i] = By.Id("c-shellmenu_" + index);
                //PrintToConsole("menuWin10: c-shellmenu_" + index);
                index++;
            }
        }


        public void Windows10MenuClick()
        {
            PrintToConsole("WIN10 menu text: " + driver.FindElement(menuWindows10Id).Text);
            driver.FindElement(menuWindows10Id).Click();
            Thread.Sleep(4000);
            SaveScreenshot(driver);
        }

        public Boolean PrintAllWindows10DropdownList()
        {
            Boolean result = false;
            try
            {
                IWebElement[] menuW10Selector = new IWebElement[win10SelectorLenght];
                PrintToConsole("--------Elements in the Win10 Selector Menu--------");
                for (int i = 0; i < win10SelectorLenght; i++)
                {
                    menuW10Selector[i] = driver.FindElement(menuW10SelectorId[i]);
                    PrintToConsole("Element text: " + menuW10Selector[i].Text);
                }
                PrintToConsole("--------------------------------------------------");
                result = true;
            }
            catch
            {
                PrintToConsole("ERROR when printing the Win10 Selector Elements!");
                result = false;
            }
            return result;
        }

        public override Boolean MenuIsDisplayed()
        {
            Boolean result = false;
            //for futher implementations;
            return result;
        }
    }
}
