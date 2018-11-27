using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Linq;
using System.Threading;

namespace Demo.Test
{
    [TestFixture]
    public class UnitTest1
    {
        IWebDriver driver;
        private string url = "";

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }


        [Test]
        [TestCase("admin", "admin123")]
        public void ValidLogin(string username, string password)
        {
            // Arrange
            this.GoToUrl(url);

            // Act
            var usernameElement = driver.FindElement(By.Id("username"));
            usernameElement.SendKeys(username);

            var passwordElement = driver.FindElement(By.Id("password"));
            passwordElement.SendKeys(password);

            var loginButton = driver.FindElement(By.XPath(@"//*[@id=""wrap""]/div/div[2]/div[1]/button"));
            loginButton.Click();

            // Assert
            Thread.Sleep(3000);
            Assert.IsTrue(driver.Url == "http://localhost/redknight/#/dashboard");
        }

        [Test]
        [TestCase("admin", "admin1234")]
        public void InvalidLogin(string username, string password)
        {
            // Arrange
            this.GoToUrl(url);

            // Act
            var usernameElement = driver.FindElement(By.Id("username"));
            usernameElement.SendKeys(username);

            var passwordElement = driver.FindElement(By.Id("password"));
            passwordElement.SendKeys(password);

            var loginButton = driver.FindElement(By.XPath(@"//*[@id=""wrap""]/div/div[2]/div[1]/button"));
            loginButton.Click();

            // Assert
            Thread.Sleep(1000);

            var alertElement = driver.FindElement(By.Id("alerts"));
            var alerts = alertElement.FindElements(By.ClassName("alert-danger"));
            Assert.IsTrue(alerts.Any());
            Assert.IsTrue(alerts[0].Text.Contains("The user name or password is incorrect."));
        }

        [Test]
        public void AddApartment_Invalid()
        {
            this.GoToUrl(url);
            this.Login("admin", "admin123");

            Thread.Sleep(5000);

            var apartmentMenu = driver.FindElement(By.XPath(@"//*[@id=""sidebar""]/li[8]/a/span[1]"));
            apartmentMenu.Click();

            var apartmentSubMenu = driver.FindElement(By.XPath(@"//*[@id=""sidebar""]/li[8]/ul/li[1]/a"));
            apartmentSubMenu.Click();

            Thread.Sleep(4000);

            var addButton = driver.FindElement(By.XPath(@"//*[@id=""wrap""]/div/div[1]/div/panel/div/div[1]/div/a"));
            addButton.Click();

            Thread.Sleep(4000);

            var saveButton = driver.FindElement(By.Id("btnSubmit"));
            saveButton.Click();

            var apartmentNameDiv = driver.FindElement(By.XPath(@"/html/body/div[4]/div/div/div/div[2]/form/div[1]/div/div[1]/div"));
            var apartmentNameValidationError = apartmentNameDiv.FindElement(By.TagName("p"));

            var apartmentTypeDiv = driver.FindElement(By.XPath(@"/html/body/div[4]/div/div/div/div[2]/form/div[1]/div/div[2]/div/div"));
            var apartmentTypeValidationError = apartmentTypeDiv.FindElement(By.TagName("p"));

            Thread.Sleep(7000);

            Assert.IsTrue(apartmentNameValidationError.Text.Contains("The ApartmentName field is required"));
            Assert.IsTrue(apartmentTypeValidationError.Text.Contains("The Apartment Type field is required"));

        }

        [TearDown]
        public void Close()
        {
            driver.Close();
        }

        private void GoToUrl(string url)
        {
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
            driver.SwitchTo().ActiveElement();
        }

        private void Login(string username, string password)
        {
            var usernameElement = driver.FindElement(By.Id("username"));
            usernameElement.SendKeys(username);

            var passwordElement = driver.FindElement(By.Id("password"));
            passwordElement.SendKeys(password);

            var loginButton = driver.FindElement(By.XPath(@"//*[@id=""wrap""]/div/div[2]/div[1]/button"));
            loginButton.Click();
        }
    }
}
