using Eye.Contract.Share.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eye.Infrastructure.Chrome.Selenium.SeleniumServices
{
    public interface IBrowserService
    {
        Task<IWebDriver> CreateProfile(ProfileModel profile);
        Task<IWebDriver> ProcessorProfile(IWebDriver webDriver);
        void QuitProfile(ProfileModel profile);
        int Test_Console(int i);
    }
}
