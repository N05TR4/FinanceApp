

using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Interfaces;
using FinanceApp.Infraestructure.Context;
using FinanceApp.Infraestructure.Core;

namespace FinanceApp.Infraestructure.Repositories
{
    public class MetodoPagoRepository : BaseRepository<MetodoPago>, IMetodoPagoRepository
    {
        private readonly FinanceAppDbContext _dbContext;

        public MetodoPagoRepository(FinanceAppDbContext context) : base(context) 
        {
            _dbContext = context;
            
        }
    }
}
