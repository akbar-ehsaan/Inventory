using inventory.domain.Core;

namespace inventory.domain.Contracts
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(Guid id);
        Task<bool> AddAsync(Product product);
        Task<bool> UpdateAsync(Product product);
        Task<bool> IsTitleTakenAsync(string title);
    }
}
