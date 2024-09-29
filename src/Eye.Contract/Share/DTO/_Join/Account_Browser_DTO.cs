using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eye.Contract.Share.DTO._JoinDTO
{
    public class Account_Browser_DTO
    {
        public Guid AccountId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsStatus { get; set; }
        public DateTime AccountCreateDate { get; set; }
        public Guid BrowserId { get; set; }
        public string Name { get; set; }
        public bool BrowserIsStatus { get; set; }
        public decimal? XPosition { get; set; }
        public decimal? YPosition { get; set; }
        public decimal? WithScreeen { get; set; }
        public decimal? HightScreen { get; set; }
        public decimal? Scale { get; set; }
        public string? UserAgent { get; set; }
        public DateTime BrowserCreateDate { get; set; }
    }
}
