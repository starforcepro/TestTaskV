using CareersTests.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace CareersTests.Pages
{
    public class BasePage
    {
        protected readonly IWebDriver driver;
        protected IJavaScriptExecutor jsExecutor;

        public BasePage()
        {
            driver = DriverController.GetCurrentDriverInstance();
            jsExecutor = (IJavaScriptExecutor) driver;

            InitPage(this);
        }

        protected T InitPage<T>(T page)
            where T : BasePage
        {
            PageFactory.InitElements(driver, page);

            return page;
        }
    }
}