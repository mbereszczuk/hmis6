using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class ChangePasswordWrongOldPassword
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "https://www.katalon.com/";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void TheChangePasswordWrongOldPasswordTest()
        {
            driver.Navigate().GoToUrl("https://loginhmis2018vs2017.azurewebsites.net/");
            driver.FindElement(By.Id("loginLink")).Click();
            driver.FindElement(By.Id("Email")).Clear();
            driver.FindElement(By.Id("Email")).SendKeys("mb2@ual.es");
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("Abc123\"");
            driver.FindElement(By.XPath("//input[@value='Iniciar sesión']")).Click();
            driver.FindElement(By.LinkText("Hola mb2@ual.es!")).Click();
            driver.FindElement(By.LinkText("Cambiar la contraseña")).Click();
            driver.FindElement(By.Id("OldPassword")).Click();
            driver.FindElement(By.Id("OldPassword")).Clear();
            driver.FindElement(By.Id("OldPassword")).SendKeys("ABab12!!");
            driver.FindElement(By.Id("NewPassword")).Clear();
            driver.FindElement(By.Id("NewPassword")).SendKeys("NewPass1!");
            driver.FindElement(By.Id("ConfirmPassword")).Clear();
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys("NewPass1!");
            driver.FindElement(By.XPath("//input[@value='Cambiar contraseña']")).Click();
            Assert.AreEqual("Incorrect password.", driver.FindElement(By.XPath("//form/div/ul/li")).Text);
            driver.FindElement(By.LinkText("Cerrar sesión")).Click();
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
