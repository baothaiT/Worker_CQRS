using Eye.Application.SeleniumServices;
using Eye.Application.SeleniumServices.Interfaces;
using Eye.Application.Services;
using Eye.Contract.Share.DTO;
using Eye.Contract.Share.Models;
using static System.Net.Mime.MediaTypeNames;

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
                await Task.Delay(2000, stoppingToken);
                await _processWorkerService.Job_CheckingAndUpdate_Proxy();
                await Task.Delay(6000);
            }
            await Task.Delay(1000, stoppingToken);
        }
    }
}