

using AutoMapper;
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
            var Ingreso = await _dbContext.Ingreso
                .Where(I => I.UsuarioID == usuarioId && I.Estado == true)
                .ToListAsync();

            var IngresoModels = _mapper.Map<List<IngresoModels>>(Ingreso);

            return IngresoModels;
        }

        public async Task<decimal> GetSaldoPorUsuario(int usuarioId)
        {
            try
            {
                var result = await _dbContext.Ingreso
                    .FromSqlRaw("SELECT dbo.fn_SaldoPorUsuario({0})", usuarioId)
                    .Select(i => (decimal)i.Monto)
                    .FirstOrDefaultAsync();

                return result;

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
                var result = await _dbContext.Ingreso
                    .FromSqlRaw("SELECT dbo.fn_TotalRecurrente({0}, {1})", usuarioId, tipo)
                    .Select(i => (decimal)i.Monto)
                    .FirstOrDefaultAsync();

                return result;
            }catch (SqlException ex)
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
