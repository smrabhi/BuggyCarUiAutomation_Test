using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;

namespace BuggyCarsUiAutomation.Hooks
{
    [Binding]
    public sealed class Hooks1
    {

        private static IObjectContainer _objectContainer;
        private readonly ScenarioContext _scenarioContext;

        private static RemoteWebDriver _driver;

        private static string outputFolder;

        //Global Variable for Extend report
        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private static ExtentReports extent;

        

        private Screenshot screenshots;



        public Hooks1(IObjectContainer objectContainer, ScenarioContext injectedContext)
        {
            _objectContainer = objectContainer;
            _scenarioContext = injectedContext;

        }


        [BeforeTestRun]
        public static void InitializeReport()
        {
            //Initialize Extent report before test starts

            string workingDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            outputFolder = Path.Combine(workingDirectory + "Output\\");
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);
            /*else
            {
                Directory.Delete(outputFolder, true);
                Directory.CreateDirectory(outputFolder);
            }*/

            var htmlReporter = new ExtentHtmlReporter(outputFolder + "OutputReport.html");
            extent = new ExtentReports();

            //Attach report to reporter
            extent.AttachReporter(htmlReporter);
        }


        [BeforeScenario]
        public void BeforeScenario()
        {
            InitializeDriver();
            
            //Create dynamic scenario name
            scenario = featureName.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
        }

        [AfterScenario]
        public void AfterScenario()
        {
         
            if (_driver != null)
            {
                _driver.Quit();
                _driver = null;
            }
        }

        public static void InitializeDriver()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            _driver.Manage().Window.Maximize();
            _objectContainer.RegisterInstanceAs<RemoteWebDriver>(_driver);           
        }


        [AfterTestRun]
        public static void AfterTestRun()
        {
            //Flush report once test completes
            if (extent != null)
            {
                extent.Flush();
                extent = null;
            }       
            
        }


        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            //Create dynamic feature name
            featureName = extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }

        [AfterStep]
        public void InsertReportingSteps()
        {
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();

            PropertyInfo pInfo = typeof(ScenarioContext).GetProperty("ScenarioExecutionStatus", BindingFlags.Instance | BindingFlags.Public);
            MethodInfo getter = pInfo.GetGetMethod(nonPublic: true);
            object TestResult = getter.Invoke(_scenarioContext, null);

            if (_scenarioContext.TestError == null)
            {

                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "And")
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
            }
            else if (_scenarioContext.TestError != null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.InnerException);
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.InnerException);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
                else if (stepType == "And")
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message);

                //capture screenshot if fail
                takeScreenshots(_scenarioContext.ScenarioInfo.Title);
            }

            //Pending Status
            if (TestResult.ToString() == "StepDefinitionPending")
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");

            }
        }


        void takeScreenshots(string sName)
        {
            screenshots = ((ITakesScreenshot)_driver).GetScreenshot();
            string cas = DateTime.Now.ToString("dd_MM_yy_HH_mm_ss");
            screenshots.SaveAsFile(outputFolder + sName + cas + ".jpeg", ScreenshotImageFormat.Jpeg);
        }
    }

}
