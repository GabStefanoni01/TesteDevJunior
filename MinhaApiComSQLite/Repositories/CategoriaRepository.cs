using Microsoft.EntityFrameworkCore;
using MinhaApiComSQLite.Data;
using MinhaApiComSQLite.Models;

namespace MinhaApiComSQLite.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<Categoria>> GetAllAsync()
        {
            return _context.Categorias
                .OrderBy(c => c.Nome)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<Categoria?> GetByIdAsync(int id)
        {
            return _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<Categoria?> GetByNomeAsync(string nome)
        {
            return _context.Categorias.FirstOrDefaultAsync(c => c.Nome == nome);
        }

        public Task AddAsync(Categoria categoria)
        {
            return _context.Categorias.AddAsync(categoria).AsTask();
        }

        public void Update(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
        }

        public void Delete(Categoria categoria)
        {
            _context.Categorias.Remove(categoria);
        }

        public Task<bool> HasProdutosAsync(int categoriaId)
        {
            return _context.Produtos.AnyAsync(p => p.CategoriaId == categoriaId);
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
