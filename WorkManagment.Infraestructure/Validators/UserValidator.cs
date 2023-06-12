using FluentValidation;
using WorkManagment.Core.Entities;

namespace WorkManagment.Infraestructure.Validators
{
    public class UserValidator : AbstractValidator<Usuario>
    {
        public UserValidator()
        {
            RuleFor(usuario => usuario.Dni.ToString())
                .NotEmpty().WithMessage("El número de dni es obligatorio.")
                .Must(dni => int.TryParse(dni.ToString(), out _)).WithMessage("El número de dni debe ser numérico.")
                .Length(8).WithMessage("El número de dni debe tener 8 dígitos.");

        }
    }
}
