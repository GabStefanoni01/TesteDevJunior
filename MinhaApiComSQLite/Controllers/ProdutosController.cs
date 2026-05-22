using Microsoft.AspNetCore.Mvc;
using MinhaApiComSQLite.DTOs;
using MinhaApiComSQLite.Services;

namespace MinhaApiComSQLite.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutosController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResultDto<ProdutoResponseDto>>> GetProdutos(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var produtos = await _produtoService.GetPagedAsync(pageNumber, pageSize);
            return Ok(produtos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProdutoResponseDto>> GetProduto(int id)
        {
            var produto = await _produtoService.GetByIdAsync(id);
            if (produto is null)
            {
                return NotFound(new { error = "Produto nao encontrado." });
            }

            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoResponseDto>> CreateProduto([FromBody] ProdutoCreateDto dto)
        {
            var result = await _produtoService.CreateAsync(dto);
            if (result.Error is not null)
            {
                return BadRequest(new { error = result.Error });
            }

            return CreatedAtAction(nameof(GetProduto), new { id = result.Produto!.Id }, result.Produto);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProdutoResponseDto>> UpdateProduto(int id, [FromBody] ProdutoUpdateDto dto)
        {
            var result = await _produtoService.UpdateAsync(id, dto);
            if (result.Error == "Produto nao encontrado.")
            {
                return NotFound(new { error = result.Error });
            }

            if (result.Error is not null)
            {
                return BadRequest(new { error = result.Error });
            }

            return Ok(result.Produto);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            var deleted = await _produtoService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound(new { error = "Produto nao encontrado." });
            }

            return NoContent();
        }

        [HttpPost("{id:int}/desconto")]
        public async Task<ActionResult<DescontoResponseDto>> CalcularDesconto(int id, [FromBody] DescontoRequestDto dto)
        {
            var result = await _produtoService.CalcularDescontoAsync(id, dto);
            if (result.Error == "Produto nao encontrado.")
            {
                return NotFound(new { error = result.Error });
            }

            if (result.Error is not null)
            {
                return BadRequest(new { error = result.Error });
            }

            return Ok(result.Desconto);
        }
    }
}
