

using FinanceApp.Domain.Core;
using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Interfaces;
using FinanceApp.Infraestructure.Context;
using FinanceApp.Infraestructure.Core;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Infraestructure.Repositories
{
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        private readonly FinanceAppDbContext _dbContext;

        public CategoriaRepository(FinanceAppDbContext context) : base(context)
        {
            _dbContext = context;
        }

       
    }
}
