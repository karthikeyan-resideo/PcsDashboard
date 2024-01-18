using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PcsKpiApi;
using PcsKpiApi.DataAccess;
using PcsKpiApi.Services;
using Titan.Common.Configurations;
using Titan.UFC.Common.ExceptionMiddleWare;
using Titan.UFC.Logger;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
var Configuration = new ConfigurationBuilder()
                        .SetBasePath(env?.ContentRootPath)
                        .AddJsonFile($"appsettings.{env?.EnvironmentName}.json", optional: false, reloadOnChange: true)
                        .Build();
// Add services to the container.
var services = builder.Services;
services.AddSingleton(Configuration);
services.AddLogger(Configuration);
services.AddScoped<ISonarqubeService, SonarqubeService>();
services.AddScoped<ISonarqubeDataAccess, SonarqubeDataAccess>();
Titan.Common.Configurations.Settings sql = new()
{
Host = Configuration.GetSection("SQL:DBHost").Value,
DBName = Configuration.GetSection("SQL:DBName").Value,
Username = Configuration.GetSection("SQL:DBUserName").Value,
Password = Configuration.GetSection("SQL:DBPassword").Value
};
string sqlconn = SettingsExtensions.GetSQLConnectionString(sql);
services.AddDbContextPool<ApplicationDbContext>(options => options.UseSqlServer(sqlconn));
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
var app = builder.Build();
app.UseMiddleware<ScopedLoggingMiddleware>();
app.UseCustomException();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Configure the HTTP request pipeline.
app.UseRouting();
app.UseEndpoints(endpoints =>
{
endpoints.MapControllers();
});

app.Run();

static class ServiceExtension
{
    public static IServiceCollection AddLogger(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("ConsoleLogEnabled"))
        {
            services.AddSingleton<ICustomLogger, CustomLogger>();
        }
        else
        {
            services.AddSingleton<ICustomLogger, TitanWebApiEventSourceLogger>();
        }
        return services;
    }
}
