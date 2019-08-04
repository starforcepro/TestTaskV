using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace CareersTests.Core
{
    [Parallelizable(ParallelScope.Fixtures)]
    [Timeout(1000 * 60 * 5)]
    public class SeleniumTestBase
    {
        private IWebDriver driver;

        public const string testServer = "http://testServer/";

        [OneTimeSetUp]
        public void TestInitialize()
        {
            driver = new ChromeDriver();

            var threadId = Thread.CurrentThread.ManagedThreadId;
            DriverController.Add(threadId, driver);

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(30);
        }

        [OneTimeTearDown]
        public void TestCleanUp()
        {
            driver.Quit();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Manage().Cookies.DeleteAllCookies();
        }
    }}