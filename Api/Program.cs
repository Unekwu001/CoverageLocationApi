using ipNXSalesPortalApis.Models;
using ipNXSalesPortalApis.Services.GoogleServices;
using ipNXSalesPortalApis.Services.SalesPortalServices;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddScoped<ICoverageService, CoverageService>();
builder.Services.AddScoped<IGoogleGeoCodingService, GoogleGeoCodingService>();


// Database connection and Identity configuration
builder.Services.AddDbContext<CoverageDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
