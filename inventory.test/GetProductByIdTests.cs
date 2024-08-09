using inventory.api.Application.Queries;
using inventory.domain.Contracts;
using inventory.domain.Core;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory.test;

public class GetProductByIdQueryTests
{
    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly Mock<ICacheService> _mockCacheService;
    private readonly GetProductByIdQuery.Handler _handler;

    public GetProductByIdQueryTests()
    {
        _mockProductRepository = new Mock<IProductRepository>();
        _mockCacheService = new Mock<ICacheService>();
        _handler = new GetProductByIdQuery.Handler(_mockProductRepository.Object, _mockCacheService.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnProduct_FromCache()
    {
        // Arrange
        var productId = Guid.NewGuid().ToString();
        var product = new Product("Test Product", 10, 100m, 10m);

        _mockCacheService.Setup(x => x.GetOrCreateAsync(
            It.IsAny<string>(),
            It.IsAny<Func<Task<Product>>>(),
            It.IsAny<TimeSpan>()
        )).ReturnsAsync(product);

        var query = new GetProductByIdQuery(productId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(product, result);
        _mockProductRepository.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Never);
    }
}
