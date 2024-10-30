
using Eye.Contract.Share.Models;
using Eye.Contract.Share.Static;
using OpenQA.Selenium.Chrome;

namespace Eye.Infrastructure.Chrome.Selenium.SeleniumServices.BuilderPattern;

public class ConfigBrowserSerivceBuilder
{
    private ChromeOptions _chromeOptions;

    public ConfigBrowserSerivceBuilder()
    {
        _chromeOptions = new ChromeOptions();
    }
    public ChromeOptions Build() => _chromeOptions;

    public ConfigBrowserSerivceBuilder SetUserDataDirs()
    {
        var currentDirectory = Directory.GetCurrentDirectory(); 
        string chromeProfilePath = Path.Combine(currentDirectory, "Chrome", "Profiles");
        _chromeOptions.AddArgument($"user-data-dir={chromeProfilePath}");
        return this;
    }

    public ConfigBrowserSerivceBuilder SetProfileDirectory(ProfileModel profile)
    {
        _chromeOptions.AddArgument($"profile-directory=1{profile.Name}");
        return this;
    }

    public ConfigBrowserSerivceBuilder SetLoadExtension()
    {
        var currentDirectory = Directory.GetCurrentDirectory(); 
        string extensionPath = Path.Combine(currentDirectory, "Chrome", "Extensions", "BaseImportProxyExtension");
        if (!FileService.IsValidFile(extensionPath, ExtensionStatics.ManifestJsonName) && !FileService.IsValidFile(extensionPath, ExtensionStatics.BackgroundJsonName))
        {
            Console.WriteLine("Extension folder is missing required files: manifest.json or background.js");
            return this;
        }
        _chromeOptions.AddArguments("--load-extension=" + extensionPath);
        return this;
    }

    public ConfigBrowserSerivceBuilder SetScale(float scale)
    {
        _chromeOptions.AddArgument($"--force-device-scale-factor={scale}");
        return this;
    }

    public ConfigBrowserSerivceBuilder SetWindowSize(ProfileModel profile)
    {
        _chromeOptions.AddArgument("window-size=" + profile.screenWidth + "," + profile.screenHeith + "");
        return this;
    }

    public ConfigBrowserSerivceBuilder SetWindowPosition(ProfileModel profile)
    {
        _chromeOptions.AddArgument("window-position=" + profile.xPosition + "," + profile.yPosition + "");
        return this;
    }

    public ConfigBrowserSerivceBuilder SetDisableWebrtc()
    {
        _chromeOptions.AddArgument("--disable-webrtc");
        return this;
    }

    public ConfigBrowserSerivceBuilder SetIgnoreCertificateErrors()
    {
        _chromeOptions.AddArgument("ignore-certificate-errors");
        return this;
    }
    
}
