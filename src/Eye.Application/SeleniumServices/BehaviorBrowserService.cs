using Eye.Contract.Share.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eye.Application.SeleniumServices;

public class BehaviorBrowserService : IInPutBehaviorBrowserService, IOutPutBehaviorBrowserService
{
    public BehaviorBrowserService()
    {

    }

    #region Get Document Element
    public void GetDocumentById(IWebDriver driver, string id)
    {
        IWebElement element = driver.FindElement(By.Id(id));
        Console.WriteLine(element);
    }

    public IWebElement GetDocumentByClass(IWebDriver driver, string className)
    {
        IWebElement element = driver.FindElement(By.ClassName(className));
        Console.WriteLine(element);
        return element;
    }

    public void GetDocumentByXPath(IWebDriver driver, string xPath)
    {
        IWebElement element = driver.FindElement(By.XPath(xPath));
        Console.WriteLine(element);
    }

    public void GetDocumentByName(IWebDriver driver, string name)
    {
        IWebElement element = driver.FindElement(By.Name(name));
        Console.WriteLine(element);
    }

    public void GetDocumentTagName(IWebDriver driver, string tagName)
    {
        IWebElement element = driver.FindElement(By.TagName(tagName));
        Console.WriteLine(element);
    }

    public IWebElement? GetDocumentCssSelector(ProfileModel profileModel, string cssSelector)
    {
        try
        {
            if (profileModel.webDriver != null)
            {
                IWebElement element = profileModel.webDriver.FindElement(By.CssSelector(cssSelector));
                return element;
            }
            return null;

        }
        catch (NoSuchElementException)
        {
            // Element not found, return null to handle it gracefully
            return null;
        }

    }

    #endregion

    #region OutPut

    public void SendKey(IWebElement element, string stringKey)
    {
        try
        {
            element.SendKeys(stringKey);
            Console.WriteLine("Thread: " + Thread.CurrentThread.ManagedThreadId + "    Sent Key: " + stringKey);
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public void Click(IWebElement element)
    {
        string outerHTML = element.GetAttribute("outerHTML");
        Console.WriteLine(outerHTML);
        element.Click();
        Console.WriteLine("Thread: " + Thread.CurrentThread.ManagedThreadId + "    Clicked");
    }


    public void Click(IWebElement element, ProfileModel profile)
    {
        try
        {
            string outerHTML = element.GetAttribute("outerHTML");
            element.Click();
            Console.WriteLine("Thread: " + Thread.CurrentThread.ManagedThreadId + "    Clicked");
        }
        catch (ElementClickInterceptedException e)
        {
            Console.WriteLine($"Element not clickable due to: {e.Message}");

            // Check for any potential overlay and remove it
            if (profile?.webDriver != null)
            {
                IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)profile.webDriver;
                jsExecutor.ExecuteScript("document.querySelector('.overlay').style.display='none';");

                // Attempt the click again with JavaScript
                jsExecutor.ExecuteScript("arguments[0].click();", element);
                Console.WriteLine("Thread: " + Thread.CurrentThread.ManagedThreadId + "    Clicked with JS after hiding overlay");
            }

        }
    }
    public IWebElement Clear(IWebElement element)
    {
        element.Clear();
        return element;
    }

    #endregion
}
