using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;

namespace TodoistTests
{
    [TestClass]
    public class CreateTaskTests
    {
        private static String EMAIL = "itai.agmon@top-q.co.il";
	    private static String PASSWORD = "secret";

        AndroidDriver<AppiumWebElement> driver;

        [TestInitialize]
        public void Setup()
        {
            DesiredCapabilities capabilities = new DesiredCapabilities();
            capabilities.SetCapability(MobileCapabilityType.AppiumVersion, "1.4.0");
            capabilities.SetCapability(MobileCapabilityType.PlatformName, "Android");
            capabilities.SetCapability(MobileCapabilityType.PlatformVersion, "5.1.0");
            capabilities.SetCapability(MobileCapabilityType.DeviceName, "Android");
            string folder = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            capabilities.SetCapability(MobileCapabilityType.App, folder + @"\Todoist_com.todoist.apk");
            String activity = "com.todoist.activity.HomeActivity";
            capabilities.SetCapability("appPackage", "com.todoist");
            capabilities.SetCapability("appActivity", activity);
            capabilities.SetCapability("appWaitActivity", "com.todoist.activity.WelcomeActivity");
            driver = new AndroidDriver<AppiumWebElement>(new Uri("http://127.0.0.1:4723/wd/hub"), capabilities);
        }

        [TestMethod]
        public void TestCreateSimpleTask()
        {
            driver.FindElement(By.Id("btn_welcome_log_in")).Click();
            // *** Google Play Services Activity ***
            driver.FindElement(By.Id("button1")).Click();

            // *** Login Activity ****
            driver.FindElement(By.Id("log_in_email")).SendKeys(EMAIL);
            driver.FindElementById("log_in_password").SendKeys(PASSWORD);
            driver.FindElementById("btn_log_in").Click();

            // *** Google Play Services Activity ***
            driver.FindElementById("button1").Click();
            
            // *** Goolge Play services in not supported ***
            //driver.FindElementById("button1").Click();

            // Clicking on the Today menu item
            driver.FindElement(By.XPath("//android.widget.TextView[@text='Today']")).Click();

            // Creating a new task
            driver.FindElementById("fab").Click();
            String taskName = "Automated task" + DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            AppiumWebElement message = driver.FindElementById("message");
            message.SendKeys(taskName + " #work");
            driver.FindElementById("button1").Click();

            // Going back
            driver.FindElementById("action_mode_close_button").Click();



            // Searching for the newly created item
            ReadOnlyCollection<AppiumWebElement> items = driver.FindElements(By.XPath("//android.widget.RelativeLayout[@resource-id='com.todoist:id/item']/android.widget.TextView"));
            Boolean found = false;
            foreach (AppiumWebElement item in items)
            {
                Console.Write("Item: " + item.Text);
                if (item.Text.Equals(taskName))
                {
                    found = true;
                    break;
                }
            }            
            Assert.IsTrue(found, "Item was not created");


            Thread.Sleep(1000);
        }

        [TestCleanup]
        public void Teardown()
        {
            driver.Quit();

        }
    }
}
