using Microsoft.AspNetCore.Mvc;
using MinhaApiComSQLite.DTOs;
using MinhaApiComSQLite.Services;

namespace MinhaApiComSQLite.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelatoriosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public RelatoriosController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet("estatisticas")]
        public async Task<ActionResult<RelatorioEstatisticasDto>> GetEstatisticas()
        {
            var relatorio = await _produtoService.GetRelatorioAsync();
            return Ok(relatorio);
        }
    }
}
