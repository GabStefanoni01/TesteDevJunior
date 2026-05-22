using MinhaApiComSQLite.DTOs;

namespace MinhaApiComSQLite.Services
{
    public interface IProdutoService
    {
        Task<PagedResultDto<ProdutoResponseDto>> GetPagedAsync(int pageNumber, int pageSize);
        Task<ProdutoResponseDto?> GetByIdAsync(int id);
        Task<(ProdutoResponseDto? Produto, string? Error)> CreateAsync(ProdutoCreateDto dto);
        Task<(ProdutoResponseDto? Produto, string? Error)> UpdateAsync(int id, ProdutoUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<(DescontoResponseDto? Desconto, string? Error)> CalcularDescontoAsync(int id, DescontoRequestDto dto);
        Task<RelatorioEstatisticasDto> GetRelatorioAsync();
    }
}
