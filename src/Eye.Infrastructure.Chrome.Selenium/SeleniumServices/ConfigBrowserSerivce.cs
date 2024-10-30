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

    public ChromeOptions Set(ChromeOptions chromeOptions)
    {
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

    protected Proxy AddProxy(ProfileModel profile)
    {
        // Proxy details
        if (
            !String.IsNullOrEmpty(profile.Ip) &&
            !String.IsNullOrEmpty(profile.Port) &&
            !String.IsNullOrEmpty(profile.UserName) &&
            !String.IsNullOrEmpty(profile.Password)
        )
        {
            string proxyHost = profile.Ip; // e.g., "123.45.67.89"
            string proxyPort = profile.Port; // e.g., 8080

            // If proxy requires authentication
            string proxyUser = profile.UserName;
            string proxyPass = profile.Password;

            // Setting up the proxy
            var proxy = new Proxy()
            {
                Kind = ProxyKind.Manual,
                IsAutoDetect = false,
                HttpProxy = $"{proxyHost}:{proxyPort}",
                SslProxy = $"{proxyHost}:{proxyPort}",
                FtpProxy = $"{proxyHost}:{proxyPort}",
                SocksUserName = proxyUser,
                SocksPassword = proxyPass
            };
            return proxy;
        }
        return null;
    }

    

    

    protected ChromeOptions SetUserAgents(ChromeOptions chromeOptions, int index)
    {
        //chromeOptions.AddArgument("--user-agent=" + UserAgents.user_agents_list_pretty[index]);
        return chromeOptions;
    }

    

    

    protected ChromeOptions SetDisableBlinkFeatures_AutomationControlled(ChromeOptions chromeOptions)
    {
        chromeOptions.AddArgument("--disable-blink-features=AutomationControlled"); // Remove automation flag
        return chromeOptions;
    }

    

    protected ChromeOptions SetDisableDevShmUsage(ChromeOptions chromeOptions)
    {
        chromeOptions.AddArgument("--disable-dev-shm-usage"); // Overcome limited resource problems in containers
        return chromeOptions;
    }

    protected ChromeOptions SetNoSandbox(ChromeOptions chromeOptions)
    {
        chromeOptions.AddArgument("--no-sandbox"); // Bypass OS security model (useful in CI environments)
        return chromeOptions;
    }

    protected ChromeOptions SetDisableGpu(ChromeOptions chromeOptions)
    {
        chromeOptions.AddArgument("--disable-gpu");
        return chromeOptions;
    }

    protected ChromeOptions SetDisableExtensions(ChromeOptions chromeOptions)
    {
        chromeOptions.AddArgument("--disable-extensions");
        return chromeOptions;
    }

    protected ChromeOptions SetHeadless(ChromeOptions chromeOptions)
    {
        chromeOptions.AddArgument("--headless");
        return chromeOptions;
    }

    
}
