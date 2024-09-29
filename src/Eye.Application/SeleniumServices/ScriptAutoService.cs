using Eye.Contract.Share.Models;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eye.Application.SeleniumServices;

public class ScriptAutoService : IScriptAutoService
{
    private readonly IInPutBehaviorBrowserService _inPutBehaviorBrowserService;
    private readonly IOutPutBehaviorBrowserService _outPutBehaviorBrowserService;

    public ScriptAutoService(
        IInPutBehaviorBrowserService inPutBehaviorBrowserService, 
        IOutPutBehaviorBrowserService outPutBehaviorBrowserService
        )
    {
        _inPutBehaviorBrowserService = inPutBehaviorBrowserService;
        _outPutBehaviorBrowserService = outPutBehaviorBrowserService;
    }
    

    public void TestScript(ProfileModel profile)
    {
        try
        {
            Random random = new Random();
            IWebElement? elementUserName = _inPutBehaviorBrowserService.GetDocumentCssSelector(profile, "#user\\[email\\]"); // UserName
            if (elementUserName != null)
            {
                elementUserName = _outPutBehaviorBrowserService.Clear(elementUserName);
                _outPutBehaviorBrowserService.SendKey(elementUserName, "test@gmail.com");
            }
            else
            {
                Console.WriteLine("Not find element");
            }

            Thread.Sleep(1000 + random.Next(100, 300));
            IWebElement? elementPasswords = _inPutBehaviorBrowserService.GetDocumentCssSelector(profile, "#user\\[password\\]"); // Password
            if (elementPasswords != null)
            {
                elementPasswords = _outPutBehaviorBrowserService.Clear(elementPasswords);
                _outPutBehaviorBrowserService.SendKey(elementPasswords, "password");
            }
            else
            {
                Console.WriteLine("Not find element");
            }

            Thread.Sleep(1000 + random.Next(100, 300));

            IWebElement? elementButtonSignIn = _inPutBehaviorBrowserService.GetDocumentCssSelector(profile, ".button.button-primary.g-recaptcha"); // Btn Signin
            if (elementButtonSignIn != null)
            {
                _outPutBehaviorBrowserService.Click(elementButtonSignIn, profile);
            }
            else
            {
                Console.WriteLine("Not find element");
            }

            Thread.Sleep(200 + random.Next(100, 300));
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

    }
}
