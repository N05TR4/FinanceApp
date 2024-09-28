

using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Interfaces;
using FinanceApp.Infraestructure.Context;
using FinanceApp.Infraestructure.Core;

namespace FinanceApp.Infraestructure.Repositories
{
    public class TipoRepository : BaseRepository<Tipo>, ITipoRepository
    {
        private readonly FinanceAppDbContext _dbContext;

        public TipoRepository(FinanceAppDbContext context) : base(context) 
        {
            _dbContext = context;
            
        }

    }
}
