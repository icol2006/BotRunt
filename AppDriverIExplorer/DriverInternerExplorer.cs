
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppDriverIExplorer
{
    public class DriverInternerExplorer
    {
        public IWebDriver driver { get; set; }
        public IWebElement element;

        public DriverInternerExplorer()
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
                driver = new InternetExplorerDriver();
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
                    while (this.driver == null)
                    {

                    }

                }
                catch (Exception ex)
                {
                }

            });
            thread.Start();
        }


    }
}
