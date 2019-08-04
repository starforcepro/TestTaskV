using CareersTests.Core;
using CareersTests.Pages;
using FluentAssertions;
using NUnit.Framework;

namespace CareersTests.Tests
{
    [TestFixture]
    public class CareersPageTests : SeleniumTestBase
    {
        [Test]
        public void VacanciesCount_ShouldBeFive_WhenCountryIsRomaniaAndLanguageIsEnglish()
        {
            DriverController.GetCurrentDriverInstance().Navigate().GoToUrl(testServer);

            const int expectedVacanciesCount = 5;
            var careersPage = new CareersPage();
            careersPage
                .SelectCountry("Romania")
                .SelectLanguage("English")
                .ClickAllVacancies();

            var actualVacanciesCount = careersPage.CountVacancies();

            actualVacanciesCount.Should().Be(expectedVacanciesCount);
        }
    }
}