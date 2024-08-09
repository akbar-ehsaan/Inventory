using FluentValidation;
using inventory.domain.Contracts;
using inventory.domain.Core;
using inventory.infrastructure.Repositories;
using MediatR;
using System.Net;

namespace inventory.api.Application.Commands;

public sealed record AddProductCommand(Product product) : IRequest
{
    public sealed class AddProductValidator : AbstractValidator<AddProductCommand>
    {
        public AddProductValidator(IProductRepository productRepository)
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
    public sealed class Handler(IProductRepository productRepository,ICacheService cacheService) : IRequestHandler<AddProductCommand>
    {
        public async Task Handle(AddProductCommand request, CancellationToken cancellationToken) =>
            await productRepository.AddAsync(request.product);
    }
}
