using FluentValidation;
using inventory.domain.Contracts;
using MediatR;

namespace inventory.api.Application.Commands
{
    public sealed record IncreaseInventoryCommand(string ProductId, int Amount) : IRequest
    {
        public sealed class IncreaseInventoryValidator : AbstractValidator<IncreaseInventoryCommand>
        {
            public IncreaseInventoryValidator()
            {
                RuleFor(x => x.ProductId)
                    .NotEmpty().WithMessage("ProductId is required.");
                RuleFor(x => x.Amount)
                   .NotEmpty().WithMessage("Amount is required.");
            }
        }
        public sealed class Hander(IProductRepository productRepository) : IRequestHandler<IncreaseInventoryCommand>
        {
            public async Task Handle(IncreaseInventoryCommand request, CancellationToken cancellationToken)
            {
                var product = await productRepository.GetByIdAsync(Guid.Parse(request.ProductId));
                if (product is not null)
                {
                    product.IncreaseInventory(request.Amount);
                    await productRepository.UpdateAsync(product);
                }
                else
                    throw new Exception("Product doesn't exist");

            }
        }
    }
}
