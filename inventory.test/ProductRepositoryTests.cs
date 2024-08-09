using Moq;
using Xunit;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using inventory.domain.Core;
using inventory.infrastructure.Repositories;
using inventory.infrastructure;

namespace inventory.test;


public class ProductRepositoryTests
{
    private readonly InventoryContext _context;
    private readonly ProductRepository _repository;

    public ProductRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<InventoryContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new InventoryContext(options);
        _repository = new ProductRepository(_context);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnProduct_WhenProductExists()
    {
        //Arrange
        var product = new Product("Sample Product", 10, 100.00m, 10.00m);

        //Act
        _context.Products.Add(product);

        await _context.SaveChangesAsync();

        var result = await _repository.GetByIdAsync(product.Id);

        //Assert
        Assert.Equal(product.Id, result?.Id);
    }

    [Fact]
    public async Task AddAsync_ShouldAddProductSuccessfully()
    {
        //Arrange
        var product = new Product("Sample Product", 10, 100.00m, 10.00m);

        //Act
        await _repository.AddAsync(product);

        var result = await _context.Products.FindAsync(product.Id);

        //Assert
        Assert.NotNull(result);

        Assert.Equal(product.Title, result.Title);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateProductSuccessfully()
    {
        //Arrange
        var product = new Product("Sample Product", 10, 100.00m, 10.00m);
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        //Act
        product.UpdatePrice(120.00m);
        await _repository.UpdateAsync(product);
        var updatedProduct = await _context.Products.FindAsync(product.Id);

        //Assert
        Assert.Equal(120.00m, updatedProduct.Price);
    }

}
