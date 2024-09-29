using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eye.Contract.Share.DTO.Account;

public class UpdateAccountsDto : AccountsBaseDto
{
    public Guid Id { get; set; }
    public bool IsDelete { get; set; }
}
