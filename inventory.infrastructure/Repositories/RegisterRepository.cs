using inventory.domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace inventory.infrastructure.Repositories
{

    public static class RegisterRepository
        {
            public static IServiceCollection AddInventoryRepository(this IServiceCollection services, IConfiguration configuration)
            {
                // Register the DbContext with the connection string
                services.AddDbContext<InventoryContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

                // Register the repository
                services.AddScoped<IProductRepository, ProductRepository>();

                return services;
            }
        }
    
}
