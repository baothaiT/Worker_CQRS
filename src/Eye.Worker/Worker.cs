using Eye.Application.SeleniumServices;
using Eye.Application.SeleniumServices.Interfaces;
using Eye.Contract.Share.ConfigurationScriptSelenium;

namespace Eye.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IProcessWorkerService _processWorkerService;


    public Worker(ILogger<Worker> logger, IProcessWorkerService processWorkerService)
    {
        _logger = logger;
        _processWorkerService = processWorkerService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await _processWorkerService.Job_Processing(stoppingToken);
            }
            await Task.Delay(1000, stoppingToken);
        }
    }
}