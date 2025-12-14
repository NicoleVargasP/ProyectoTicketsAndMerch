using FluentValidation;
using TicketsAndMerch.Core.QueryFilters;

namespace TicketsAndMerch.Infrastructure.Validators
{
    public class AvailableConcertQueryFilterValidator : AbstractValidator<AvailableConcertQueryFilter>
    {
        public AvailableConcertQueryFilterValidator()
        {
            RuleFor(x => x.Title)
                .MaximumLength(200)
                .WithMessage("El título no puede exceder 200 caracteres.");

            RuleFor(x => x.Location)
                .MaximumLength(200)
                .WithMessage("La ubicación no puede exceder 200 caracteres.");
        }
    }
}
