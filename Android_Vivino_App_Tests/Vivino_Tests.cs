using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Linq;

namespace Android_Vivino_App_Tests
{
    public class Vivino_Tests
    {
        private const string AppiumServerUrl = "http://[::1]:4723/wd/hub";
       // private const string VivinoAppPath = @"D:\QaAutomationSoftUny\AppiumExercise\vivino_8.18.11-8181203.apk";
        private const string ViviniAppPackage = "vivino.web.app";
        private const string VivinoAppStsrtActivity = "com.sphinx_solution.activities.SplashActivity";
        private const string VivinoTestAccountEmail = "bob_4o@yahoo.com";
        private const string VivinoTetsAccountPasword = "BlaGoSimov";

        private AndroidDriver<AndroidElement> driver;

       [OneTimeSetUp]
        public void Setup()
        {
            var appiumOptions = new AppiumOptions() { PlatformName = "Android" };
           // appiumOptions.AddAdditionalCapability("app", VivinoAppPath);
            appiumOptions.AddAdditionalCapability("appPackage", ViviniAppPackage);
            appiumOptions.AddAdditionalCapability("appActivity", VivinoAppStsrtActivity);
            driver = new AndroidDriver<AndroidElement>(new Uri(AppiumServerUrl), appiumOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            
        }

        [Test]
        public void SearchForCurrentWine()
        {

            var loginLink = driver.FindElementById("vivino.web.app:id/txthaveaccount");
            loginLink.Click();

            var textBoxLoginEmal = driver.FindElementById("vivino.web.app:id/edtEmail");
            textBoxLoginEmal.SendKeys(VivinoTestAccountEmail);

            var textBoxLoginPassword = driver.FindElementById("vivino.web.app:id/edtPassword");
            textBoxLoginPassword.SendKeys(VivinoTetsAccountPasword);

            var loginButton = driver.FindElementById("vivino.web.app:id/action_signin");
            loginButton.Click();

            var explorerTab = driver.FindElementById("vivino.web.app:id/wine_explorer_tab");
            explorerTab.Click();

            var searchBoxButton = driver.FindElementById("vivino.web.app:id/search_vivino");
            searchBoxButton.Click();

            var searchBox = driver.FindElementById("vivino.web.app:id/editText_input");
            searchBox.SendKeys("Katarzyna Mezzek Mavrud");

            var searchButton = driver.FindElementById("vivino.web.app:id/imageView_arrow_back");
            searchButton.Click();

       
            var currentElement = driver.FindElementByXPath("//*[@resource-id='vivino.web.app:id/listviewWineListActivity']" +
              "//android.widget.ImageView[1]");
            currentElement.Click();

            var elementWinename = driver.FindElementById("vivino.web.app:id/wine_name");
            Assert.AreEqual("Mezzek Mavrud", elementWinename.Text);

            var elementrating = driver.FindElementById("vivino.web.app:id/rating");
            double rating = double.Parse(elementrating.Text);
            Assert.IsTrue(rating >= 3.00 && rating <= 5.00);

            var highlifthsDescription = driver.FindElementByAndroidUIAutomator
                            ("new UiScrollable(new UiSelector().scrollable(true))" +
                             ".scrollIntoView(new UiSelector().resourceIdMatches(" +
                              "\"vivino.web.app:id/highlight_description\"))");
            Assert.AreEqual("Among top 9% of all wines in the world", highlifthsDescription.Text);

           
        }

        [OneTimeTearDown]
        public void ShutDown()
        {
            driver.Quit();
        }
    }
}