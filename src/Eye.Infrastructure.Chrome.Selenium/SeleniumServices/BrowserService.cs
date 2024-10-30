using Eye.Contract.Share.Models;
using Eye.Contract.Share.Static;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using Eye.Infrastructure.Chrome.Selenium.SeleniumServices.BuilderPattern;

namespace Eye.Infrastructure.Chrome.Selenium.SeleniumServices;

public class BrowserService : ConfigBrowserSerivce, IBrowserService
{
    public BrowserService()
    {
    }
    public void QuitProfile(ProfileModel profile)
    {
        if (profile.webDriver != null) profile.webDriver.Dispose();
    }

    private ChromeOptions SettingBrowserOption(ChromeOptions chromeOptions, ProfileModel profile)
    => new ConfigBrowserSerivceBuilder()
        .SetUserDataDirs()
        .SetProfileDirectory(profile)
        .SetLoadExtension()
        .SetScale(ConfigurationDefaultDevice.ScaleBrowser)
        .SetWindowSize(profile)
        .SetWindowPosition(profile)
        //.SetDisableWebrtc(chromeOptions)
        .SetIgnoreCertificateErrors()
        .Build();
    
    public Task<IWebDriver> CreateProfile(ProfileModel profile)
    {
        Console.WriteLine("Thread: " + Thread.CurrentThread.ManagedThreadId);
        ChromeOptions chromeOptions = new ChromeOptions();
        chromeOptions = SettingBrowserOption(chromeOptions, profile);
        //chromeOptions.Proxy = AddProxy(profile);

        IWebDriver webDriver = new ChromeDriver(chromeOptions);
        return ProcessorProfile(webDriver);
    }
    
    private Task<IWebDriver> ProcessorProfile(IWebDriver webDriver)
    {
        try
        {
            webDriver.Navigate().GoToUrl(ExampleUrl.WhatIsMyIPAddress); // URL

            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(60));
            wait.Until(driver => driver.Title.Length > 0);
            return Task.FromResult(webDriver);
        }
        catch (WebDriverException extensions)
        {
            Console.WriteLine(extensions);
            webDriver?.Dispose();
            return null;
        }
    }

    public int Test_Console(int i)
    {
        Random random = new Random();
        int randomNumber = random.Next(100, 500);
        return randomNumber;
    }
}
