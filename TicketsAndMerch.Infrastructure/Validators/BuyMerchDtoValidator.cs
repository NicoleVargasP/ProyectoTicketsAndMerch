
using FluentValidation;
using TicketsAndMerch.Infrastructure.DTOs;

namespace TicketsAndMerch.Infrastructure.Validators
{
    public class BuyMerchDtoValidator : AbstractValidator<BuyMerchDto>
    {
        public BuyMerchDtoValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("El Id del usuario es obligatorio y debe ser mayor que cero.");

            RuleFor(x => x.MerchId)
                .GreaterThan(0).WithMessage("El Id del producto es obligatorio y debe ser mayor que cero.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Debe comprar al menos una unidad.")
                .LessThanOrEqualTo(100).WithMessage("No puede comprar más de 100 unidades por orden.");

            RuleFor(x => x.TotalAmount)
                .GreaterThan(0).WithMessage("El monto total debe ser mayor que cero.");
        }
    }
}
