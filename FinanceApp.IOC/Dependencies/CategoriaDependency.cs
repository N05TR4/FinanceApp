using FinanceApp.Domain.Interfaces;
using FinanceApp.Infraestructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceApp.IOC.Dependencies
{
    public static class CategoriaDependency
    {
        public static void AddCategoriaDependency(this IServiceCollection services)
        {
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        }
    }
}
