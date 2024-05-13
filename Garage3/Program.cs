using Garage3.Models.Entities;
using Garage3.Models.Persistence;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Text;
using Garage3.Models.Config;
using Garage3.Services;
using Garage3.NewFolder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<GarageMVCContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("GarageMVCContext") ?? throw new InvalidOperationException("Connection string 'GarageMVCContext' not found.")));
builder.Services.AddScoped<IGetVehicleTypeService, GetVehicleTypeService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    await app.SeedDataAsync();
}


_ = new Config();



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
