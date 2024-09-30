

using AutoMapper;
using FinanceApp.API.Models.Ingreso;
using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Interfaces;
using FinanceApp.Domain.Models;
using FinanceApp.Infraestructure.Context;
using FinanceApp.Infraestructure.Core;
using FinanceApp.Infraestructure.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Infraestructure.Repositories
{
    public class IngresoRepository : BaseRepository<Ingreso>, IIngresoRepository
    {

        private readonly FinanceAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public IngresoRepository(FinanceAppDbContext context, IMapper mapper) : base(context) 
        {
            _dbContext = context;
            _mapper = mapper;
        }


        public async Task<List<IngresoModels>> GetByUserId(int usuarioId)
        {
            try
            {
                var Ingreso = await _dbContext.Ingreso
                    .Where(I => I.UsuarioID == usuarioId && I.Estado == true)
                    .ToListAsync();

                var IngresoModels = _mapper.Map<List<IngresoModels>>(Ingreso);

                return IngresoModels;
            }
            catch (AutoMapperMappingException ex)
            {
                
                throw new Exception($"Error mapping types: {ex.Message}", ex);
            }
        }

        public async Task<decimal> GetSaldoPorUsuario(int usuarioId)
        {
            try
            {
                var result = await _dbContext.Database
                    .SqlQueryRaw<SaldoResult>("SELECT dbo.fn_SaldoPorUsuario({0}) AS Saldo", usuarioId)
                    .FirstOrDefaultAsync();

                return result?.Saldo ?? 0;
            }
            catch (SqlException ex)
            {
                throw new IngresoException($"Error al obtener el saldo del usuario con ID {usuarioId}: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new IngresoException($"Se produjo un error al obtener el saldo del usuario con ID {usuarioId}: {ex.Message}");
            }
        }


        public async Task<decimal> GetTotalRecurrente(int usuarioId)
        {
            try
            {
                var tipo = "Ingreso";
                var result = await _dbContext.Database
                    .SqlQueryRaw<TotalRecurrenteResult>("SELECT dbo.fn_TotalRecurrente({0}, {1}) AS TotalRecurrente", usuarioId, tipo)
                    .FirstOrDefaultAsync();

                return result?.TotalRecurrente ?? 0;
            }
            catch (SqlException ex)
            {
                throw new IngresoException($"Error al obtener el total recurrente para el usuario con ID {usuarioId}: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new IngresoException($"Se produjo un error al obtener el total recurrente para el usuario con ID {usuarioId}: {ex.Message}");
            }
        }
    }
}
