using AutoMapper;
using FinanceApp.API.Models.Categoria;
using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Interfaces;
using FinanceApp.Infraestructure.Context;
using FinanceApp.Infraestructure.Exceptions;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinanceApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;
        private readonly FinanceAppDbContext _dbContext;

        public CategoriaController(ICategoriaRepository categoriaRepository, IMapper mapper, FinanceAppDbContext dbContext)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
            _dbContext = dbContext;
            
        }


        // GET: api/<CategoriaController>
        [HttpGet("getAllCategoria")]
        public async Task<IActionResult> GetAllCategoria()
        {
            try
            {
                var categoria = await _categoriaRepository.GetAll();

                if (categoria == null || !categoria.Any())
                {
                    return NotFound(new { message = "No se encontraron categorías." });
                }
                return Ok(categoria);

            }catch (CategoriaException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Ocurrió un error al procesar la solicitud.", details = ex.Message });
            }
        }

        // GET api/<CategoriaController>/5
        [HttpGet("getCategoriaById/{id}")]
        public async Task<IActionResult> GetCAtegoriaById(int id)
        {
            try
            {
                var categoria = await _categoriaRepository.GetById(id);

                if (categoria is null)
                {
                    return NotFound(new { message = $"Categoría con ID {id} no encontrada." });
                }
                return Ok(categoria);

            }
            catch (CategoriaException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
            }
            
        }

        // POST api/<CategoriaController>
        [HttpPost("createCategoria")]
        public async Task<IActionResult> CreateCAtegoria([FromBody] CategoriaCreate categoria)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Si UsuarioID tiene un valor, verificar si el Usuario existe
                if (categoria.UsuarioID.HasValue)
                {
                    var usuarioExistente = await _dbContext.Usuario.FindAsync(categoria.UsuarioID.Value);
                    if (usuarioExistente == null)
                    {
                        return NotFound(new { message = "El Usuario asociado no existe." });
                    }
                }

                var categorias = _mapper.Map<Categoria>(categoria);

                await _categoriaRepository.Save(categorias);

                return CreatedAtAction(nameof(GetCAtegoriaById), new { id = categorias.CategoriaID }, categorias);


            }catch  (CategoriaException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
            }
            
        }

        // PUT api/<CategoriaController>/5
        [HttpPut("updateCategoria/{id}")]
        public async Task<IActionResult> UpdateCategoria(int id, [FromBody] CategoriaUpdate categoria)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Verificar si la categoría existe
                var exist = await _categoriaRepository.GetById(id);
                if (exist == null)
                {
                    return NotFound(new { message = "La Categoria con el ID: {0} no fue encontrada.", id });
                }

                // mapear los cambios
                _mapper.Map(categoria, exist);

                // guardar los cambios
                await _categoriaRepository.Update(exist);
                return Ok();

            }catch (CategoriaException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
            }


        }

        // DELETE api/<CategoriaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
