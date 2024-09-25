using Eye.Contract.Share.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eye.Application.SeleniumServices
{
    public interface IOutPutBehaviorBrowserService
    {
        void SendKey(IWebElement element, string stringKey);
        void Click(IWebElement element);

        void Click(IWebElement element, ProfileModel profile);
        IWebElement Clear(IWebElement element);
    }
}
