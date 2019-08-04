using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace CareersTests.Pages
{
    public class CareersPage : BasePage
    {
#pragma warning disable 649
        [FindsBy(How = How.Id, Using = "country-element")]
        private IWebElement countriesDiv;

        [FindsBy(How = How.Id, Using = "language")]
        private IWebElement languagesDiv;

        [FindsBy(How = How.Id, Using = "index-vacancies-buttons")]
        private IWebElement allVacanciesButton;

        [FindsBy(How = How.XPath, Using = @"//*[@id=""hp""]/section[3]/div/div[1]")]
        private IWebElement vacanciesTable;

        [FindsBy(How = How.ClassName, Using = "vacancies-blocks-col")]
        private IList<IWebElement> vacancyRows;
#pragma warning restore 649

        public CareersPage SelectCountry(string country)
        {
            countriesDiv.Click();
            var countryElement =
                countriesDiv.FindElements(By.ClassName("selecter-item"))
                    .SingleOrDefault(x => x.GetAttribute("data-value") == country) ??
                throw new Exception($"{country} hasn't been found");
            countryElement.Click();

            return InitPage(this);
        }

        public CareersPage SelectLanguage(string language)
        {
            languagesDiv.Click();
            var languageElement =
                languagesDiv
                    .FindElement(By.ClassName("col-xs-12"))
                    .FindElements(By.TagName("label"))
                    .SingleOrDefault(x => x.Text.Contains(language)) ??
                throw new Exception($"{language} hasn't been found");
            languageElement.Click();

            languagesDiv.Click();

            return InitPage(this);
        }

        public CareersPage ClickAllVacancies()
        {
            CreateReloadVacanciesChecker();
            WaitForReloadVacancies();
            InvalidateCheckerResult();

            allVacanciesButton.Click();

            return InitPage(this);
        }

        public int CountVacancies()
        {
            WaitForReloadVacancies();

            return vacancyRows.Count;
        }

        private void CreateReloadVacanciesChecker()
        {
            jsExecutor.ExecuteScript(
                "var requiredElement = $(\"#hp\").find(\".vacancies-blocks\")[0]; var state; requiredElement.addEventListener(\"DOMNodeInserted\", function(e) { window.state = e; }, false);");
        }

        private void WaitForReloadVacancies()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(x =>
            {
                try
                {
                    jsExecutor.ExecuteScript("return window.state.returnValue");

                    return true;
                }
                catch (WebDriverException)
                {
                    return false;
                }
            });
        }

        private void InvalidateCheckerResult()
        {
            jsExecutor.ExecuteScript("window.state = null");
        }
    }
}