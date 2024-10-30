
using Eye.Contract.Share.Models;
using OpenQA.Selenium.Chrome;

namespace Eye.Infrastructure.Chrome.Selenium.SeleniumServices.BuilderPattern;

public class ConfigBrowserSerivceBuilder
{
    private ChromeOptions _chromeOptions;

    public ConfigBrowserSerivceBuilder()
    {
        _chromeOptions = new ChromeOptions();
    }

    public ConfigBrowserSerivceBuilder SetUserDataDirs(ChromeOptions chromeOptions, ProfileModel profile)
    {
        var currentDirectory = System.IO.Directory.GetCurrentDirectory(); 
        string chromeProfilePath = Path.Combine(currentDirectory, "Chrome", "Profiles");
        chromeOptions.AddArgument($"user-data-dir={chromeProfilePath}");
        return this;
    }

    public ConfigBrowserSerivceBuilder SetProfileDirectory(ChromeOptions chromeOptions, ProfileModel profile)
    {
        chromeOptions.AddArgument($"profile-directory={profile.Name}");
        return this;
    }

    public ConfigBrowserSerivceBuilder SetLoadExtension(ChromeOptions chromeOptions)
    {
        var currentDirectory = System.IO.Directory.GetCurrentDirectory(); 
        string extensionPath = Path.Combine(currentDirectory, "Chrome", "Extensions", "ImportProxyExtension");
        chromeOptions.AddArguments("--load-extension=" + extensionPath);
        return this;
    }

    public ConfigBrowserSerivceBuilder SetScale(ChromeOptions chromeOptions, float scale)
    {
        chromeOptions.AddArgument($"--force-device-scale-factor={scale}");
        return this;
    }

    public ConfigBrowserSerivceBuilder SetWindowSize(ChromeOptions chromeOptions, ProfileModel profile)
    {
        chromeOptions.AddArgument("window-size=" + profile.screenWidth + "," + profile.screenHeith + "");
        return this;
    }

    public ConfigBrowserSerivceBuilder SetWindowPosition(ChromeOptions chromeOptions, ProfileModel profile)
    {
        chromeOptions.AddArgument("window-position=" + profile.xPosition + "," + profile.yPosition + "");
        return this;
    }

    public ConfigBrowserSerivceBuilder SetDisableWebrtc(ChromeOptions chromeOptions)
    {
        chromeOptions.AddArgument("--disable-webrtc");
        return this;
    }

    public ConfigBrowserSerivceBuilder SetIgnoreCertificateErrors(ChromeOptions chromeOptions)
    {
        chromeOptions.AddArgument("ignore-certificate-errors");
        return this;
    }

    public ChromeOptions Build()
    {
        return _chromeOptions;
    }
    

}
