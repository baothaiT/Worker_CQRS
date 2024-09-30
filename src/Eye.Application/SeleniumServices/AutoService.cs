using Eye.Contract.Share.DTO;
using Eye.Contract.Share.Models;
using Eye.Contract.Share.Static;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eye.Application.SeleniumServices;

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
        await _browserService.GridForAllProfile(screenHeight, screenWidth);
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
    //public void Test()
    //{
    //    Parallel.ForEach(_profileModels, (profile, state, index) =>
    //    {
    //        //_scriptAutoService.TestScript(profile);
    //    });
    //}



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

        //profileModels.Add(CreateProfile($"DepinProfile1", "104.239.105.125", 6655, "qxibizrx", "ximfqfs33pyv"));
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

    public Task Test()
    {
       return Task.CompletedTask;
    }
}
