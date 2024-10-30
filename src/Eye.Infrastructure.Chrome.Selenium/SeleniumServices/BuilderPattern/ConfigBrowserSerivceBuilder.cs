
using Eye.Contract.Share.Models;
using Eye.Contract.Share.Static;
using OpenQA.Selenium;
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
    public ConfigBrowserSerivceBuilder SetUserAgents(int index)
    {
        //_chromeOptions.AddArgument("--user-agent=" + UserAgents.user_agents_list_pretty[index]);
        return this;
    }
    public ConfigBrowserSerivceBuilder SetDisableBlinkFeatures_AutomationControlled()
    {
        _chromeOptions.AddArgument("--disable-blink-features=AutomationControlled"); // Remove automation flag
        return this;
    }
    public ConfigBrowserSerivceBuilder SetDisableDevShmUsage(ChromeOptions chromeOptions)
    {
        _chromeOptions.AddArgument("--disable-dev-shm-usage"); // Overcome limited resource problems in containers
        return this;
    }
    public ConfigBrowserSerivceBuilder SetNoSandbox()
    {
        _chromeOptions.AddArgument("--no-sandbox"); // Bypass OS security model (useful in CI environments)
        return this;
    }
    public ConfigBrowserSerivceBuilder SetDisableGpu()
    {
        _chromeOptions.AddArgument("--disable-gpu");
        return this;
    }
    public ConfigBrowserSerivceBuilder SetDisableExtensions()
    {
        _chromeOptions.AddArgument("--disable-extensions");
        return this;
    }
    public ConfigBrowserSerivceBuilder SetHeadless()
    {
        _chromeOptions.AddArgument("--headless");
        return this;
    }
    public ConfigBrowserSerivceBuilder SetProxy(ProfileModel profile)
    {
        // Proxy details
        if (
            !String.IsNullOrEmpty(profile.Ip) &&
            !String.IsNullOrEmpty(profile.Port) &&
            !String.IsNullOrEmpty(profile.UserName) &&
            !String.IsNullOrEmpty(profile.Password)
        )
        {
            string proxyHost = profile.Ip;
            string proxyPort = profile.Port;
            string proxyUser = profile.UserName;
            string proxyPass = profile.Password;

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
            _chromeOptions.Proxy = proxy;
        }
        return this;
    }
}
