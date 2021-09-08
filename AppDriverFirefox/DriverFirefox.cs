using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppDriverFirefox
{
    public class DriverFirefox
    {
        public IWebDriver driver { get; set; }
        public IWebElement element;

        public DriverFirefox()
        {

        }

        /// <summary>
        /// Visibility of driver
        /// </summary>
        /// <param name="visible"></param>
        public void setDriver()
        {
            Thread thread = new Thread(() =>
            {
                FirefoxOptions firefoxOptions = new FirefoxOptions();            
                var profile = new FirefoxProfile();
                profile.AcceptUntrustedCertificates = true;
                firefoxOptions.Profile = profile;

                driver = new FirefoxDriver(firefoxOptions);
                
            });
            thread.Start();
        }

        public void CloseDriver()
        {
            try
            {
                driver.Dispose();
            }
            catch (Exception)
            {

            }           
        }


        public void iniciarNavegador(Form form)
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
                    form.Invoke(new Action(() =>
                    {
                        MessageBox.Show(ex.Message);
                    }));
                }

            });
            thread.Start();
        }


    }
}

