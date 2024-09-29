using Eye.Application.Services;
using Eye.Application.Services.Interface;
using Eye.Contract.Share.DTO._JoinDTO;
using Microsoft.AspNetCore.Mvc;

namespace Eye.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IAccountService _accountService;

        public AccountController(ILogger<ProductController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var responseAllAccount = await _accountService.GetAl();
            List<Account_Browser_DTO> account_Browser_DTOs = new List<Account_Browser_DTO>();
            if (responseAllAccount != null)
            {
                foreach (var item in responseAllAccount)
                {
                    var responseBrowserOfAccount = await _accountService.Account_Browser_DTO(item.Id.ToString());
                    if (responseBrowserOfAccount != null)
                    {
                        account_Browser_DTOs.AddRange(responseBrowserOfAccount);
                    }
                }
                
            }
            
            return View(account_Browser_DTOs);
        }
    }
}
