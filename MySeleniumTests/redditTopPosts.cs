using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace MySeleniumTests
{
    class redditTopPosts
    {
        class RedditTopPosts
        {
            IWebDriver driver;

            [Test]
            public void redditTopPosts()
            {
                // disable browser notifications
                ChromeOptions option = new ChromeOptions();
                option.AddArguments("disable-notifications");

                // open page and maximize window
                driver = new ChromeDriver(option);
                driver.Url = "https://old.reddit.com/";
                driver.Manage().Window.Maximize();
                Thread.Sleep(1000);


                IList<IWebElement> postContainers = driver.FindElements(By.CssSelector("[data-subreddit-type='public']"));  // << post container


                List<Tuple<string, int>> mylist = new List<Tuple<string, int>>();  // << list to store stings and ints

                for (int i = 0; i < postContainers.Count; i++)
                {
                    IWebElement redditPost = postContainers[i];


                    int titlePoints;
                    string titlePostName = redditPost.FindElement(By.CssSelector(".title.may-blank")).Text;

                    // if post has no points add 0 as default value
                    try
                    {
                        titlePoints = Int32.Parse(redditPost.FindElement(By.CssSelector(".score.unvoted")).GetAttribute("title"));
                    }
                    catch
                    {
                        titlePoints = 0;
                    }

                    mylist.Add(new Tuple<string, int>(titlePostName, titlePoints)); // << items added to list
                }

                mylist = mylist.OrderByDescending(t => t.Item2).ToList();       // list is sorted my Items2 in list (int)


                for (int i = 0; i < 5; i++)
                {
                    Tuple<string, int> listItems = mylist[i];
                    Console.WriteLine("Post name : {0} \nPost Points: {1}\n ", listItems.Item1, listItems.Item2);
                }


                driver.Quit();
            }
        }
    }
}
