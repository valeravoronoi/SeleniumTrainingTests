using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace MySeleniumTests
{
    class CVonline
    {
        IWebDriver driver;

        [Test]

        public void cvOnline()
        {
            // open page and maximize window
            driver = new ChromeDriver();
            driver.Url = "https://www.cv.ee/";
            driver.Manage().Window.Maximize();
            Thread.Sleep(1000);

            // close ad pop-up
            driver.FindElement(By.ClassName("close")).Click();

            // click search and input info
            driver.FindElement(By.XPath("//*[@id='cvo_masthead']/div/div/div/form/div[1]/div/span/span[1]/span/ul/li/input")).SendKeys("Infotehnoloogia" + Keys.Enter);
            Thread.Sleep(1000);

            // click search button
            driver.FindElement(By.Id("searchbutton")).Click();

            Thread.Sleep(1000);

            List<String> allnames = new List<String>();

            while (true)
            {
                Thread.Sleep(1000);
                // Find all job names
                IList<IWebElement> jobnames = driver.FindElements(By.XPath("//div[@class='offer_primary_info']/h2/a[@target='_blank'] | //div[@class='offer_company_premium']/h2/a[@target='_blank']"));

                for (int i = 0; i < jobnames.Count; i++)
                {
                    allnames.Add(jobnames[i].Text);
                }

                // Find "next button" Xpath
                var nextbuttonCount = driver.FindElements(By.XPath("//li[@class='page_next']/a")).Count();
                if (nextbuttonCount > 0)
                {
                    driver.FindElement(By.XPath("//li[@class='page_next']/a")).Click();
                }
                else
                {
                    break;
                }
            }

            System.IO.File.WriteAllLines(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @".\\CVOnlineJobNamesList.txt", allnames);

            driver.Quit();

        }
    }
}
