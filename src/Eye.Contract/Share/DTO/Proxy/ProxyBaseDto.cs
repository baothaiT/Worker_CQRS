using Eye.Contract.Share.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eye.Contract.Share.DTO;

public class ProxyBaseDto
{
    public string Ip { get; set; }
    public int Port { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
    public ProxyStatusEnum? IsStatus { get; set; }
    public bool? IsMigration { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public DateTime? CheckLiveDate { get; set; }
}
