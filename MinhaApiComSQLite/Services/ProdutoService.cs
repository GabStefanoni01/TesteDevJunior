using MinhaApiComSQLite.DTOs;
using MinhaApiComSQLite.Models;
using MinhaApiComSQLite.Repositories;

namespace MinhaApiComSQLite.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public ProdutoService(IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<PagedResultDto<ProdutoResponseDto>> GetPagedAsync(int pageNumber, int pageSize)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            pageSize = pageSize < 1 ? 10 : Math.Min(pageSize, 100);

            var totalItems = await _produtoRepository.CountAsync();
            var produtos = await _produtoRepository.GetPagedAsync(pageNumber, pageSize);
            var items = produtos.Select(MapToResponse).ToList();

            foreach (var produto in items)
            {
                if (produto.Nome.Contains("PROMOCAO", StringComparison.OrdinalIgnoreCase) ||
                    produto.Nome.Contains("PROMOÇÃO", StringComparison.OrdinalIgnoreCase))
                {
                    produto.Nome = $"[Em Promocao] {produto.Nome}";
                }
            }

            return new PagedResultDto<ProdutoResponseDto>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                Items = items
            };
        }

        public async Task<ProdutoResponseDto?> GetByIdAsync(int id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);
            return produto is null ? null : MapToResponse(produto);
        }

        public async Task<(ProdutoResponseDto? Produto, string? Error)> CreateAsync(ProdutoCreateDto dto)
        {
            var normalizedName = NormalizeName(dto.Nome);

            if (string.IsNullOrWhiteSpace(normalizedName))
            {
                return (null, "O nome do produto e obrigatorio.");
            }

            if (dto.Preco <= 0)
            {
                return (null, "O preco deve ser maior que zero.");
            }

            if (await _categoriaRepository.GetByIdAsync(dto.CategoriaId) is null)
            {
                return (null, "Categoria nao encontrada.");
            }

            if (await _produtoRepository.GetByNomeAsync(normalizedName) is not null)
            {
                return (null, "Ja existe um produto cadastrado com este nome.");
            }

            var produto = new Produto
            {
                Nome = normalizedName,
                Preco = dto.Preco,
                CategoriaId = dto.CategoriaId
            };

            await _produtoRepository.AddAsync(produto);
            await _produtoRepository.SaveChangesAsync();

            produto = await _produtoRepository.GetByIdAsync(produto.Id) ?? produto;
            return (MapToResponse(produto), null);
        }

        public async Task<(ProdutoResponseDto? Produto, string? Error)> UpdateAsync(int id, ProdutoUpdateDto dto)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);
            if (produto is null)
            {
                return (null, "Produto nao encontrado.");
            }

            var normalizedName = NormalizeName(dto.Nome);
            if (string.IsNullOrWhiteSpace(normalizedName))
            {
                return (null, "O nome do produto e obrigatorio.");
            }

            if (dto.Preco <= 0)
            {
                return (null, "O preco deve ser maior que zero.");
            }

            if (await _categoriaRepository.GetByIdAsync(dto.CategoriaId) is null)
            {
                return (null, "Categoria nao encontrada.");
            }

            var existing = await _produtoRepository.GetByNomeAsync(normalizedName);
            if (existing is not null && existing.Id != id)
            {
                return (null, "Ja existe um produto cadastrado com este nome.");
            }

            produto.Nome = normalizedName;
            produto.Preco = dto.Preco;
            produto.CategoriaId = dto.CategoriaId;

            _produtoRepository.Update(produto);
            await _produtoRepository.SaveChangesAsync();

            produto = await _produtoRepository.GetByIdAsync(produto.Id) ?? produto;
            return (MapToResponse(produto), null);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var produto = await _produtoRepository.GetByIdAsync(id);
            if (produto is null)
            {
                return false;
            }

            _produtoRepository.Delete(produto);
            await _produtoRepository.SaveChangesAsync();
            return true;
        }

        public async Task<(DescontoResponseDto? Desconto, string? Error)> CalcularDescontoAsync(int id, DescontoRequestDto dto)
        {
            if (dto.Quantidade <= 0)
            {
                return (null, "A quantidade deve ser maior que zero.");
            }

            var produto = await _produtoRepository.GetByIdAsync(id);
            if (produto is null)
            {
                return (null, "Produto nao encontrado.");
            }

            var percentual = dto.Quantidade > 20 ? 0.15m
                : dto.Quantidade > 10 ? 0.10m
                : dto.Quantidade > 5 ? 0.05m
                : 0m;

            var subtotal = produto.Preco * dto.Quantidade;
            var valorDesconto = subtotal * percentual;

            return (new DescontoResponseDto
            {
                ProdutoId = produto.Id,
                ProdutoNome = produto.Nome,
                Quantidade = dto.Quantidade,
                PrecoUnitario = produto.Preco,
                PercentualDesconto = percentual * 100,
                ValorDesconto = valorDesconto,
                ValorTotal = subtotal - valorDesconto
            }, null);
        }

        public async Task<RelatorioEstatisticasDto> GetRelatorioAsync()
        {
            var produtos = await _produtoRepository.GetAllAsync();
            return new RelatorioEstatisticasDto
            {
                TotalProdutosCadastrados = produtos.Count,
                MediaPrecos = produtos.Count == 0 ? 0 : produtos.Average(p => p.Preco),
                ValorTotalProdutos = produtos.Sum(p => p.Preco)
            };
        }

        private static ProdutoResponseDto MapToResponse(Produto produto)
        {
            return new ProdutoResponseDto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco,
                CategoriaId = produto.CategoriaId,
                CategoriaNome = produto.Categoria?.Nome ?? string.Empty
            };
        }

        private static string NormalizeName(string name)
        {
            var trimmed = name.Trim();
            return trimmed.ToUpperInvariant();
        }
    }
}
