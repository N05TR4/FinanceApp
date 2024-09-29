using AutoMapper;
using FinanceApp.API.Models.Usuario;
using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Interfaces;
using FinanceApp.Infraestructure.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinanceApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
        }



        // GET: api/<UsuarioController>
        [HttpGet("getAllUsuario")]
        public async Task<IActionResult> GetAllUsuario()
        {
            try
            {
                var usuario = await _usuarioRepository.GetAll();

                if (usuario == null || !usuario.Any())
                {
                    return NotFound(new { message = "No se encontraron usuarios." });
                }

                return Ok(usuario);
            }
            catch (UsuarioException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Ocurrió un error al procesar la solicitud.", details = ex.Message });
            }
        }

        // GET api/<UsuarioController>/5
        [HttpGet("getUsuarioById/{id}")]
        public async Task<IActionResult> GetUsuarioById(int id)
        {
            try
            {
                var usuario = await _usuarioRepository.GetById(id);
                if (usuario == null)
                {
                    return NotFound(new { message = $"El usuario con el ID {id} no fue encontrado." });
                }
                return Ok(usuario);

            }
            catch (UsuarioException ex)
            {
                return BadRequest(new {message = ex.Message});
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Ocurrió un error al procesar la solicitud.", details = ex.Message });
            }
        }

        // POST api/<UsuarioController>
        [HttpPost("createUsuario")]
        public async Task<IActionResult> CreateUsuario([FromBody] UsuarioCreate usuario)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var usuarios = _mapper.Map<Usuario>(usuario);
                await _usuarioRepository.Save(usuarios);

                return Ok();
            }
            catch (UsuarioException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("updateUsuario/{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UsuarioUpdate usuario)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //Verificar si el Usuario existe
                var exist = await _usuarioRepository.GetById(id);
                if (exist == null)
                {
                    return NotFound(new { message = "El usuario con el ID: {0} no fue encontrado.", id });
                }
                
                //Mapear los cambios
                _mapper.Map(usuario, exist);

                //Actualizar fecha de modificacion
                exist.FechaModificacion = DateTime.Now;

                //guardar los cambios
                await _usuarioRepository.Update(exist);

                return Ok();
            }
            catch (UsuarioException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
