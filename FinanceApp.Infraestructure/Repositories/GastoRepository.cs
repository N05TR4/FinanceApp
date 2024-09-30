using AutoMapper;
using FinanceApp.Infraestructure.Exceptions;
using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Interfaces;
using FinanceApp.Domain.Models;
using FinanceApp.Infraestructure.Context;
using FinanceApp.Infraestructure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;


namespace FinanceApp.Infraestructure.Repositories
{
    public class GastoRepository : BaseRepository<Gasto>, IGastoRepository
    {
        private readonly FinanceAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public GastoRepository(FinanceAppDbContext context, IMapper mapper) : base(context) 
        {
            _dbContext = context;
            _mapper = mapper;
            
        }

        public async Task<List<GastoModels>> GetByUserId(int usuarioId)
        {
            try
            {
                var gastos = await _dbContext.Gasto
               .Where(g => g.UsuarioID == usuarioId && g.Estado == true)
               .ToListAsync();

                if (gastos == null || !gastos.Any())
                {
                    throw new GastoException($"No se encontraron gastos para el usuario con ID {usuarioId}.");
                }

                var gastoModels = _mapper.Map<List<GastoModels>>(gastos);

                return gastoModels;

            }catch (DbUpdateException ex)
            {
                throw new GastoException($"Error al obtener los gastos del usuario con ID {usuarioId}: {ex.Message}");
            }
            catch (Exception ex)
            {

                throw new GastoException($"Se ha producido un error al obtener los gastos del usuario con ID {usuarioId}: {ex.Message}");
            }
        }

        public async Task<decimal> GetTotalRecurrente(int usuarioId)
        {
            try
            {
                var tipo = "Gasto";
                var result = await _dbContext.Database
                    .SqlQueryRaw<TotalRecurrenteResult>("SELECT dbo.fn_TotalRecurrente({0}, {1}) AS TotalRecurrente", usuarioId, tipo)
                    .FirstOrDefaultAsync();

                return result?.TotalRecurrente ?? 0;
            }catch (SqlException ex)
            {
                throw new GastoException($"Error en la base de datos al obtener el total recurrente para el usuario con ID {usuarioId}: {ex.Message}");
            }
            catch (Exception ex)
            {

                throw new GastoException($"Se ha producido un error al obtener el total recurrente para el usuario con ID {usuarioId}: {ex.Message}");
            }
            
        }

        

        
    }
}
