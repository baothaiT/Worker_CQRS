using Eye.Contract.Share.DTO;
using Eye.Contract.Share.Models;
using Eye.Contract.Share.Static;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eye.Infrastructure.Chrome.Selenium.SeleniumServices;

public class AutoService : IAutoService
{
    private readonly IBrowserService _browserService;
    private List<ProfileModel> _profileModels;
    private readonly IScriptAutoService _scriptAutoService;
    private readonly ILogger<AutoService> _logger;

    public AutoService(IBrowserService browserService, IScriptAutoService scriptAutoService, ILogger<AutoService> logger)
    {
        _browserService = browserService;
        _profileModels = new List<ProfileModel>();
        _scriptAutoService = scriptAutoService;
        _logger = logger;
    }

    public async Task<IWebDriver> StartProfile(ProfileModel profileModel)
    {
        Console.WriteLine($"Started Profile: " + profileModel.Name);
        var responseDriver = await _browserService.CreateProfile(profileModel);
        if(responseDriver != null)
        {
            profileModel.webDriver = responseDriver;
            _profileModels.Add(profileModel);
            Console.WriteLine($"Ended Profile: " + profileModel.Name);
            return profileModel.webDriver;
        }
        return null;
    }

    public void CloseProfile(string profileName)
    {
        var driver = _profileModels.FirstOrDefault(p => p.Name.Equals(profileName));
        if (driver != null)
        {
            _browserService.QuitProfile(driver);
            _profileModels.Remove(driver);
            Console.WriteLine("Close Profile: " + profileName);
        }
        else
        {
            Console.WriteLine("Not Found Profile: " + profileName);
        }
    }

    public async Task GridProfiles(int screenHeight, int screenWidth)
    {
        await Task.CompletedTask;
    }

    public List<ProfileModel> GridProfilesWhenStart(int numberWidth, int numberHeith, List<ProfileModel> profileModels)
    {
        //Full HD 1920x1080
        int profileWidth = (int)Math.Round((decimal)ConfigurationDefaultDevice.FullHD_Width / numberWidth, MidpointRounding.ToEven);
        int profileHeith = (int)Math.Round((decimal)ConfigurationDefaultDevice.FullHD_Height / numberHeith, MidpointRounding.ToEven);

        int x = 0;
        int y = 0;
        for (int i = 0; i < profileModels.Count; i++)
        {

            profileModels[i].screenWidth = profileWidth;
            profileModels[i].screenHeith = profileHeith;

            profileModels[i].xPosition = profileWidth * x;
            profileModels[i].yPosition = profileHeith * y;

            if (x == numberWidth - 1)
            {
                x = 0;
                ++y;
            }
            else
            {
                ++x;
            }

        }
        return profileModels;
    }

    public List<ProfileModel> GridProfilesWhenStart(int numberWidth, int numberHeith, List<ProfileModel> profileModels, int xPaging, int yPaging)
    {
        //Full HD 1920x1080
        // int profileWidth = (int) Math.Round( (decimal) ConfigurationDefaultDevice.FullHD_Width/numberWidth, MidpointRounding.ToEven);
        // int profileHeith = (int) Math.Round( (decimal) ConfigurationDefaultDevice.FullHD_Height/numberHeith, MidpointRounding.ToEven);

        // int profileWidth = 400;
        // int profileHeith = 500;

        int profileWidth = 600;
        int profileHeith = 800;

        int x = 0;
        int y = 0;
        for (int i = 0; i < profileModels.Count; i++)
        {

            profileModels[i].screenWidth = profileWidth;
            profileModels[i].screenHeith = profileHeith;

            profileModels[i].xPosition = profileWidth * x + xPaging;
            profileModels[i].yPosition = profileHeith * y + yPaging;

            if (x == numberWidth - 1)
            {
                x = 0;
                ++y;
            }
            else
            {
                ++x;
            }

        }
        return profileModels;
    }

    public async Task StartAll(List<ProfileModel> profileModels)
    {
        foreach (var profile in profileModels)
        {
            await StartProfile(profile);
        }
    }

    public void StartAll_Parallel(List<ProfileModel> profileModels)
    {
        Parallel.ForEach(profileModels, profile =>
        {
            try
            {
                Task.Run(() => StartProfile(profile)).Wait();
            }
            catch (AggregateException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error => AggregateException: {ex}");
                Console.ResetColor();
            }
            // Start the asynchronous method within a task.
            // Start
            

            // Run Script
            //_scriptAutoService.TestScript(profile);

            // Close
            //profile.webDriver?.Dispose();
            //_profileModels.Remove(profile);
        });
    }

