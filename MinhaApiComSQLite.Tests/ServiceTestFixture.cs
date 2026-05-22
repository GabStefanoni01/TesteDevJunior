using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MinhaApiComSQLite.Data;
using MinhaApiComSQLite.Repositories;
using MinhaApiComSQLite.Services;

namespace MinhaApiComSQLite.Tests
{
    internal sealed class ServiceTestFixture : IDisposable
    {
        private readonly SqliteConnection _connection;

        public AppDbContext Context { get; }
        public ProdutoService ProdutoService { get; }
        public CategoriaService CategoriaService { get; }

        public ServiceTestFixture()
        {
            _connection = new SqliteConnection("Data Source=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(_connection)
                .Options;

            Context = new AppDbContext(options);
            Context.Database.EnsureCreated();

            var produtoRepository = new ProdutoRepository(Context);
            var categoriaRepository = new CategoriaRepository(Context);

            ProdutoService = new ProdutoService(produtoRepository, categoriaRepository);
            CategoriaService = new CategoriaService(categoriaRepository);
        }

        public void Dispose()
        {
            Context.Dispose();
            _connection.Dispose();
        }
    }
}
