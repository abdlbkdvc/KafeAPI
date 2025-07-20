using FluentValidation;
using KafeAPI.Application.Dtos.OrderItemDtos;

namespace KafeAPI.Application.Validators.OrderItem
{
    public class AddOrderItemValidator : AbstractValidator<CreateOrderItemDto>
    {
        public AddOrderItemValidator()
        {
            RuleFor(x =>x.Quantity)
                .NotEmpty().WithMessage("Sipariş adeti boş olamaz.")
                .GreaterThan(0).WithMessage("Sipariş adeti 0'dan büyük olmalıdır.");
        }
    }
}
