

namespace FinanceApp.Domain.Core
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task Save(TEntity entity);
        Task<List<TEntity>> GetAll();
        Task<TEntity> GetById(int Id);
        Task Update(TEntity entity);
        Task DeleteById(int Id);


    }
}
