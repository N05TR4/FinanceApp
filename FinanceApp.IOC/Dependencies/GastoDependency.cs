

using FinanceApp.Domain.Interfaces;
using FinanceApp.Infraestructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceApp.IOC.Dependencies
{
    public static class GastoDependency
    {
        public static void AddGastoDependency(this IServiceCollection services) 
        {
            services.AddScoped<IGastoRepository, GastoRepository>();
        }
    }
}
