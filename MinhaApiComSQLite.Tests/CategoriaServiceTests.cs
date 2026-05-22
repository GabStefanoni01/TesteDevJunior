using MinhaApiComSQLite.DTOs;

namespace MinhaApiComSQLite.Tests
{
    public class CategoriaServiceTests
    {
        [Fact]
        public async Task CreateAsync_DeveSalvarNomeEmMaiusculas()
        {
            using var fixture = new ServiceTestFixture();

            var result = await fixture.CategoriaService.CreateAsync(new CategoriaCreateDto
            {
                Nome = "eletronicos"
            });

            Assert.Null(result.Error);
            Assert.NotNull(result.Categoria);
            Assert.Equal("ELETRONICOS", result.Categoria!.Nome);
        }

        [Fact]
        public async Task CreateAsync_NaoDevePermitirNomeDuplicado()
        {
            using var fixture = new ServiceTestFixture();
            var dto = new CategoriaCreateDto { Nome = "Eletronicos" };

            await fixture.CategoriaService.CreateAsync(dto);
            var result = await fixture.CategoriaService.CreateAsync(dto);

            Assert.Null(result.Categoria);
            Assert.Equal("Ja existe uma categoria cadastrada com este nome.", result.Error);
        }

        [Fact]
        public async Task DeleteAsync_NaoDeveExcluirCategoriaComProdutos()
        {
            using var fixture = new ServiceTestFixture();
            var categoria = await fixture.CategoriaService.CreateAsync(new CategoriaCreateDto { Nome = "Mercado" });
            await fixture.ProdutoService.CreateAsync(new ProdutoCreateDto
            {
                Nome = "Arroz",
                Preco = 25,
                CategoriaId = categoria.Categoria!.Id
            });

            var result = await fixture.CategoriaService.DeleteAsync(categoria.Categoria.Id);

            Assert.False(result.Success);
            Assert.Equal("Nao e possivel excluir uma categoria que possui produtos.", result.Error);
        }
    }
}
