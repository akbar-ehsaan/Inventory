using FluentValidation;
using inventory.domain.Contracts;
using inventory.domain.Core;
using inventory.infrastructure.Repositories;
using MediatR;
using System.Net;

namespace inventory.api.Application.Commands;

public sealed record AddProductCommand(Product product):IRequest<bool>
{
    public sealed class AddProductValidator : AbstractValidator<AddProductCommand>
    {
        public AddProductValidator(IProductRepository productRepository)
        {

            RuleFor(x => x.product.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(40).WithMessage("Title must be less than 40 characters.");

            // Rule: Title must be unique
            RuleFor(x => x.product.Title)
            .MustAsync(async (title, cancellation) =>
                    !(await productRepository.IsTitleTakenAsync(title)))
                .WithMessage("A product with this title already exists.");
        }
    }
    public sealed class Handler(IProductRepository productRepository) : IRequestHandler<AddProductCommand, bool>
    {
        public async Task<bool> Handle(AddProductCommand request, CancellationToken cancellationToken) 
            => await productRepository.AddAsync(request.product);
    }
}
