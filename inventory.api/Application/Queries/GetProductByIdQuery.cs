using FluentValidation;
using inventory.api.Application.Commands;
using inventory.domain.Contracts;
using inventory.domain.Core;
using MediatR;

namespace inventory.api.Application.Queries;

public sealed record class GetProductByIdQuery(string ProductId) : IRequest<Product>
{
    public sealed class GetProductByIdValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdValidator() 
            => RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required.");
    }
    public sealed class Handler(IProductRepository productRepository) : IRequestHandler<GetProductByIdQuery, Product>
    {
        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken) 
            => await productRepository.GetByIdAsync(Guid.Parse(request.ProductId));
    }
}
