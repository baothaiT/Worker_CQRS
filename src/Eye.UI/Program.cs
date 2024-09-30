using Eye.Application.SeleniumServices;
using Eye.Application.Services;
using Eye.Application.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddTransient<IHttpClientService, HttpClientService>();
builder.Services.AddScoped<IProductClientService, ProductClientService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddSingleton<IAutoService, AutoService>();
builder.Services.AddSingleton<IBrowserService, BrowserService>();
builder.Services.AddSingleton<IInPutBehaviorBrowserService, BehaviorBrowserService>();
builder.Services.AddSingleton<IOutPutBehaviorBrowserService, BehaviorBrowserService>();
builder.Services.AddSingleton<IScriptAutoService, ScriptAutoService>();
builder.Services.AddScoped<IProxyClientServce, ProxyClientServce>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
