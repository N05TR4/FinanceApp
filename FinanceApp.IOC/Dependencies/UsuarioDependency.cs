

using FinanceApp.Domain.Interfaces;
using FinanceApp.Infraestructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceApp.IOC.Dependencies
{
    public static class UsuarioDependency
    {
        public static void AddUsuarioDependency(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        }
    }
}
