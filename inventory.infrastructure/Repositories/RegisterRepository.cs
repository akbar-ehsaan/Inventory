using inventory.domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace inventory.infrastructure.Repositories;


public static class RegisterRepository
{
    public static IServiceCollection AddInventoryRepository(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<InventoryContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}

