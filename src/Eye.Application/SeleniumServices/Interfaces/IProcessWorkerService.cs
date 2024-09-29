using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eye.Application.SeleniumServices.Interfaces;

public interface IProcessWorkerService
{
    Task Job_CheckingAndUpdate_Proxy();
}
