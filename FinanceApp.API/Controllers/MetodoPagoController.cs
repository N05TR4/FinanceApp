using AutoMapper;
using FinanceApp.API.Models.MetodoPago;
using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Interfaces;
using FinanceApp.Infraestructure.Exceptions;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinanceApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetodoPagoController : ControllerBase
    {
        private readonly IMetodoPagoRepository _metodoPagoRepository;
        private readonly IMapper _mapper;

        public MetodoPagoController(IMetodoPagoRepository metodoPagoRepository, IMapper mapper)
        {
            _mapper = mapper;
            _metodoPagoRepository = metodoPagoRepository;
               
        }


        // GET: api/<MetodoPagoController>
        [HttpGet("getAllMetodoPago")]
        public async Task<IActionResult> GetAllMetodoPago()
        {
            try
            {
                var metodoPago = await _metodoPagoRepository.GetAll();

                if (metodoPago == null || !metodoPago.Any()) 
                {
                    return NotFound(new { message = "No se encontraron Metodos de pagos." });
                }

                return Ok(metodoPago);

            }catch(MetodoPagoException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Ocurrió un error al procesar la solicitud.", details = ex.Message });
            }
        }

        // GET api/<MetodoPagoController>/5
        [HttpGet("getMetodoPagoById/{id}")]
        public async Task<IActionResult> GetMetodoPagoById(int id)
        {
            try
            {
               var metodoPago = await _metodoPagoRepository.GetById(id);

                if(metodoPago == null)
                {
                    return NotFound(new { message = $"Metodo de pago con ID {id} no encontrado." });
                }

                return Ok(metodoPago);

            }catch(MetodoPagoException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        // POST api/<MetodoPagoController>
        [HttpPost("createMetodoPago")]
        public async Task<IActionResult> CreateMetodoPago([FromBody] MetodoPagoCreate metodoPago)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Mapeo de las entidades
                var metodoPagos = _mapper.Map<MetodoPago>(metodoPago);

                //Guardar nuevo registro
                await _metodoPagoRepository.Save(metodoPagos);

                return Ok();

            }catch(MetodoPagoException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        // PUT api/<MetodoPagoController>/5
        [HttpPut("updateMetodoPago/{id}")]
        public async Task<IActionResult> UpdateMetodoPago(int id, [FromBody] MetodoPagoUpdate metodoPago)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Verificar si el metodo de pago existe
                var exist = await _metodoPagoRepository.GetById(id);
                if (exist == null)
                {
                    return NotFound(new { message = "El metodo de pago con el ID: {0} no fue encontrado.", id });
                }

                // Mapear los cambios
                _mapper.Map(metodoPago, exist);

                //Actualizar fecha de modificacion
                exist.FechaModificacion = DateTime.Now;

                //guardar los cambios
                await _metodoPagoRepository.Update(exist);

                return Ok();


            }
            catch (MetodoPagoException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        // DELETE api/<MetodoPagoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
