using Eye.Contract.Share.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eye.Infrastructure.Chrome.Selenium.SeleniumServices;

public interface IScriptAutoService
{
    public void TestScript(ProfileModel profile);
    
}
