using MinhaApiComSQLite.DTOs;
using MinhaApiComSQLite.Models;
using MinhaApiComSQLite.Repositories;

namespace MinhaApiComSQLite.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<List<CategoriaResponseDto>> GetAllAsync()
        {
            var categorias = await _categoriaRepository.GetAllAsync();
            return categorias.Select(MapToResponse).ToList();
        }

        public async Task<CategoriaResponseDto?> GetByIdAsync(int id)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id);
            return categoria is null ? null : MapToResponse(categoria);
        }

        public async Task<(CategoriaResponseDto? Categoria, string? Error)> CreateAsync(CategoriaCreateDto dto)
        {
            var normalizedName = NormalizeName(dto.Nome);

            if (string.IsNullOrWhiteSpace(normalizedName))
            {
                return (null, "O nome da categoria e obrigatorio.");
            }

            if (await _categoriaRepository.GetByNomeAsync(normalizedName) is not null)
            {
                return (null, "Ja existe uma categoria cadastrada com este nome.");
            }

            var categoria = new Categoria { Nome = normalizedName };
            await _categoriaRepository.AddAsync(categoria);
            await _categoriaRepository.SaveChangesAsync();

            return (MapToResponse(categoria), null);
        }

        public async Task<(CategoriaResponseDto? Categoria, string? Error)> UpdateAsync(int id, CategoriaUpdateDto dto)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id);
            if (categoria is null)
            {
                return (null, "Categoria nao encontrada.");
            }

            var normalizedName = NormalizeName(dto.Nome);
            if (string.IsNullOrWhiteSpace(normalizedName))
            {
                return (null, "O nome da categoria e obrigatorio.");
            }

            var existing = await _categoriaRepository.GetByNomeAsync(normalizedName);
            if (existing is not null && existing.Id != id)
            {
                return (null, "Ja existe uma categoria cadastrada com este nome.");
            }

            categoria.Nome = normalizedName;
            _categoriaRepository.Update(categoria);
            await _categoriaRepository.SaveChangesAsync();

            return (MapToResponse(categoria), null);
        }

        public async Task<(bool Success, string? Error)> DeleteAsync(int id)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id);
            if (categoria is null)
            {
                return (false, "Categoria nao encontrada.");
            }

            if (await _categoriaRepository.HasProdutosAsync(id))
            {
                return (false, "Nao e possivel excluir uma categoria que possui produtos.");
            }

            _categoriaRepository.Delete(categoria);
            await _categoriaRepository.SaveChangesAsync();
            return (true, null);
        }

        private static CategoriaResponseDto MapToResponse(Categoria categoria)
        {
            return new CategoriaResponseDto
            {
                Id = categoria.Id,
                Nome = categoria.Nome
            };
        }

        private static string NormalizeName(string name)
        {
            return name.Trim().ToUpperInvariant();
        }
    }
}