    public void CloseAll()
    {
        Parallel.ForEach(_profileModels, (profile, state, index) =>
        {
            Console.WriteLine("Start Close profile: " + index);
            Console.WriteLine("Thread: " + Thread.CurrentThread.ManagedThreadId);
            if(profile != null)
            {
                profile.webDriver?.Dispose();
                _profileModels.Remove(profile);
            }
            
            Console.WriteLine("End Close profile: " + index);
        });

    }

    private ProfileModel CreateProfile(string NameProfile, string Ip, int Port, string User, string Password)
    {
        ProfileModel profileModel = new ProfileModel()
        {
            Name = NameProfile,
            xPosition = 180,
            yPosition = 100,
            screenHeith = 800,
            screenWidth = 600,
            Ip = Ip,
            Port = Port.ToString(),
            UserName = User,
            Password = Password
        };
        return profileModel;
    }

    private List<ProfileModel> CreateProfileModels(List<GetProxyDto> getProxyDtos)
    {
        List<ProfileModel> profileModels = new List<ProfileModel>();
        int i = 0;
        foreach (var proxy in getProxyDtos)
        {
            ++i;
            profileModels.Add(CreateProfile($"Profile-{proxy.Ip}-{proxy.Port}", proxy.Ip, proxy.Port, proxy.User, proxy.Password));
        }
        return profileModels;
    }

    public async Task StartAllByProxies(List<GetProxyDto> getProxyDtos)
    {
        _logger.LogInformation("Start Create Browser Selenium");

        List<ProfileModel> profileModels = CreateProfileModels(getProxyDtos);

        Console.WriteLine("Start StartAll");
        int x = 7;
        int y = 4;
        profileModels = GridProfilesWhenStart(x, y, profileModels, 180, 0);
        StartAll_Parallel(profileModels);

        Console.WriteLine("End StartAll");
        await Task.Delay(6000);
        // CloseAll();
        //_logger.LogInformation("End Create Browser Selenium");
        //await Task.Delay(2000);
    }

    #region Test Proxy
    private async Task TestAddProxy()
    {
        string httpHost = "103.177.108.235";
        int httpPort = 6789;
        string username = "xwci1j2w";
        string password = "xWcI1j2w";

        var currentDirectory = System.IO.Directory.GetCurrentDirectory(); 
        string extensionPath = Path.Combine(currentDirectory, "Chrome", "Extensions", "ImportProxyExtension");
        _logger.LogInformation("target: " + extensionPath);
       

        // Check if necessary files are present
        if (!IsValidExtensionFolder(extensionPath))
        {
            Console.WriteLine("Extension folder is missing required files: manifest.json or background.js");
            return;
        }
        IWebDriver driver = InitializeChromeWithExtension(extensionPath);
        try
        {

            var originalWindow = driver.CurrentWindowHandle;
            Console.WriteLine("Current ", originalWindow);

            string settingJs = $"saveProxyHttpSettings('{httpHost}', '{httpPort}', '{username}', '{password}');";

            // Combine into the final script with event listener
            string script = $@"document.addEventListener('DOMContentLoaded', function() {{
                {settingJs}
                httpProxy(); // Ensure httpProxy() is defined in the same scope
            }});";

            // Execute the script to add the event listener
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript(script);


            Thread.Sleep(3000);
            driver.SwitchTo().Window(driver.WindowHandles[1]);
            driver.Close();
            driver.SwitchTo().Window(driver.WindowHandles[0]);


            Thread.Sleep(2000);

            driver.Navigate().GoToUrl($"https://ident.me");

            Thread.Sleep(4000);

        }
        catch (System.Exception)
        {
            throw;
        }
        finally
        {
            Thread.Sleep(100000);
            driver.Quit();
        }
    }

    public static bool IsValidExtensionFolder(string folderPath)
    {
        string manifestPath = Path.Combine(folderPath, "manifest.json");
        string backgroundPath = Path.Combine(folderPath, "background.js");

        return File.Exists(manifestPath) && File.Exists(backgroundPath);
    }

    public static IWebDriver InitializeChromeWithExtension(string extensionPath)
    {
        ChromeOptions options = new ChromeOptions();
        var currentDirectory = System.IO.Directory.GetCurrentDirectory(); 
        string chromeProfilePath = Path.Combine(currentDirectory, "Chrome", "Profiles");
        options.AddArgument($"user-data-dir={chromeProfilePath}");
        options.AddArgument("profile-directory=Profile 1");
        options.AddArguments("--load-extension=" + extensionPath);
        return new ChromeDriver(options);
    }

    #endregion

    public async Task Test()
    {
        _logger.LogInformation("Start test -  Create Browser Selenium");
        // await TestAddProxy();
        var profileTest = CreateProfile(
            "profileTest",
            "1",
            1,
            "user",
            "pass"
        );

        await StartProfile(profileTest);

    }
}
