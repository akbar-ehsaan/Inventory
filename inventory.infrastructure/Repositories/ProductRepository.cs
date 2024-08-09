using inventory.domain.Contracts;
using inventory.domain.Core;

namespace inventory.infrastructure.Repositories;


public class ProductRepository(InventoryContext context) : IProductRepository
{
    private readonly InventoryContext _context = context;

    public async Task<Product> GetByIdAsync(Guid id)
        => await _context.Products.FindAsync(id);

    public async Task AddAsync(Product product)
    {

        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsTitleTakenAsync(string title)
        => _context.Products.Where(i => i.Title == title).Any() ? false : true;
}
