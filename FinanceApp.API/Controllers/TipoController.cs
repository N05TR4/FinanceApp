using AutoMapper;
using FinanceApp.API.Models.Tipo;
using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Interfaces;
using FinanceApp.Infraestructure.Exceptions;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinanceApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoController : ControllerBase
    {
        private readonly ITipoRepository _tipoRepository;
        private readonly IMapper _mapper;

        public TipoController(ITipoRepository tipoRepository, IMapper mapper)
        {
            _mapper = mapper;
            _tipoRepository = tipoRepository;
        }



        // GET: api/<TipoController>
        [HttpGet("getAllTipo")]
        public async Task<IActionResult> GetAllTipo()
        {
        
            try
            {
                var tipo = await _tipoRepository.GetAll();

                if (tipo == null || !tipo.Any())
                {
                    return NotFound(new { message = "No se encontraron tipos de transacciones." });
                }

                return Ok(tipo);
            }
            catch (TipoException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Ocurrió un error al procesar la solicitud.", details = ex.Message });
            }
        }

        // GET api/<TipoController>/5
        [HttpGet("getTipoById/{id}")]
        public async Task<IActionResult> GetTipoById(int id)
        {
            try
            {
                var tipo = await _tipoRepository.GetById(id);
                if (tipo == null)
                    return NotFound(new { message = $"Tipo de transacción con ID {id} no encontrado." });

                return Ok(tipo);
            }
            catch (TipoException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        // POST api/<TipoController>
        [HttpPost("createTipo")]
        public async Task<IActionResult> CreateTipo([FromBody] TipoCreate tipo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var tipos = _mapper.Map<Tipo>(tipo);

                await _tipoRepository.Save(tipos);

                return Ok();

            }
            catch (TipoException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        // PUT api/<TipoController>/5
        [HttpPut("updateTipo/{id}")]
        public async Task<IActionResult> UpdateTipo(int id, [FromBody] TipoUpdate tipo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Verificar si el tipo de transacción existe
                var exist = await _tipoRepository.GetById(id);
                if (exist == null)
                {
                    return NotFound(new { message = "El tipo de transacción con el ID: {0} no fue encontrado.", id });
                }


                // mapear los cambios
                _mapper.Map(tipo, exist);

                // guardar cambios
                await _tipoRepository.Update(exist);

                return Ok();

            }
            catch (TipoException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        // DELETE api/<TipoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
