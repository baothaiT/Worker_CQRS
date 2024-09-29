using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eye.Application.Services.Interface;

public interface IHttpClientService
{
    public Task<HttpResponseMessage> GetAsync(string url);
}
