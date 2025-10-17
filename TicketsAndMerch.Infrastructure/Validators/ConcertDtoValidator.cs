using FluentValidation;
using TicketsAndMerch.Core.Entities;

namespace TicketsAndMerch.Infrastructure.Validators
{
    public class ConcertValidator : AbstractValidator<Concert>
    {
        public ConcertValidator()
        {
            // Validar título del concierto
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("El título del concierto es obligatorio.")
                .MaximumLength(100).WithMessage("El título no puede tener más de 100 caracteres.");

            // Validar descripción (opcional)
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("La descripción no puede superar los 500 caracteres.")
                .When(x => !string.IsNullOrEmpty(x.Description));

            // Validar ubicación
            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("La ubicación del concierto es obligatoria.")
                .MaximumLength(200).WithMessage("La ubicación no puede tener más de 200 caracteres.");

            // Validar fecha del concierto
            RuleFor(x => x.Date)
                .Must(BeValidDate).WithMessage("La fecha del concierto debe ser una fecha válida.")
                .Must(BeFutureDate).WithMessage("La fecha del concierto debe ser futura (no puede ser anterior a hoy).");

            // Validar cantidad de entradas disponibles
            RuleFor(x => x.AvailableTickets)
                .GreaterThanOrEqualTo(0).WithMessage("El número de entradas disponibles no puede ser negativo.");
        }

        // Método auxiliar: la fecha debe ser válida (no vacía ni default)
        private bool BeValidDate(DateOnly date)
        {
            return date != default(DateOnly);
        }

        // Método auxiliar: la fecha debe ser igual o posterior a hoy
        private bool BeFutureDate(DateOnly date)
        {
            return date >= DateOnly.FromDateTime(DateTime.Today);
        }
    }
}
