using Eye.Contract.Share.DTO._JoinDTO;
using Eye.Contract.Share.DTO.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eye.Application.Services.Interface;

public interface IAccountService
{
    Task<List<Account_Browser_DTO>> Account_Browser_DTO(string AccountId);
    Task<List<GetAccountsDto>> GetAl();
}
