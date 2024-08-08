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

        public async Task<bool> AddAsync(Product product)
        {
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            { return false; }
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            try
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            { return false; }
        }

        public async Task<bool> IsTitleTakenAsync(string title) 
            => _context.Products.Where(i => i.Title == title).Any() ? false : true;
    }
}
