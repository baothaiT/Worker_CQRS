using Eye.Contract.Share.Models;
using Eye.Contract.Share.Static;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eye.Infrastructure.Chrome.Selenium.SeleniumServices;

public abstract class ConfigBrowserSerivce
{
    public ConfigBrowserSerivce()
    {
        //chromeOptions.AddArgument("--disable-infobars");
        //chromeOptions.AddUserProfilePreference("credentials_enable_service", false);
        //chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);

        //chromeOptions.AddExcludedArgument("enable-automation");
        //chromeOptions.AddAdditionalOption("useAutomationExtension", false);
        //chromeOptions.AddUserProfilePreference("profile.default_content_settings.popups", 0); // Disable popups
        //chromeOptions.AddUserProfilePreference("download.default_directory", "/path/to/download"); // Change download directory
        //chromeOptions.AddArgument("--proxy-bypass-list=*");
        //chromeOptions.AddArgument("--incognito");
    }
}
