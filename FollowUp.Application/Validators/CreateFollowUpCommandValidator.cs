using FluentValidation;
using FollowUp.Application.Commands.CreateFollowUp;
using FollowUp.Application.DTOs;
using FollowUp.Application.Interfaces;
using FollowUp.Domain;

namespace FollowUp.Application.Validators
{
    internal class CreateFollowUpCommandValidator 
        : AbstractValidator<CreateFollowUpCommand>
    {
        private readonly ITagRepository _tagRepository;
        public CreateFollowUpCommandValidator(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;

            RuleFor(_ => _.AssistanceId)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0)
                .WithMessage("O AssistanceId: {PropertyValue} é inválido");

            When(command => command.Author is not null, () =>
            {
                RuleFor(_ => _.Author!)
                    .Cascade(CascadeMode.Stop)
                    .SetValidator(new AuthorDTOValidator());
            });

            When(command => command.Contact is not null, () =>
            {
                RuleFor(_ => _.Contact!)
                    .Cascade(CascadeMode.Stop)
                    .SetValidator(new ContactDTOValidator());
            });

            RuleFor(_ => _.Message)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("A Mensagem não pode ser nula")
                .NotEmpty().WithMessage("A mensagem não pode ser vazia")
                .Must(message => message.Length >= 1)
                    .WithMessage("A mensagem deve ter no mínimo 1 caracter")
                .Must(message => message.Length <= 8000)
                    .WithMessage("A mensagem deve ter no máximo 8000 caracteres");

            //RuleFor(_ => _.CreatedAt)
            //    .Cascade(CascadeMode.Stop);

            //RuleFor(_ => _.OccuredAt)
            //    .Cascade(CascadeMode.Stop);


            RuleForEach(_ => _.TagIds)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithMessage("O TagId: {PropertyValue} é inválido")
                .MustAsync(async (tagId, cancellationToken) => 
                    {
                        Tag? tag = await _tagRepository.Get(tagId);

                        return tag is not null;
                    })
                .WithMessage("O TagId: {PropertyValue} não está cadastrado");
        }
    }
}
