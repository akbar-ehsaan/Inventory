using inventory.domain.Contracts;
using inventory.domain.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory.infrastructure.Repositories
{

    public class UserRepository(InventoryContext context) : IUserRepository
    {
        private readonly InventoryContext _context = context;

        public async Task<User> GetByIdAsync(Guid id) =>
            await _context.Users
                    .Include(u => u.Orders)
                    .FirstOrDefaultAsync(u => u.Id == id);


        public async Task UpdateAsync(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

    }
}


