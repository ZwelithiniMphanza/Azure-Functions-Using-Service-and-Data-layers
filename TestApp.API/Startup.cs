using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using TestApp.API;
using TestApp.Data.Interfaces;
using TestApp.Data.Repositories;
using TestApp.Service.Interfaces;
using TestApp.Service.Services;

[assembly: FunctionsStartup(typeof(Startup))]
namespace TestApp.API
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        { 
            builder.Services.AddTransient<IPersonDatabaseRepository, PersonDatabaseRepository>();
            builder.Services.AddTransient<IPersonDatabaseService, PersonDatabaseService>();
        }
    }
}
