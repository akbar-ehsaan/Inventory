using FluentValidation;
using inventory.api.Dtoes;
using inventory.domain.Contracts;
using MediatR;

namespace inventory.api.Application.Commands;

public sealed record AddOrderCommand(AddOrderDto Order) : IRequest
{

    public sealed class AddOrderValidator : AbstractValidator<AddOrderCommand>
    {
        public AddOrderValidator()
        {

            RuleFor(x => x.Order.ProductId)
                .NotEmpty().WithMessage("ProductId is required.");

            RuleFor(x => x.Order.UserId)
              .NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.Order.Amount)
              .NotEmpty().WithMessage("Amount is required.");

        }
    }
    public sealed class Handler(IProductRepository productRepository
                                , IUserRepository userRepository
                                , ICacheService cacheService) : IRequestHandler<AddOrderCommand>
    {
        public async Task Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {

            var user = await userRepository.GetByIdAsync(Guid.Parse(request.Order.UserId));


            if (user == null) throw new Exception("User not found.");

            var product = await productRepository.GetByIdAsync(Guid.Parse(request.Order.ProductId));

            if (product == null) throw new Exception("Product not found.");

            user.PlaceOrder(product, request.Order.Amount);

            await userRepository.UpdateAsync(user);

            await productRepository.UpdateAsync(product);

            cacheService.Remove($"Product_{product.Id}");


        }
    }
}
