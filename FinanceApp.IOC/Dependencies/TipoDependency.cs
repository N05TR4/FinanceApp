

using FinanceApp.Domain.Interfaces;
using FinanceApp.Infraestructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceApp.IOC.Dependencies
{
    public static class TipoDependency
    {
        public static void AddTipoDependency(this IServiceCollection services)
        {
            services.AddScoped<ITipoRepository, TipoRepository>();  
        }
    }
}
