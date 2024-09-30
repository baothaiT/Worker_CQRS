using Eye.Application.SeleniumServices;
using Eye.Application.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Eye.Contract.Share.Models;
using Eye.Contract.Share.Enum;

namespace Eye.UI.Controllers
{
   public class BrowserController : Controller
   {
        private readonly IAutoService _autoService;
        private readonly IProxyClientServce _proxyServce;
        private List<ProxyModel> _proxyModels;

        public BrowserController(IAutoService autoService, IProxyClientServce proxyServce)
        {
            _autoService = autoService;
            _proxyServce = proxyServce;
            _proxyModels = new List<ProxyModel>();
        }
        // GET: BrowserController
        public async Task<ActionResult> Index()
        {
            return View();
        }   
        [HttpPost]
        public async Task<ActionResult> StartBrowser()
        {

            await _autoService.Test();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> StartAll()
        {
            var proxiesResponse = await _proxyServce.GetAllProxiesByStatus(ProxyStatusEnum.Live);
            await _autoService.StartAllByProxies(proxiesResponse.ToList());
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult CloseAll()
        {
            _autoService.CloseAll();
            return RedirectToAction("Index");
        }
    }
}
