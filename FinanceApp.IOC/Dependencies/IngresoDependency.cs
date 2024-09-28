

using FinanceApp.Domain.Interfaces;
using FinanceApp.Infraestructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceApp.IOC.Dependencies
{
    public static class IngresoDependency
    {
        public static void AddIngresoDependency(this IServiceCollection services)
        {
            services.AddScoped<IIngresoRepository, IngresoRepository>();
        }
    }
}
