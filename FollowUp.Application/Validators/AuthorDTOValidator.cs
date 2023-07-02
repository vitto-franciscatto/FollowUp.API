using FluentValidation;
using FollowUp.Application.DTOs;

namespace FollowUp.Application.Validators
{
    internal class AuthorDTOValidator : AbstractValidator<AuthorDTO>
    {
        public AuthorDTOValidator()
        {
            RuleFor(author => author.Id)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0)
                    .WithMessage("O Id: {PropertyValue} do autor é inválido");
            
            When(author => !string.IsNullOrEmpty(author.Extension), () =>
            {
                RuleFor(author => author.Extension)
                    .Cascade(CascadeMode.Stop)
                    .Must(ext => ext.Length >= 1)
                        .WithMessage("O Ramal do autor deve ter no mínimo 1 caracter")
                    .Must(ext => ext.Length <= 50)
                        .WithMessage("O Ramal do autor deve ter no máximo 50 caracteres");
            });
        }
    }
}
