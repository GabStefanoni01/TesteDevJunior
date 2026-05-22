using MinhaApiComSQLite.Models;

namespace MinhaApiComSQLite.Repositories
{
    public interface ICategoriaRepository
    {
        Task<List<Categoria>> GetAllAsync();
        Task<Categoria?> GetByIdAsync(int id);
        Task<Categoria?> GetByNomeAsync(string nome);
        Task AddAsync(Categoria categoria);
        void Update(Categoria categoria);
        void Delete(Categoria categoria);
        Task<bool> HasProdutosAsync(int categoriaId);
        Task SaveChangesAsync();
    }
}
