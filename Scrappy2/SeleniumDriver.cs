namespace Scrappy2
{
    using System;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    public class SeleniumDriver
    {
        private readonly IWebDriver driver;

        public SeleniumDriver()
        {
            this.driver = new ChromeDriver();
        }

        public string GetHtml(Uri uri)
        {
            this.driver.Navigate().GoToUrl(uri);
            var doc = this.driver.FindElement(By.TagName("html"));
            var html = doc.GetAttribute("innerHTML");
            
            return html;
        }

        public void Quit()
        {
            this.driver.Quit();
        }
    }
}