using FluentValidation;
using inventory.api.Application.Commands;
using inventory.domain.Contracts;
using inventory.domain.Core;
using inventory.infrastructure.Cache;
using inventory.infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace inventory.api.Application.Queries;

public sealed record class GetProductByIdQuery(string ProductId) : IRequest<Product>
{
    public sealed class GetProductByIdValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdValidator() 
            => RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required.");
    }
    public sealed class Handler(IProductRepository productRepository,ICacheService cacheService) : IRequestHandler<GetProductByIdQuery, Product>
    {
        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            string cacheKey = $"Product_{request.ProductId}";

            return await cacheService.GetOrCreateAsync(
                cacheKey,
                async () => await productRepository.GetByIdAsync(Guid.Parse(request.ProductId)),
                TimeSpan.FromMinutes(5));
        }
    }
}
