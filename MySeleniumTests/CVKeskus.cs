using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace MySeleniumTests
{
    class CVKeskus
    {
        IWebDriver m_driver;

        [Test]
        public void cvKeskus()
        {
            m_driver = new ChromeDriver();
            m_driver.Url = "https://www.cvkeskus.ee/";
            m_driver.Manage().Window.Maximize();

            Thread.Sleep(1000);

            // click Kategooria window
            m_driver.FindElement(By.XPath("//*[@id='job_categories_top']/a[1]")).Click();

            Thread.Sleep(1000);
            // click infotehnoloogia option
            m_driver.FindElement(By.XPath("//*[@id='job_categories_top']/span/a[7]")).Click();

            Thread.Sleep(1000);
            // click search button
            m_driver.FindElement(By.XPath("/html/body/div[1]/form/div/div[1]/div[1]/div[3]/div/span[4]/button")).Click();



            List<String> allnames = new List<String>();

            while (true)
            {
                Thread.Sleep(1000);
                // find all elements containing job names
                IList<IWebElement> jobnames = m_driver.FindElements(By.CssSelector(".hidden-xs-down a.f_job_title"));

                for (int i = 0; i < jobnames.Count; i++)
                {
                    allnames.Add(jobnames[i].Text);
                }

                // Find all pagination buttons
                IList<IWebElement> buttons = m_driver.FindElements(By.XPath("//ul[@class='pager_xs pagination']//li[@class='hidden-xs-down' or @class='']/a[not(@class='next') and not(@class='previous')]"));
                // Find current pagination button
                IWebElement currentPageButton = m_driver.FindElement(By.ClassName("act"));

                int biggest = 0;



                foreach (IWebElement item in buttons)
                {
                    int toInt = Int32.Parse(item.Text);
                    if (toInt > biggest)
                    {
                        biggest = toInt;
                    }
                }

                int currentNumber = Int32.Parse(currentPageButton.Text);


                // If current button is biggest then stop else push next button
                if (currentNumber < biggest)
                {
                    m_driver.FindElement(By.ClassName("next")).Click();
                }
                else
                {
                    break;
                }

            }


            // Write result to desktop
            System.IO.File.WriteAllLines(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @".\\CVKeskusJobNamesList.txt", allnames);


            m_driver.Close();
        }
    }
}
