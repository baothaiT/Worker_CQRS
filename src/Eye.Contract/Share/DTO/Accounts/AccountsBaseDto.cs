using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eye.Contract.Share.DTO.Account;

public class AccountsBaseDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public bool IsStatus { get; set; }
    public DateTime CreateDate { get; set; }

    // Navigation Proxy
    //public Guid? Proxy { get; set; }
    //public GetProxyDto? Account_Proxy { get; set; }

    // Navigation AccountType
    //public Guid UserType { get; set; } // FK to AccountType
    //public GetAccountTypeDto? AccountType { get; set; }


    //public ICollection<LogsEntity>? Logs { get; set; }
    //public ICollection<AccountsInBrowserEntity>? Account_AccountsInBrowser { get; set; }
    //public ICollection<AccountsInProjectEntity>? Account_AccountsInProject { get; set; }
}
