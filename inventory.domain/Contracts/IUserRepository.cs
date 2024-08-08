using inventory.domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory.domain.Contracts
{
 
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid id);

        Task UpdateAsync(User user);
    }
}
