using Eye.Application.SeleniumServices;
using Eye.Application.Services;
using Eye.Application.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddTransient<IHttpClientService, HttpClientService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAutoService, AutoService>();
builder.Services.AddScoped<IBrowserService, BrowserService>();
builder.Services.AddScoped<IInPutBehaviorBrowserService, BehaviorBrowserService>();
builder.Services.AddScoped<IOutPutBehaviorBrowserService, BehaviorBrowserService>();
builder.Services.AddScoped<IScriptAutoService, ScriptAutoService>();
builder.Services.AddScoped<IProxyServce, ProxyServce>();



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
