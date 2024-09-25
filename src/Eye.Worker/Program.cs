using Eye.Worker;
using Eye.Application.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<Worker>();

builder.Services.AddHttpClient<IProductService, ProductService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5199/"); // Replace with your API URL
});

var host = builder.Build();
host.Run();
