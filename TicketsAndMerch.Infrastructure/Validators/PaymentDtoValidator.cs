using FluentValidation;
using TicketsAndMerch.Core.Entities;
using TicketsAndMerch.Infrastructure.DTOs;

namespace TicketsAndMerch.Infrastructure.Validators
{
    public class PaymentDtoValidator : AbstractValidator<PaymentDto>
    {
        public PaymentDtoValidator()
        {
            RuleFor(x => x.Method)
                .NotEmpty().WithMessage("El método de pago es obligatorio.")
                .MaximumLength(50).WithMessage("El método de pago no puede exceder los 50 caracteres.");

            RuleFor(x => x.PaymentDate)
                .NotEmpty().WithMessage("La fecha de pago es requerida.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de pago no puede ser futura.");

            RuleFor(x => x.OrderAmount)
                .GreaterThan(0).WithMessage("El monto debe ser mayor que 0.");

            RuleFor(x => x.PaymentState)
                .NotEmpty().WithMessage("El estado del pago es obligatorio.")
                .MaximumLength(50).WithMessage("El estado no puede exceder los 50 caracteres.");
        }
    }
}
