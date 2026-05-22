using MinhaApiComSQLite.DTOs;

namespace MinhaApiComSQLite.Services
{
    public interface ICategoriaService
    {
        Task<List<CategoriaResponseDto>> GetAllAsync();
        Task<CategoriaResponseDto?> GetByIdAsync(int id);
        Task<(CategoriaResponseDto? Categoria, string? Error)> CreateAsync(CategoriaCreateDto dto);
        Task<(CategoriaResponseDto? Categoria, string? Error)> UpdateAsync(int id, CategoriaUpdateDto dto);
        Task<(bool Success, string? Error)> DeleteAsync(int id);
    }
}
