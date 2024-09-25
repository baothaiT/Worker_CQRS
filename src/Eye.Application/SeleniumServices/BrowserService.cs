using Eye.Contract.Share.Models;
using Eye.Contract.Share.Static;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eye.Application.SeleniumServices
{
    public class BrowserService : IBrowserService
    {
        public BrowserService()
        {
        }

        public Task GridForAllProfile(int screenWidth, int screenHeight) => Task.CompletedTask;
        public void QuitProfile(ProfileModel profile)
        {
            if (profile.webDriver != null)
            {
                profile.webDriver.Dispose();
            }

        }

        private ChromeOptions SettingBrowserOption(ChromeOptions chromeOptions, ProfileModel profile)
        {
            chromeOptions.AddArgument($"user-data-dir={ConfigurationDefaultDevice.Path + profile.Name}");
            // _chromeOptions.AddArgument("--headless"); // Run headless
            chromeOptions.AddUserProfilePreference("profile.default_content_settings.popups", 0); // Disable popups
            chromeOptions.AddUserProfilePreference("download.default_directory", "/path/to/download"); // Change download directory
            chromeOptions.AddArgument("--force-device-scale-factor=0.8");

            chromeOptions.AddExcludedArgument("enable-automation");
            chromeOptions.AddAdditionalOption("useAutomationExtension", false);

            chromeOptions.AddArgument("--disable-extensions");
            chromeOptions.AddArgument("--disable-gpu");
            chromeOptions.AddArgument("--no-sandbox"); // Bypass OS security model (useful in CI environments)
            chromeOptions.AddArgument("--disable-dev-shm-usage"); // Overcome limited resource problems in containers
            chromeOptions.AddArgument("window-size=" + profile.screenWidth + "," + profile.screenHeith + "");
            chromeOptions.AddArgument("window-position=" + profile.xPosition + "," + profile.yPosition + "");

            chromeOptions.AddArgument("--disable-blink-features=AutomationControlled"); // Remove automation flag
            chromeOptions.AddArgument("--disable-infobars");
            chromeOptions.AddUserProfilePreference("credentials_enable_service", false);
            chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
            return chromeOptions;
        }

        public ChromeOptions AddProxy(ChromeOptions chromeOptions)
        {
            Proxy proxy = new Proxy();
            proxy.Kind = ProxyKind.Manual;
            proxy.IsAutoDetect = false;
            proxy.SslProxy = "<HOST:PORT>";
            chromeOptions.Proxy = proxy;
            chromeOptions.AddArgument("ignore-certificate-errors");
            return chromeOptions;
        }

        public ChromeOptions AddUserAgents(ChromeOptions chromeOptions, int index)
        {
            //chromeOptions.AddArgument("--user-agent=" + UserAgents.user_agents_list_pretty[index]);
            return chromeOptions;
        }
        public Task<IWebDriver> CreateProfile(ProfileModel profile)
        {
            Console.WriteLine("Thread: " + Thread.CurrentThread.ManagedThreadId);
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions = SettingBrowserOption(chromeOptions, profile);
            //chromeOptions = AddUserAgents(chromeOptions, 1);

            IWebDriver _driverProfile = new ChromeDriver(chromeOptions);
            // _driverProfile.Navigate().GoToUrl(ExampleUrl.TestFormLogin); // URL
            // _driverProfile.Navigate().GoToUrl(ExampleUrl.Fv); // URL
            _driverProfile.Navigate().GoToUrl(ExampleUrl.WhatIsMyBrowser); // URL

            WebDriverWait wait = new WebDriverWait(_driverProfile, TimeSpan.FromSeconds(10));
            wait.Until(driver => driver.Title.Length > 0);
            return Task.FromResult(_driverProfile);
        }

        public int Test_Console(int i)
        {
            Random random = new Random();
            int randomNumber = random.Next(100, 500);
            return randomNumber;
        }

    }
}
