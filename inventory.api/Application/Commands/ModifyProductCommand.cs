using FluentValidation;
using inventory.api.Dtoes;
using inventory.domain.Contracts;
using inventory.domain.Core;
using MediatR;

namespace inventory.api.Application.Commands;

public sealed record ModifyProductCommand(ModifyProductDto product) : IRequest
{
    public sealed class ModifyProductValidator : AbstractValidator<ModifyProductCommand>
    {
        public ModifyProductValidator(IProductRepository productRepository)
        {

            RuleFor(x => x.product.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(40).WithMessage("Title must be less than 40 characters.");

            RuleFor(x => x.product.Title)
            .MustAsync(async (title, cancellation) =>
                    !(await productRepository.IsTitleTakenAsync(title)))
                .WithMessage("A product with this title already exists.");

            RuleFor(x => x.product.Price)
                      .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(x => x.product.Discount)
                      .InclusiveBetween(0, 100).WithMessage("Discount must be between 0 and 100.");
        }
    }
    public sealed class Handler(IProductRepository productRepository,ICacheService cacheService) : IRequestHandler<ModifyProductCommand>
    {
        public async Task Handle(ModifyProductCommand request, CancellationToken cancellationToken)
        {
            var productRes = await productRepository.GetByIdAsync(Guid.Parse(request.product.Id));

            productRes.UpdateTitle(request.product.Title);

            productRes.UpdatePrice(request.product.Price);

            productRes.UpdateDiscount(request.product.Discount);

            await productRepository.UpdateAsync(productRes);

            cacheService.Remove($"Product_{productRes.Id}");

        }
    }
}
