using FluentValidation;
using TicketsAndMerch.Infrastructure.DTOs;

namespace TicketsAndMerch.Infrastructure.Validators
{
    public class MerchDtoValidator : AbstractValidator<MerchDto>
    {
        public MerchDtoValidator()
        {
            RuleFor(x => x.MerchName)
                .NotEmpty().WithMessage("El nombre del producto es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("La descripción no puede exceder los 500 caracteres.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("El precio debe ser mayor que 0.");

            RuleFor(x => x.TypeMerch)
                .NotEmpty().WithMessage("El tipo de producto es obligatorio.")
                .MaximumLength(50).WithMessage("El tipo de producto no puede exceder los 50 caracteres.");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("El stock no puede ser negativo.");
        }
    }
}
