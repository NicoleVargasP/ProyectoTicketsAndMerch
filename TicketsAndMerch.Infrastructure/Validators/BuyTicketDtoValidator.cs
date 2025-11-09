using FluentValidation;
using TicketsAndMerch.Infrastructure.DTOs;

namespace TicketsAndMerch.Infrastructure.Validators
{
    public class BuyTicketDtoValidator : AbstractValidator<BuyTicketDto>
    {
        public BuyTicketDtoValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("El Id del usuario es obligatorio y debe ser mayor que cero.");

            RuleFor(x => x.ConcertId)
                .GreaterThan(0).WithMessage("El Id del concierto es obligatorio y debe ser mayor que cero.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Debe comprar al menos un ticket.")
                .LessThanOrEqualTo(10).WithMessage("No puede comprar más de 10 tickets por orden.");

            // Ya no se usa PaymentMethod, se calcula TotalAmount en el servicio
            RuleFor(x => x.TotalAmount)
                .GreaterThan(0).WithMessage("El monto total debe ser mayor que cero.");
        }
    }
}
