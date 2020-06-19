
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace ChallengeMicrosoft
{
    abstract class Utilities : takeScreenshot
    {
        static By cancelRedirectId = By.Id("R1MarketRedirect-close");
        static By cancelSingUpAlterId = By.CssSelector("#email-newsletter-dialog > div.sfw-dialog > div.c-glyph.glyph-cancel");

        public abstract Boolean MenuIsDisplayed();

        public string LoadConfig()
        {
            string ulrReturn = null;
            string workingDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string pathJson = workingDir + "\\Config\\URLconfig.json";
            PrintToConsole("Current dir: " + workingDir);
            PrintToConsole("Json path: " + pathJson);
            StreamReader r = new System.IO.StreamReader(pathJson);
            string json = r.ReadToEnd();
            PrintToConsole(json);

            var objects = JArray.Parse(json); // parse as array  
            foreach (JObject root in objects)
            {
                foreach (KeyValuePair<String, JToken> url in root)
                {
                    var urlConfig = url.Value;
                    ulrReturn = urlConfig.ToString();
                    PrintToConsole("URL config: "+ urlConfig.ToString());
                }
            }
            return ulrReturn;
        }

        public void PrintToConsole(string text)
        {
            TestContext.Progress.WriteLine(text);
        }

        public void CancelRedirect(IWebDriver driver)
        {
            try
            {
                if (driver.FindElement(cancelRedirectId).Displayed)
                {
                    driver.FindElement(cancelRedirectId).Click();
                    PrintToConsole("Cancelling redirect to other region.");
                }
            }
            catch
            {
                PrintToConsole("No need to cancel redirect.");
            }
        }

        public void CancelSingUpAlert(IWebDriver driver)
        {
            try
            {
                string MainWindow = driver.CurrentWindowHandle;
                if (driver.FindElement(cancelSingUpAlterId).Displayed)
                {
                    driver.FindElement(cancelSingUpAlterId).Click();
                    PrintToConsole("Cancelling signup email alert.");
                }
                driver.SwitchTo().Window(MainWindow);
            }
            catch
            {
                PrintToConsole("No need to signup email alert");
            }
            Thread.Sleep(2000);
        }

        public void SaveScreenshot(IWebDriver driver)
        {
            string filename = "screenshot_" + GetTimestamp(DateTime.Now) + ".png";
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            ss.SaveAsFile(filename);
        }

        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyy_MM_dd_HHmmss");
        }
    }

    public interface takeScreenshot
    {
        public void SaveScreenshot(IWebDriver driver);
    }
}