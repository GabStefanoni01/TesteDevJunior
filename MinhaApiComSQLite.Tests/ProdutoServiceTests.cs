using MinhaApiComSQLite.DTOs;

namespace MinhaApiComSQLite.Tests
{
    public class ProdutoServiceTests
    {
        [Fact]
        public async Task CreateAsync_DeveSalvarProdutoEmMaiusculas()
        {
            using var fixture = new ServiceTestFixture();
            var categoriaId = await CriarCategoriaAsync(fixture);

            var result = await fixture.ProdutoService.CreateAsync(new ProdutoCreateDto
            {
                Nome = "produto exemplo",
                Preco = 50,
                CategoriaId = categoriaId
            });

            Assert.Null(result.Error);
            Assert.NotNull(result.Produto);
            Assert.Equal("PRODUTO EXEMPLO", result.Produto!.Nome);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public async Task CreateAsync_NaoDevePermitirPrecoMenorOuIgualAZero(decimal preco)
        {
            using var fixture = new ServiceTestFixture();
            var categoriaId = await CriarCategoriaAsync(fixture);

            var result = await fixture.ProdutoService.CreateAsync(new ProdutoCreateDto
            {
                Nome = "Produto invalido",
                Preco = preco,
                CategoriaId = categoriaId
            });

            Assert.Null(result.Produto);
            Assert.Equal("O preco deve ser maior que zero.", result.Error);
        }

        [Fact]
        public async Task CreateAsync_NaoDevePermitirCategoriaInexistente()
        {
            using var fixture = new ServiceTestFixture();

            var result = await fixture.ProdutoService.CreateAsync(new ProdutoCreateDto
            {
                Nome = "Produto sem categoria",
                Preco = 20,
                CategoriaId = 999
            });

            Assert.Null(result.Produto);
            Assert.Equal("Categoria nao encontrada.", result.Error);
        }

        [Fact]
        public async Task GetPagedAsync_DeveOrdenarPorPrecoEPaginar()
        {
            using var fixture = new ServiceTestFixture();
            var categoriaId = await CriarCategoriaAsync(fixture);

            await fixture.ProdutoService.CreateAsync(new ProdutoCreateDto { Nome = "Produto caro", Preco = 100, CategoriaId = categoriaId });
            await fixture.ProdutoService.CreateAsync(new ProdutoCreateDto { Nome = "Produto barato", Preco = 10, CategoriaId = categoriaId });
            await fixture.ProdutoService.CreateAsync(new ProdutoCreateDto { Nome = "Produto medio", Preco = 50, CategoriaId = categoriaId });

            var result = await fixture.ProdutoService.GetPagedAsync(pageNumber: 1, pageSize: 2);
            var produtos = result.Items.ToList();

            Assert.Equal(3, result.TotalItems);
            Assert.Equal(2, result.TotalPages);
            Assert.Equal(2, produtos.Count);
            Assert.Equal("PRODUTO BARATO", produtos[0].Nome);
            Assert.Equal("PRODUTO MEDIO", produtos[1].Nome);
        }

        [Theory]
        [InlineData(6, 5)]
        [InlineData(11, 10)]
        [InlineData(21, 15)]
        public async Task CalcularDescontoAsync_DeveAplicarDescontoProgressivo(int quantidade, decimal percentualEsperado)
        {
            using var fixture = new ServiceTestFixture();
            var categoriaId = await CriarCategoriaAsync(fixture);
            var produto = await fixture.ProdutoService.CreateAsync(new ProdutoCreateDto
            {
                Nome = $"Produto desconto {quantidade}",
                Preco = 100,
                CategoriaId = categoriaId
            });

            var result = await fixture.ProdutoService.CalcularDescontoAsync(produto.Produto!.Id, new DescontoRequestDto
            {
                Quantidade = quantidade
            });

            Assert.Null(result.Error);
            Assert.NotNull(result.Desconto);
            Assert.Equal(percentualEsperado, result.Desconto!.PercentualDesconto);
        }

        [Fact]
        public async Task GetRelatorioAsync_DeveRetornarEstatisticasDosProdutos()
        {
            using var fixture = new ServiceTestFixture();
            var categoriaId = await CriarCategoriaAsync(fixture);

            await fixture.ProdutoService.CreateAsync(new ProdutoCreateDto { Nome = "Produto um", Preco = 10, CategoriaId = categoriaId });
            await fixture.ProdutoService.CreateAsync(new ProdutoCreateDto { Nome = "Produto dois", Preco = 30, CategoriaId = categoriaId });

            var result = await fixture.ProdutoService.GetRelatorioAsync();

            Assert.Equal(2, result.TotalProdutosCadastrados);
            Assert.Equal(20, result.MediaPrecos);
            Assert.Equal(40, result.ValorTotalProdutos);
        }

        private static async Task<int> CriarCategoriaAsync(ServiceTestFixture fixture)
        {
            var result = await fixture.CategoriaService.CreateAsync(new CategoriaCreateDto
            {
                Nome = Guid.NewGuid().ToString("N")
            });

            return result.Categoria!.Id;
        }
    }
}
