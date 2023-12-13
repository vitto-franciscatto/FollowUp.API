using FluentValidation;
using FollowUp.API.Features.Tags;
using LanguageExt.Common;
using MediatR;

namespace FollowUp.API.Features.FollowUps.CreateFollowUp
{
    public class CreateFollowUpCommand : IRequest<Result<FollowUpDTO>>
    {
        public int AssistanceId { get; set; } = 0;
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
    
    public static class CreateFollowUpCommandMapper
    {
        public static FollowUp MapToFollowUp(
            this CreateFollowUpCommand command)
        {
            return FollowUp.Create(
                0,
                command.AssistanceId, 
                command.Author.MapToAuthor(), 
                command.Contact.MapToContact(), 
                command.Message, 
                command.CreatedAt, 
                command.OccuredAt, 
                command.TagIds?
                    .Select(id => Tag.Create(id, string.Empty))
                    .ToList()
            );
        }
    }
}
