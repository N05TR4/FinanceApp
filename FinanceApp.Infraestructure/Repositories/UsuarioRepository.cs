

using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Interfaces;
using FinanceApp.Infraestructure.Context;
using FinanceApp.Infraestructure.Core;

namespace FinanceApp.Infraestructure.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private readonly FinanceAppDbContext _dbContext;

        public UsuarioRepository(FinanceAppDbContext context) : base(context) 
        {
            _dbContext = context;
        }
    }
}
