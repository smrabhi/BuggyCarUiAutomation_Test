using BuggyCarsUiAutomation.Page;
using OpenQA.Selenium.Remote;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace BuggyCarsUiAutomation.Steps
{
    [Binding]
    public class BuggyCarUiSteps
    {

        BuggyCarUiPage currentPage;
        RemoteWebDriver _driver;
        dynamic data;

        public BuggyCarUiSteps(RemoteWebDriver driver, ScenarioContext injectedContext)
        {
            _driver = driver;
            currentPage  = new BuggyCarUiPage(_driver);
        }


        [Given(@"I go to application url  '(.*)'")]
        public void GivenIGoToApplicationUrl(string url)
        {
           
            currentPage.GoToApplicationHomepage(url);
        }


        [When(@"I enter username and Password")]
        public void WhenIEnterUsernameAndPassword(Table table)
        {
            data = table.CreateDynamicInstance();
            currentPage.EnterUserNameAndPassword((string)data.Username, (string)data.Password);
        }


        [When(@"I press Login button")]
        public void WhenIPressLoginButton()
        {
            currentPage.ClickLogin();
        }


        [Then(@"I can see login page")]
        public void ThenICanSeeLoginPage()
        {
            
            currentPage.VerifyProfilePage();
        }


        [Then(@"I can see profile page")]
        public void ThenICanSeeProfilePage()
        {
            currentPage.VerifyLoginPage();
        }


        [Then(@"I logout from Application")]
        public void ThenILogoutFromApplication()
        {
            currentPage.ClickLogout();
            currentPage.VerifyLogout();

;       } 
        

        [Then(@"I should not see profile details")]
        public void ThenIShouldNotSeeProfileDetails()
        {
           currentPage.VerifyProfileAfterLogout();
        }


        [Given(@"I click on register")]
        public void GivenIClickOnRegister()
        {
            currentPage.ClickRegister();
        }

        
        [When(@"I can see register page")]
        public void WhenICanSeeRegisterPage()
        {
            currentPage.VerifyRegisterPage();
        }
        
        [When(@"I fill all details")]
        public void WhenIFillAllDetails(Table table)
        {
            data = table.CreateDynamicInstance();
            currentPage.EnterUserDetail((string)data.Login, (string)data.FirstName, (string)data.LastName, (string)data.Password);
        }
        
        [When(@"I press register button")]
        public void WhenIPressRegisterButton()
        {
            currentPage.ClickSubmitRegister();
        }

        [Then(@"I can see success message")]
        public void ThenICanSeeSuccessMessage()
        {
            currentPage.VerifyRegistrationSuccess();
        }


        [When(@"I click on Lamborghini under popular make")]
        public void WhenIClickOnLamborghiniUnderPopularMake()
        {
            currentPage.PopularMakeClick();
        }

        [Then(@"I can see Lamborghini page")]
        public void ThenICanSeeLamborghiniPage()
        {
            currentPage.VerifyPopularMakePage();
        }

        [When(@"I click on Popular Model")]
        public void WhenIClickOnPopularModel()
        {
            currentPage.PopularModelClick();
        }

        [Then(@"I can see Popular Model Page")]
        public void ThenICanSeePopularModelPage()
        {
            ScenarioContext.Current.Pending(); // as this is not implemented in Application
            //currentPage.VerifyPopularModelPage(); //this can be found in page file
        }  
            

        [When(@"I click on overall rating")]
        public void WhenIClickOnOverallRating()
        {
            currentPage.OverallRatingClick();
        }

        [Then(@"I can see overall rating page")]
        public void ThenICanSeeOverallRatingPage()
        {
            currentPage.VerifyOverallRatingPage();
        }
        
        [Then(@"I can see Different models on  the page")]
        public void ThenICanSeeDifferentModelsOnThePage()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
