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


        //public async Task UpdateAsync(User user)
        //{
        //    if (user == null) throw new ArgumentNullException(nameof(user));
        //    await _context.SaveChangesAsync();


        public async Task UpdateAsync(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            try
            {
                foreach (var order in user.Orders)
                {
                    var q = _context.Entry(order).State;
                    if (_context.Entry(order).State == EntityState.Modified ||
                            _context.Entry(order).State == EntityState.Detached)
                    {
                        _context.Orders.Add(order); // Add the order to the context if it's not already tracked
                    }
                }                //_context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var clientValues = entry.Entity;
                var databaseEntry = entry.GetDatabaseValues();

                if (databaseEntry == null)
                {
                    throw new Exception("The entity was deleted by another user.");
                }
                else
                {
                    var databaseValues = (User)databaseEntry.ToObject();
                    // You could retry the update or inform the user about the conflict.
                    throw new Exception("The entity was updated by another user.");
                }
            }

        }
    }
}


