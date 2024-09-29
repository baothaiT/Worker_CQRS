using Eye.Contract.Share.DTO;
using Eye.Contract.Share.Models;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eye.Application.Services.Interface
{
    public interface IProxyClientServce
    {
        Task<IEnumerable<GetProxyDto>> AllProxy();
        Task<IEnumerable<GetProxyDto>> IsProxyWorking(IEnumerable<GetProxyDto> proxies);
        Task UpdateAllProxies(List<GetProxyDto> proxies);
    }
}
