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

    //    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    //    {
    //        while (!stoppingToken.IsCancellationRequested)
    //        {
    //            try
    //            {
    //                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

    //                // Sample CRUD operations

    //                // 1. Create a Product
    //                //var newProduct = new CreateProductDto { Name = "Sample Product", Price = 99, Stock = 10 };
    //                //await _productService.CreateProductAsync(newProduct);
    //                //_logger.LogInformation("Product Created");


    //                //// 2. Get the created product (assuming ID 1 for this example)
    //                //var product = await _productService.GetProductByIdAsync("");
    //                //_logger.LogInformation($"Product Retrieved: {product.Name}");

    //                //// 3. Update the Product
    //                //product.Price = 149.99M;
    //                //await _productService.UpdateProductAsync();
    //                //_logger.LogInformation("Product Updated");

    //                //// 4. Delete the Product
    //                //await _productService.DeleteProductAsync(1);
    //                //_logger.LogInformation("Product Deleted");
    //            }
    //            catch (Exception ex)
    //            {
    //                _logger.LogError(ex, "An error occurred while performing CRUD operations");
    //            }

    //            // Wait for 10 minutes before running again
    //            //await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
    //            await Task.Delay(1000, stoppingToken);
    //        }
    //    }
    //}
}