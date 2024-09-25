using Eye.Contract.Share.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eye.Application.SeleniumServices
{
    public interface IBrowserService
    {
        Task<IWebDriver> CreateProfile(ProfileModel profile);
        void QuitProfile(ProfileModel profile);
        Task GridForAllProfile(int screenWidth, int screenHeight);
        int Test_Console(int i);
    }
}
