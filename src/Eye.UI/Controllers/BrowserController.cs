using Eye.Application.SeleniumServices;
using Eye.Application.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Eye.Contract.Share.Models;

namespace Eye.UI.Controllers
{
   public class BrowserController : Controller
   {
        private readonly IAutoService _autoService;
        private readonly IProxyServce _proxyServce;
        private List<ProxyModel> _proxyModels;

        public BrowserController(IAutoService autoService, IProxyServce proxyServce)
        {
            _autoService = autoService;
            _proxyServce = proxyServce;
            _proxyModels = new List<ProxyModel>();
        }
       // GET: BrowserController
       public async Task<ActionResult> Index(string checkProxy, string ip, string user, string password)
       {
            ViewBag.CheckProxy = !String.IsNullOrEmpty(checkProxy)? checkProxy: "null";
            ViewBag.Ip = !String.IsNullOrEmpty(ip) ? ip : "null";
            ViewBag.User = !String.IsNullOrEmpty(user) ? user : "null";
            ViewBag.Password = !String.IsNullOrEmpty(password) ? password : "null";

            string filePath = Path.Combine("Data", "proxies.txt"); // Assuming 'Data' folder is in the same directory as executable
            List<ProxyModel> proxyModels = _proxyServce.ReadProxies(filePath);

            for (int i = 0; i < proxyModels.Count; i++)
            {
                string proxyAddress = proxyModels[i].IP + ":" + proxyModels[i].Port;
                string proxyUsername = proxyModels[i].Username; // Leave blank if no auth
                string proxyPassword = proxyModels[i].Password; // Leave blank if no auth
                string testUrl = "https://httpbin.org/ip";
                bool isProxyWorking = await _proxyServce.IsProxyWorking(proxyAddress, proxyUsername, proxyPassword, testUrl);
                if (isProxyWorking)
                {
                    proxyModels[i].CheckStatus = "Proxy is working.";
                }
                else
                {
                    proxyModels[i].CheckStatus = "Proxy is not working.";
                }
            }

            return View(proxyModels);
       }
       [HttpPost]
       public async Task<ActionResult> StartBrowser()
       {

            await _autoService.Test();
            //    return RedirectToAction("Action", new { id = 99 });
            return RedirectToAction("Index");
       }

        [HttpPost]
        public async Task<ActionResult> CheckProxy(IFormCollection collection)
        {
            //string path = Path.GetFullPath("");
            

            string proxyAddress = collection["ip"]+":"+ collection["port"];
            string proxyUsername = "qxibizrx"; // Leave blank if no auth
            string proxyPassword = "ximfqfs33pyv"; // Leave blank if no auth

            // URL to test the proxy
            string testUrl = "https://httpbin.org/ip";

            //// Check if the proxy is working
            //for (int i = 0; i < proxyModels.Count; i++)
            //{
            //    bool isProxyWorking = await _proxyServce.IsProxyWorking(proxyAddress, proxyUsername, proxyPassword, testUrl);
            //    if (isProxyWorking)
            //    {
            //        proxyModels[i].CheckStatus = "Proxy is working.";
            //    }
            //    else
            //    {
            //        proxyModels[i].CheckStatus = "Proxy is not working.";
            //    }
            //}

            //_proxyModels = proxyModels;

            return RedirectToAction("Index", new
            {
                checkProxy = "Look at table",
                ip = proxyAddress,
                user = proxyUsername,
                password = proxyPassword
            });
        }


        // GET: BrowserController/Create
        public ActionResult Create()
       {
           return View();
       }

       // POST: BrowserController/Create
       [HttpPost]
       public ActionResult Create(IFormCollection collection)
       {
           try
           {
               return RedirectToAction(nameof(Index));
           }
           catch
           {
               return View();
           }
       }


   }
}
