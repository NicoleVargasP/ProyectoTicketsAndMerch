using FluentValidation;
using TicketsAndMerch.Infrastructure.DTOs;

namespace TicketsAndMerch.Infrastructure.Validators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("El nombre de usuario es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre de usuario no puede exceder los 100 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
                .EmailAddress().WithMessage("El formato del correo electrónico no es válido.")
                .MaximumLength(150).WithMessage("El correo no puede exceder los 150 caracteres.")
                .MinimumLength(10).WithMessage("El correo electrónico debe tener al menos 10 caracteres.")
                .Must(email => email.Contains("@"))
                .WithMessage("El correo electrónico debe contener '@'.");

            RuleFor(x => x.Contrasenia)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.")
                .MaximumLength(100).WithMessage("La contraseña no puede exceder los 100 caracteres.");

            RuleFor(x => x.Rol)
                .NotEmpty().WithMessage("El rol es obligatorio.")
                .MaximumLength(50).WithMessage("El rol no puede exceder los 50 caracteres.");
        }
    }
}
