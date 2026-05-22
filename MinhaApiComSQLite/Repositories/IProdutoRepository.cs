using MinhaApiComSQLite.Models;

namespace MinhaApiComSQLite.Repositories
{
    public interface IProdutoRepository
    {
        Task<int> CountAsync();
        Task<List<Produto>> GetPagedAsync(int pageNumber, int pageSize);
        Task<List<Produto>> GetAllAsync();
        Task<Produto?> GetByIdAsync(int id);
        Task<Produto?> GetByNomeAsync(string nome);
        Task AddAsync(Produto produto);
        void Update(Produto produto);
        void Delete(Produto produto);
        Task SaveChangesAsync();
    }
}
