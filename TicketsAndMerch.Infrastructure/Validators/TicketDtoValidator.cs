using FluentValidation;
using TicketsAndMerch.Infrastructure.DTOs;

namespace TicketsAndMerch.Infrastructure.Validators
{
    public class TicketDtoValidator : AbstractValidator<TicketDto>
    {
        public TicketDtoValidator()
        {
            RuleFor(x => x.ConcertId)
                .GreaterThan(0)
                .WithMessage("El Id del concierto debe ser mayor que 0.");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("El precio del ticket debe ser mayor que 0.");

            RuleFor(x => x.TicketType)
                .NotEmpty().WithMessage("El tipo de ticket es obligatorio.")
                .MaximumLength(50).WithMessage("El tipo de ticket no puede exceder los 50 caracteres.");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El stock no puede ser negativo.");
        }
    }
}
