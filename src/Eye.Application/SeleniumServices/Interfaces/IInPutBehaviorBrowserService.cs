using Eye.Contract.Share.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eye.Application.SeleniumServices
{
    public interface IInPutBehaviorBrowserService
    {
        IWebElement? GetDocumentCssSelector(ProfileModel profileModel, string cssSelector);
        IWebElement GetDocumentByClass(IWebDriver driver, string className);
    }
}
