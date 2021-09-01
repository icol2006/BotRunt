using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppDriverChrome
{
    public class DriverChrome
    {
        public IWebDriver driver { get; set; }
        public IWebElement element;

        ChromeOptions chromeOptions = null;
        ChromeDriverService chromeDriverService = null;

        public DriverChrome()
        {
            setDriverOptions();
        }

        /// <summary>
        /// Set driver options
        /// </summary>
        private void setDriverOptions()
        {
            chromeDriverService = ChromeDriverService.CreateDefaultService();
           // chromeDriverService.HideCommandPromptWindow = true;

            chromeOptions = new ChromeOptions();
            //chromeOptions.AddArguments("headless");
            //chromeOptions.AddArguments("disable-infobars");
            //chromeOptions.AddArguments("start-maximized");
            chromeOptions.AddArguments("--ignore-certificate-errors");
            chromeOptions.AddArguments("--ignore-ssl-errors");
        }

        /// <summary>
        /// Visibility of driver
        /// </summary>
        /// <param name="visible"></param>
        public void setDriver()
        {
            Thread thread = new Thread(() =>
            {
                driver = new ChromeDriver(chromeDriverService, chromeOptions);
            });
            thread.Start();
        }

        public void CloseDriver()
        {
            driver.Dispose();
        }


        public void iniciarNavegador()
        {
            Thread thread = new Thread(() =>
            {
                try
                {
                    driver = null;
                    this.setDriver();
                }
                catch (Exception ex)
                {
                }

            });
            thread.Start();
        }


    }
}

