using FluentValidation;
using inventory.domain.Contracts;
using MediatR;

namespace inventory.api.Application.Commands
{
    public sealed record IncreaseInventoryCommand(string ProductId, int Amount) : IRequest<bool>
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
        public sealed class Hander(IProductRepository productRepository) : IRequestHandler<IncreaseInventoryCommand, bool>
        {
            public async Task<bool> Handle(IncreaseInventoryCommand request, CancellationToken cancellationToken)
            {
                var product = await productRepository.GetByIdAsync(Guid.Parse(request.ProductId));
                if (product is not null)
                {
                    product.IncreaseInventory(request.Amount);
                    return await productRepository.UpdateAsync(product);
                }
                throw new Exception("Product doesn't exist");

            }
        }
    }
}
