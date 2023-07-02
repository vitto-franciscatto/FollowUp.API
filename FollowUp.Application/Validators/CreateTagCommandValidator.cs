using FluentValidation;
using FollowUp.Application.Commands.CreateTag;

namespace FollowUp.Application.Validators
{
    internal class CreateTagCommandValidator 
        : AbstractValidator<CreateTagCommand>
    {
        public CreateTagCommandValidator()
        {
            RuleFor(_ => _.Name)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("O Nome da tag não pode ser nulo")
                .NotEmpty().WithMessage("O Nome da tag não pode ser vazio")
                .Must(name => name.Length >= 1)
                    .WithMessage("O Nome da tag deve ter no mínimo 1 caracter")
                .Must(name => name.Length <= 255)
                    .WithMessage("O Nome da tag deve ter no máximo 255 caracteres");
        }
    }
}
