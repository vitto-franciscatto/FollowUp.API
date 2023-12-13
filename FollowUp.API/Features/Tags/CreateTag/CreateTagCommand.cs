using FluentValidation;
using LanguageExt.Common;
using MediatR;

namespace FollowUp.API.Features.Tags.CreateTag
{
    public class CreateTagCommand : IRequest<Result<TagDTO>>
    {
        public string Name { get; set; } = string.Empty;
    }
    
    public class CreateTagCommandValidator 
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
    
    public static class CreateTagCommandMapper
    {
        public static Tag MapToTag(
            this CreateTagCommand command)
        {
            return Tag.Create(
                0, 
                command.Name);
        }
    }
}
