

using FinanceApp.Domain.Core;
using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Models;

namespace FinanceApp.Domain.Interfaces
{
    public interface IIngresoRepository : IBaseRepository<Ingreso>
    {
        Task<List<IngresoModels>> GetByUserId(int usuarioId);
        Task<decimal> GetSaldoPorUsuario(int usuarioId);
        Task<decimal> GetTotalRecurrente(int usuarioId);
    }
}
