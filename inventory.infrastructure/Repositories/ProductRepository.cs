using inventory.domain.Contracts;
using inventory.domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory.infrastructure.Repositories
{

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
    }
}
