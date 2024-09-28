using FinanceApp.Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Infraestructure.Core
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private DbSet<TEntity> _entities;

        protected BaseRepository(DbContext context) 
        { 
            this._context = context;
            this._entities = this._context.Set<TEntity>();
        }


        public Task DeleteById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public async Task<TEntity> GetById(int Id)
        {
            return await _entities.FindAsync(Id);
        }

        public async Task Save(TEntity entity)
        {
            _entities.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
           _entities.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
