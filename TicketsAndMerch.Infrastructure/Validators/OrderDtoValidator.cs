using FluentValidation;
using TicketsAndMerch.Infrastructure.DTOs;

namespace TicketsAndMerch.Infrastructure.Validators
{
    public class OrderDtoValidator : AbstractValidator<OrderDto>
    {
        public OrderDtoValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .WithMessage("El Id del usuario debe ser mayor que 0.");

            RuleFor(x => x.DateOrder)
                .NotEmpty().WithMessage("La fecha de la orden es requerida.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha no puede ser futura.");

            RuleFor(x => x.State)
                .NotEmpty().WithMessage("El estado de la orden es obligatorio.")
                .MaximumLength(50).WithMessage("El estado no puede exceder los 50 caracteres.");

            RuleFor(x => x.OrderAmount)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor que 0.");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("El precio unitario debe ser mayor que 0.");

            RuleFor(x => x.OrderDetail)
                .MaximumLength(500)
                .WithMessage("El detalle de la orden no puede exceder los 500 caracteres.");
        }
    }
}
