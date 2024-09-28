

using FinanceApp.Domain.Core;
using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Models;

namespace FinanceApp.Domain.Interfaces
{
    public interface IGastoRepository : IBaseRepository<Gasto>
    {
        Task<List<GastoModels>> GetByUserId(int usuarioId);
        Task<decimal> GetTotalRecurrente(int usuarioId);
        
    }
}
