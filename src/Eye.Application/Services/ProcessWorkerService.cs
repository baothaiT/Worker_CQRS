using Eye.Application.SeleniumServices.Interfaces;
using Eye.Application.Services.Interface;
using Eye.Contract.Share.DTO;
using Eye.Contract.Share.ConfigurationScriptSelenium;

namespace Eye.Application.SeleniumServices;

public class ProcessWorkerService : IProcessWorkerService
{
    private readonly IProxyClientServce _proxyClientServce;
    public ProcessWorkerService(IProxyClientServce proxyClientServce)
    {
        _proxyClientServce = proxyClientServce;
    }

    public async Task Job_Processing(CancellationToken stoppingToken)
    {
        await Task.Delay(ConfigurationScriptSelenium.Start_CheckingAndUpdate_Proxy, stoppingToken);
        await Job_CheckingAndUpdate_Proxy();
        await Task.Delay(ConfigurationScriptSelenium.End_CheckingAndUpdate_Proxy);
    }

    private async Task Job_CheckingAndUpdate_Proxy()
    {
        Console.WriteLine("Start Update all proxies from DB");
        var proxiesResponse = await _proxyClientServce.GetAllProxy();

        int batchSize = ConfigurationScriptSelenium.BatchSize_CheckingAndUpdate_Proxy;
        int currentBatch = 0;
        List<GetProxyDto> proxies_Checked = new List<GetProxyDto>();

        // Create batches of 10 and process them
        foreach (var batch in proxiesResponse
                    .Select((record, index) => new { record, index })
                    .GroupBy(x => x.index / batchSize))
        {
            currentBatch++;
            Console.WriteLine($"Processing batch {currentBatch}:");
            List<GetProxyDto> batchList = batch.Select(x => x.record).ToList();

            // var proxiesResponse_Checked = await _proxyClientServce.IsProxyWorking(batchList);
            // proxies_Checked.AddRange(proxiesResponse_Checked);

            Console.WriteLine();
        }
        // await _proxyClientServce.UpdateAllProxies(proxies_Checked);
        Console.WriteLine("End Update all proxies from DB");
    }
}
