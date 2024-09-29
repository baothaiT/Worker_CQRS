using Eye.Worker;
using Eye.Application.Services;
using Eye.Application.SeleniumServices;
using Eye.Application.Services.Interface;

var builder = Host.CreateApplicationBuilder(args);


builder.Services.AddHttpClient();
builder.Services.AddSingleton<IAutoService, AutoService>();
builder.Services.AddSingleton<IBrowserService, BrowserService>();
builder.Services.AddSingleton<IInPutBehaviorBrowserService, BehaviorBrowserService>();
builder.Services.AddSingleton<IOutPutBehaviorBrowserService, BehaviorBrowserService>();
builder.Services.AddSingleton<IScriptAutoService, ScriptAutoService>();

builder.Services.AddSingleton<IAccountService, AccountService>();
builder.Services.AddSingleton<IProductService, ProductService>();

builder.Services.AddHostedService<Worker>();



var host = builder.Build();
host.Run();
