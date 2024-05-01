using Microsoft.Identity.Client;
using Postal.Data.Abstraction;
using Postal.Data.Business;
using Postal.Store.Abstraction;

using Postal.Stores.Buisness;
using System.Diagnostics.Metrics;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();

// Configure session options
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
});


var configbuilder = new ConfigurationBuilder()
		   .AddJsonFile("appSettings.json");
var Configuration = configbuilder.Build();



builder.Services.AddScoped<IDatabaseManager, DatabaseManager>();
builder.Services.AddScoped<IDatabaseHandler, DatabaseHandler>();
builder.Services.AddScoped<IParamManager, ParamManager>();

builder.Services.AddScoped<IAuthStore, AuthStore>();
builder.Services.AddScoped<IAccountStore, AccountStore>();



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
	pattern: "{controller=Login}/{action=Index}/{id?}");

app.UseSession();

app.Run();
