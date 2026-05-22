using Microsoft.EntityFrameworkCore;
using MinhaApiComSQLite.Data;
using MinhaApiComSQLite.Models;

namespace MinhaApiComSQLite.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<int> CountAsync()
        {
            return _context.Produtos.CountAsync();
        }

        public Task<List<Produto>> GetPagedAsync(int pageNumber, int pageSize)
        {
            return _context.Produtos
                .Include(p => p.Categoria)
                .OrderBy(p => (double)p.Preco)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<List<Produto>> GetAllAsync()
        {
            return _context.Produtos
                .Include(p => p.Categoria)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<Produto?> GetByIdAsync(int id)
        {
            return _context.Produtos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task<Produto?> GetByNomeAsync(string nome)
        {
            return _context.Produtos.FirstOrDefaultAsync(p => p.Nome == nome);
        }

        public Task AddAsync(Produto produto)
        {
            return _context.Produtos.AddAsync(produto).AsTask();
        }

        public void Update(Produto produto)
        {
            _context.Produtos.Update(produto);
        }

        public void Delete(Produto produto)
        {
            _context.Produtos.Remove(produto);
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
