using FluentValidation;
using TicketsAndMerch.Core.QueryFilters;

namespace TicketsAndMerch.Infrastructure.Validators
{
    public class UserOrderQueryFilterValidator : AbstractValidator<UserOrderQueryFilter>
    {
        public UserOrderQueryFilterValidator()
        {
            RuleFor(x => x.OrderType)
                .Must(x => x == null || x == "Ticket" || x == "Merch")
                .WithMessage("El tipo de orden debe ser 'Ticket' o 'Merch'");

            RuleFor(x => x.Status)
                .MaximumLength(50).WithMessage("El estado no puede superar los 50 caracteres");
        }
    }
}
