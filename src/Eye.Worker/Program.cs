using Eye.Worker;
using Eye.Application.Services;
using Eye.Application.SeleniumServices;
using Eye.Application.Services.Interface;
using Eye.Application.SeleniumServices.Interfaces;

var builder = Host.CreateApplicationBuilder(args);


builder.Services.AddHttpClient();
builder.Services.AddSingleton<IAutoService, AutoService>();
builder.Services.AddSingleton<IBrowserService, BrowserService>();
builder.Services.AddSingleton<IInPutBehaviorBrowserService, BehaviorBrowserService>();
builder.Services.AddSingleton<IOutPutBehaviorBrowserService, BehaviorBrowserService>();
builder.Services.AddSingleton<IScriptAutoService, ScriptAutoService>();

builder.Services.AddSingleton<IAccountService, AccountService>();
builder.Services.AddSingleton<IProductClientService, ProductClientService>();
builder.Services.AddSingleton<IProxyClientServce, ProxyClientServce>();
builder.Services.AddSingleton<IProcessWorkerService, ProcessWorkerService>();

builder.Services.AddHostedService<Worker>();



var host = builder.Build();
host.Run();
