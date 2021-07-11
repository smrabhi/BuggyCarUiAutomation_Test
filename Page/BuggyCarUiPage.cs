using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BuggyCarsUiAutomation.Page
{
    class BuggyCarUiPage
    {

        public RemoteWebDriver _driver;

        public BuggyCarUiPage(RemoteWebDriver driver) => _driver = driver;

        IWebElement textUserName => _driver.FindElementByName("login");
        IWebElement textPassword => _driver.FindElementByName("password");
        IWebElement buttonLogin => _driver.FindElementByCssSelector("button.btn:nth-child(2)");

        IWebElement buttonLogout => _driver.FindElementByPartialLinkText("Logout");

        IWebElement profile => _driver.FindElementByXPath("//a[contains(@href,'/profile')]");

        IWebElement profilePageBasic => _driver.FindElementByXPath("//h3[contains(., 'Basic')]");

        WebDriverWait wait;

        IWebElement buttonRegister => _driver.FindElementByXPath("//a[contains(@href,'/register')]");
        IWebElement registerPageTitle => _driver.FindElementByXPath("//h2[contains(., 'Register with Buggy Cars Rating')]");

        IWebElement newUserName => _driver.FindElementById("username");
        IWebElement firstName => _driver.FindElementById("firstName");

        IWebElement lastName => _driver.FindElementById("lastName");

        IWebElement newPassword => _driver.FindElementById("password");

        IWebElement confirmPassword => _driver.FindElementById("confirmPassword");

        IWebElement buttonSubmitRegister => _driver.FindElementByCssSelector("button.btn:nth-child(6)");


        IWebElement registrationSuccess => _driver.FindElementByCssSelector(".result");


        IWebElement popularMake => _driver.FindElementByCssSelector("img[title='Lamborghini']");

        IWebElement popularMakePageTitle => _driver.FindElementByXPath("//h3[contains(., 'Lamborghini')]");

        IWebElement popularModel => _driver.FindElementByCssSelector("img[title='Diablo']");

        IWebElement popularModelPageTitle => _driver.FindElementByXPath("//h3[contains(., 'Lamborghini')]");

        IWebElement overallRating => _driver.FindElementByCssSelector("img[src='/img/overall.jpg']");

        IWebElement overallRatingPageTitle => _driver.FindElementByXPath("//a[contains(., 'Make')]");














        public void GoToApplicationHomepage(string applicationUrl)
        {
            _driver.Navigate().GoToUrl(applicationUrl);
            wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public void EnterUserNameAndPassword(string userName, string password)
        {
            textUserName.SendKeys(userName);
            textPassword.SendKeys(password);
        }


        public void ClickLogin()
        {
            buttonLogin.Click();
        }

        public void VerifyLoginPage()
        {
            //IWebElement profile1 = wait.Until(e => e.profile.Displayed));
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            Assert.That(profile.Displayed, Is.True, "Correct input, can login");

        }
        public void ClickLogout()
        {
            buttonLogout.Click();
        }
        public void VerifyProfilePage()
        {
            profile.Click();
            Assert.That(profilePageBasic.Displayed, Is.True, "Profile displayed");

        }

        public void VerifyLogout()
        {
            Assert.That(buttonLogin.Displayed, Is.True, "Logout success");


        }

        public void VerifyProfileAfterLogout()
        {

            Assert.That(profilePageBasic.Displayed, Is.False, "Profile closed");

        }

        public void ClickRegister()
        {
            buttonRegister.Click();
        }

        public void VerifyRegisterPage()
        {

            Assert.That(registerPageTitle.Displayed, Is.True, "Registration Page launched");

        }

        public void EnterUserDetail(string login, string firstname, string lastname, string password)
        {
            string loginUsername = login + DateTime.Now.ToString("dd_MM_yy_HH_mm_ss");
            newUserName.SendKeys(loginUsername);
            firstName.SendKeys(firstname);
            lastName.SendKeys(lastname);
            newPassword.SendKeys(password);
            confirmPassword.SendKeys(password);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        }

        public void ClickSubmitRegister()
        {
            buttonSubmitRegister.Click();
        }

        public void VerifyRegistrationSuccess()
        {
            Assert.That(registrationSuccess.Displayed, Is.True, "Registration successful");
        }


        public void PopularMakeClick()
        {
            popularMake.Click();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
        }

        public void VerifyPopularMakePage()

        {
            Assert.That(popularMakePageTitle.Displayed, Is.True, "Popular Make launched successfully");
        }

        public void PopularModelClick()
        {
            popularModel.Click();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
        }

        public void VerifyPopularModelPage()

        {
            Assert.That(popularModelPageTitle.Displayed, Is.True, "Popular Model launched successfully");
        }

        public void OverallRatingClick()
        {
            overallRating.Click();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        }

        public void VerifyOverallRatingPage()

        {
            Assert.That(overallRatingPageTitle.Displayed, Is.True, "Overall rating page launched successfully");
        }


    }
}
