using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            chromeDriverService.HideCommandPromptWindow = true;

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
        public void setDriver(Form form)
        {
            Thread thread = new Thread(() =>
            {
                try
                {
                    driver = new ChromeDriver(chromeDriverService, chromeOptions);
                }
                catch (Exception ex)
                {
                    form.Invoke(new Action(() =>
                    {
                        MessageBox.Show(ex.Message);
                    }));
                }
               
            });
            thread.Start();
        }

        public void CloseDriver(Form form)
        {
            Thread thread = new Thread(() =>
            {
                try
                {
                    if(driver!=null)
                    driver.Dispose();
                }
                catch (Exception ex)
                {
      
                }

            });
            thread.Start();        
        }

        public void iniciarNavegador(Form form)
        {
            Thread thread = new Thread(() =>
            {
                try
                {
                    driver = null;
                    this.setDriver(form);
                }
                catch (Exception ex)
                {
                }

            });
            thread.Start();
        }


    }
}