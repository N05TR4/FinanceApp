using AutoMapper;
using FinanceApp.API.Models.Ingreso;
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
    public class IngresoController : ControllerBase
    {
        private readonly IIngresoRepository _ingresoRepository;
        private readonly FinanceAppDbContext _dbContext;
        private IMapper _mapper;
        


        public IngresoController(IIngresoRepository ingresoRepository, IMapper mapper, FinanceAppDbContext dbContext) 
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _ingresoRepository = ingresoRepository;

        }


        // GET: api/<IngresoController>
        [HttpGet("getAllIngreso")]
        public async Task<IActionResult> GetAllIngreso()
        {
            try
            {
                var ingreso = await _ingresoRepository.GetAll();

                if (ingreso == null || !ingreso.Any())
                {
                    return NotFound(new { message = "No se encontraron ingresos registrados." });
                }

                return Ok(ingreso);

            }
            catch (IngresoException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Ocurrió un error al procesar la solicitud.", details = ex.Message });
            }
        }

        // GET api/<IngresoController>/5
        [HttpGet("getIngresoById/{id}")]
        public async Task<IActionResult> GetIngresoById(int id)
        {
            try
            {
                var ingreso = await _ingresoRepository.GetById(id);

                if(ingreso == null)
                {
                    return NotFound(new { message = $"El Ingreso con ID {id} no encontrada." });
                }
                return Ok(ingreso);
            }
            catch (IngresoException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
            }
        }


        // Obtener todos los ingresos de un usuario
        [HttpGet("getIngresoByUsuarioId/{id}")]
        public async Task<IActionResult> GetIngresoByUsuarioId(int id)
        {
            try
            {
                // verificar si el usuario existe
                var Usuario = await _dbContext.Usuario.FindAsync(id);
                if (Usuario == null)
                {
                    return NotFound(new { message = $"El Usuario con el ID {id} no Existe." });
                }

                var ingresoByUsuario = await _ingresoRepository.GetByUserId(id);
                if(ingresoByUsuario == null)
                {
                    return NotFound(new { message = $"El Usuario con el ID {id} no tiene registros de ingresos." });
                }

                return Ok(ingresoByUsuario);

            }
            catch (IngresoException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
            }
        }


        //Obtener el saldo total de los ingresos de un usuario
        [HttpGet("getSaldoPorUsuario/{usuarioId}")]
        public async Task<IActionResult> GetSaldoPorUsuario(int usuarioId)
        {
            try
            {
                // verificar si el usuario existe
                var Usuario = await _dbContext.Usuario.FindAsync(usuarioId);
                if (Usuario == null)
                {
                    return NotFound(new { message = $"El Usuario con el ID {usuarioId} no Existe." });
                }

                var saldoPorUsuario = await _ingresoRepository.GetSaldoPorUsuario(usuarioId);
                if(saldoPorUsuario == null)
                {
                    return NotFound(new { message = $"El Usuario con el ID {usuarioId} no contiene saldo" });
                }
                return Ok(new { saldo = saldoPorUsuario });
            }
            catch (IngresoException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
            }
        }


        //Obtener el total de los ingresos recurrentes de un usuario
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

                var totalRecurrente = await _ingresoRepository.GetTotalRecurrente(usuarioId);
                if(totalRecurrente == null)
                {
                    return NotFound(new { message = $"El Usuario con el ID {usuarioId} no contiene ingresos recurrentes" });
                }

                return Ok(new { totalRecurrente = totalRecurrente });


            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST api/<IngresoController>
        [HttpPost("createIngreso")]
        public async Task<IActionResult> CreateIngreso([FromBody] IngresoCreate ingresoCreate)
        {
            
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Mapeo de las entidades
                var ingresos = _mapper.Map<Ingreso>(ingresoCreate);

                //Guardar nuevo registro
                await _ingresoRepository.Save(ingresos);

                return Ok();
            }
            catch (IngresoException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        // PUT api/<IngresoController>/5
        [HttpPut("updateIngreso{id}")]
        public async Task<IActionResult> UpdateIngreso(int id, [FromBody] IngresoUpdate ingresoUpdate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Verificar existe el ingreso
                var exist = await _ingresoRepository.GetById(id);
                if (exist == null)
                {
                    return NotFound(new { message = "El ingreso con el ID: {0} no fue encontrado.", id });
                }

                // Mapear los cambios
                _mapper.Map(ingresoUpdate, exist);

                //Actualizar fecha de modificacion
                exist.FechaModificacion = DateTime.Now;

                //guardar los cambios
                await _ingresoRepository.Update(exist);

                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

        // DELETE api/<IngresoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
