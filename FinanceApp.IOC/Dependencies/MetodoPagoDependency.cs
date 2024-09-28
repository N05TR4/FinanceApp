

using FinanceApp.Domain.Interfaces;
using FinanceApp.Infraestructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceApp.IOC.Dependencies
{
    public static class MetodoPagoDependency
    {
        public static void AddMetodoPagoDependency(this IServiceCollection services)
        {
            services.AddScoped<IMetodoPagoRepository, MetodoPagoRepository>();
        }
    }
}
