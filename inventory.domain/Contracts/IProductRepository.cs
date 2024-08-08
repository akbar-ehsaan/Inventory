using inventory.domain.Core;

namespace inventory.domain.Contracts
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(Guid id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
    }
}
