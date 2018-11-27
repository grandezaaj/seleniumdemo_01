using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DevCon2018SeleniumDemo
{
    public class Program
    {
        static void Main(string[] args)
        {

            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl("http://localhost/redknight/#/login");
            driver.Manage().Window.Maximize();
            //driver.SwitchTo().ActiveElement();

            var username = driver.FindElement(By.Id("username"));
            username.SendKeys("admin");

            var password = driver.FindElement(By.Id("password"));
            password.SendKeys("admin123");

            var loginButton = driver.FindElement(By.XPath(@"//*[@id=""wrap""]/div/div[2]/div[1]/button"));
            loginButton.Click();
            
            Thread.Sleep(3000);

            var guestLink = driver.FindElement(By.XPath(@"//*[@id=""sidebar""]/li[5]/a"));
            guestLink.Click();

            Thread.Sleep(5000);

            driver.Navigate().Back();

            Thread.Sleep(5000);

            driver.Navigate().Refresh();


            Console.ReadKey();


        }
    }
}
