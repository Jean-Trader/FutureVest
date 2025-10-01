using Application.Interfaces;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Entities;
using Persistence.Interfaces;
using Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);


var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<VestAppDbContext>(options =>
    options.UseSqlServer(ConnectionString));

builder.Services.AddScoped<ICountryServices, CountryServices>();
builder.Services.AddScoped<ICountryIndicatorService, CountryIndicatorService>();
builder.Services.AddScoped<IMacroIndicatorService, MacroIndicatorServices>();
builder.Services.AddScoped<IRateReturnService,RateReturnService>();
builder.Services.AddScoped<ICommonRepo<CountryIndicator>, CountryIndicatorRepo>();
builder.Services.AddScoped<IRateReturnRepo, RateReturnRepo>();
builder.Services.AddScoped<ICommonRepo<Country>,CountryRepo>();
builder.Services.AddScoped<ICommonRepo<MacroIndicator>, MacroIndicatorRepo>();
builder.Services.AddScoped<IRanking, CalculateRanking>();
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
