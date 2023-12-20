using FluentValidation;
using FollowUp.API.Features.Tags;
using LanguageExt.Common;
using MediatR;

namespace FollowUp.API.Features.FollowUps.CreateFollowUp
{
    public class CreateFollowUpCommand : IRequest<Result<FollowUp>>
    {
        public string IdentifierKey { get; set; } = string.Empty;
        public AuthorDTO? Author { get; set; }
        public ContactDTO? Contact { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.MinValue;
        public DateTime OccuredAt { get; set; } = DateTime.MinValue;
        public IEnumerable<int>? TagIds { get; set; }
    }
    
    public class CreateFollowUpCommandValidator 
        : AbstractValidator<CreateFollowUpCommand>
    {
        private readonly ITagRepository _tagRepository;
        public CreateFollowUpCommandValidator(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;

            RuleFor(cmd => cmd.IdentifierKey)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("O Identificador não pode ser nulo")
                .NotEmpty().WithMessage("O Identificador não pode ser vazio");

            When(command => command.Author is not null, () =>
            {
                RuleFor(cmd => cmd.Author!)
                    .Cascade(CascadeMode.Stop)
                    .SetValidator(new AuthorDTOValidator());
            });

            When(command => command.Contact is not null, () =>
            {
                RuleFor(cmd => cmd.Contact!)
                    .Cascade(CascadeMode.Stop)
                    .SetValidator(new ContactDTOValidator());
            });

            RuleFor(cmd => cmd.Message)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("A Mensagem não pode ser nula")
                .NotEmpty().WithMessage("A mensagem não pode ser vazia")
                .Must(message => message.Length >= 1)
                    .WithMessage("A mensagem deve ter no mínimo 1 caracter")
                .Must(message => message.Length <= 8000)
                    .WithMessage("A mensagem deve ter no máximo 8000 caracteres");

            //RuleFor(cmd => cmd.CreatedAt)
            //    .Cascade(CascadeMode.Stop);

            //RuleFor(cmd => cmd.OccuredAt)
            //    .Cascade(CascadeMode.Stop);


            RuleForEach(cmd => cmd.TagIds)
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
