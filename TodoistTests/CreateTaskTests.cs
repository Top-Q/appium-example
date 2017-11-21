using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace TodoistTests
{
    [TestClass]
    public class CreateTaskTests
    {

        AndroidDriver<AppiumWebElement> driver;

        [TestInitialize]
        public void Setup()
        {
            DesiredCapabilities capabilities = new DesiredCapabilities();
            capabilities.SetCapability(MobileCapabilityType.AppiumVersion, "1.4.0");
            capabilities.SetCapability(MobileCapabilityType.PlatformName, "Android");
            capabilities.SetCapability(MobileCapabilityType.PlatformVersion, "5.1.0");
            capabilities.SetCapability(MobileCapabilityType.DeviceName, "Android");
            capabilities.SetCapability(MobileCapabilityType.App, "C:\\Users\\ITAI\\git\\Todoist\\TodoistTests\\Todoist_com.todoist.apk");
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
            Thread.Sleep(3000);
            Console.Write("Success");
        }

        [TestCleanup]
        public void Teardown()
        {
            driver.Quit();

        }
    }
}
