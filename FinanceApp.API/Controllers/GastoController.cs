using AutoMapper;
using FinanceApp.API.Models.Gasto;
using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Interfaces;
using FinanceApp.Infraestructure.Context;
using FinanceApp.Infraestructure.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinanceApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GastoController : ControllerBase
    {

        private readonly IGastoRepository _gastoRepository;
        private readonly IMapper _mapper;
        private readonly FinanceAppDbContext _dbContext;

        public GastoController(IGastoRepository gastoRepository, IMapper mapper, FinanceAppDbContext dbContext)
        {
            _gastoRepository = gastoRepository;
            _mapper = mapper;
            _dbContext = dbContext;
            
        }

        // GET: api/<GastoController>
        [HttpGet("getAllGasto")]
        public async Task<IActionResult> GetAllGasto()
        {
            try
            {
                var gasto = await _gastoRepository.GetAll();

                if (gasto == null || !gasto.Any())
                {
                    return NotFound(new { message = "No se encontraron gastos registrados." });
                }
                return Ok(gasto);
            }
            catch (GastoException ex)
            {

                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al procesar la solicitud.", details = ex.Message });

            }
        }




        // GET api/<GastoController>/5
        [HttpGet("getGastoById/{id}")]
        public async Task<IActionResult> GetGastoById(int id)
        {
            try
            {
                var gasto = await _gastoRepository.GetById(id);

                if (gasto == null)
                {
                    return NotFound(new { message = $"El Gasto con el ID {id} no encontrado" });
                }
                return Ok(gasto);
            }
            catch (GastoException ex)
            {

                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al procesar la solicitud.", details = ex.Message });

            }
        }


        [HttpGet("getGastoByUsuarioId/{usuarioId}")]
        public async Task<IActionResult> GetGastoByUsuarioId(int usuarioId)
        {
            try
            {
                var Usuario = await _dbContext.Usuario.FindAsync(usuarioId);
                if (Usuario == null)
                {
                    return NotFound(new { message = $"El Usuario con el ID {usuarioId} no Existe." });

                }

                var gasto = await _gastoRepository.GetByUserId(usuarioId);
                if(gasto == null)
                {
                    return NotFound(new { message = $"El Usuario con el ID {usuarioId} no tiene registros de gastos." });
                }

                return Ok(gasto);
            }
            catch (GastoException ex)
            {

                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al procesar la solicitud.", details = ex.Message });

            }
        }


        [HttpGet("getTotalRecurrente/{usuarioId}")]
        public async Task<IActionResult> GetTotalRecurrente(int usuarioId)
        {
            try
            {
                // verificar si el usuario existe
                var Usuario = await _dbContext.Usuario.FindAsync(usuarioId);
                if (Usuario == null)
                {
                    return NotFound(new { message = $"El Usuario con el ID {usuarioId} no Existe." });
                }

                var totalRecurrente = await _gastoRepository.GetTotalRecurrente(usuarioId);
                if (totalRecurrente == null)
                {
                    return NotFound(new { message = $"El Usuario con el ID {usuarioId} no contiene gastos recurrentes" });
                }

                return Ok(new { Total_Recurrente = totalRecurrente });

            }
            catch (GastoException ex)
            {

                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al procesar la solicitud.", details = ex.Message });

            }
        }



        // POST api/<GastoController>
        [HttpPost("createGasto")]
        public async Task<IActionResult> CreateGasto([FromBody] GastoCreate gastoCreate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Mapeo de las entidades
                var gastos = _mapper.Map<Gasto>(gastoCreate);

                //Guardar nuevo registro
                await _gastoRepository.Save(gastos);

                return Ok();

            }
            catch (GastoException ex)
            {

                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al procesar la solicitud.", details = ex.Message });

            }
        }

        // PUT api/<GastoController>/5
        [HttpPut("updateGasto/{id}")]
        public async Task<IActionResult> UpdateGasto(int id, [FromBody] GastoUpdate gastoUpdate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Verificar existe el ingreso
                var exist = await _gastoRepository.GetById(id);
                if (exist == null)
                {
                    return NotFound(new { message = "El gasto con el ID: {0} no fue encontrado.", id });
                }

                // Mapear los cambios
                _mapper.Map(gastoUpdate, exist);

                //Actualizar fecha de modificacion
                exist.FechaModificacion = DateTime.Now;

                //guardar los cambios
                await _gastoRepository.Update(exist);

                return Ok();

            }
            catch (GastoException ex)
            {

                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al procesar la solicitud.", details = ex.Message });

            }
        }

        // DELETE api/<GastoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
