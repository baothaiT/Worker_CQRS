using Eye.Contract.Share.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eye.Application.SeleniumServices
{
    public interface IAutoService
    {
        Task<IWebDriver> StartProfile(ProfileModel profileModel);
        void CloseProfile(string profileName);
        Task GridProfiles(int screenHeight, int screenWidth);
        Task StartAll(List<ProfileModel> profileModels);
        void StartAll_Parallel(List<ProfileModel> profileModels);
        void CloseAll();
        List<ProfileModel> GridProfilesWhenStart(int numberWidth, int numberHeith, List<ProfileModel> profileModels);
        List<ProfileModel> GridProfilesWhenStart(int numberWidth, int numberHeith, List<ProfileModel> profileModels, int xPaging, int yPaging);
        Task Test();
    }
}
