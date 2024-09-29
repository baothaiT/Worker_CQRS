using Eye.Contract.Share.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eye.Application.Services.Interface
{
    public interface IProxyServce
    {
        Task<bool> IsProxyWorking(string proxyAddress, string proxyUsername, string proxyPassword, string testUrl);
        List<ProxyModel> ReadProxies(string filePath);
    }
}
