using Microsoft.AspNetCore.Mvc;
using MinhaApiComSQLite.DTOs;
using MinhaApiComSQLite.Services;

namespace MinhaApiComSQLite.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoriaResponseDto>>> GetCategorias()
        {
            var categorias = await _categoriaService.GetAllAsync();
            return Ok(categorias);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoriaResponseDto>> GetCategoria(int id)
        {
            var categoria = await _categoriaService.GetByIdAsync(id);
            if (categoria is null)
            {
                return NotFound(new { error = "Categoria nao encontrada." });
            }

            return Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaResponseDto>> CreateCategoria([FromBody] CategoriaCreateDto dto)
        {
            var result = await _categoriaService.CreateAsync(dto);
            if (result.Error is not null)
            {
                return BadRequest(new { error = result.Error });
            }

            return CreatedAtAction(nameof(GetCategoria), new { id = result.Categoria!.Id }, result.Categoria);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoriaResponseDto>> UpdateCategoria(int id, [FromBody] CategoriaUpdateDto dto)
        {
            var result = await _categoriaService.UpdateAsync(id, dto);
            if (result.Error == "Categoria nao encontrada.")
            {
                return NotFound(new { error = result.Error });
            }

            if (result.Error is not null)
            {
                return BadRequest(new { error = result.Error });
            }

            return Ok(result.Categoria);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var result = await _categoriaService.DeleteAsync(id);
            if (result.Error == "Categoria nao encontrada.")
            {
                return NotFound(new { error = result.Error });
            }

            if (result.Error is not null)
            {
                return BadRequest(new { error = result.Error });
            }

            return NoContent();
        }
    }
}
