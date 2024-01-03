using FluentValidation;
using FollowUp.API.Features.Tags;
using Newtonsoft.Json;

namespace FollowUp.API.Features.FollowUps.CreateFollowUp
{
    public class CreateFollowUpRequest
    {   
        [JsonProperty("identifierKey")]
        public string IdentifierKey { get; set; } = string.Empty;

        [JsonProperty("author")]
        public AuthorDTO? Author { get; set; }

        [JsonProperty("contact")]
        public ContactDTO? Contact { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;

        [JsonProperty("dateTime")]
        public DateTime DateTime { get; set; } = DateTime.MinValue;

        [JsonProperty("tags")]
        public IEnumerable<int>? Tags { get; set; }
    }
    
    public class CreateFollowUpRequestValidator 
        : AbstractValidator<CreateFollowUpRequest>
    {
        private readonly ITagRepository _tagRepository;
        public CreateFollowUpRequestValidator(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;

            RuleFor(request => request.IdentifierKey)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("O Identificador não pode ser nulo")
                .NotEmpty().WithMessage("O Identificador não pode ser vazio");

            // When(command => command.Author is not null, () =>
            // {
            //     RuleFor(request => request.Author!)
            //         .SetValidator(new AuthorValidator());
            // });

            // When(command => command.Contact is not null, () =>
            // {
            //     RuleFor(request => request.Contact!)
            //         .SetValidator(new ContactValidator());
            // });

            RuleFor(request => request.Message)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("A mensagem não pode ser nula")
                .NotEmpty().WithMessage("A mensagem não pode ser vazia")
                .Must(message => message.Length <= 8000)
                    .WithMessage("A mensagem deve ter no máximo 8000 caracteres");

            //RuleFor(request => request.CreatedAt)
            //    .Cascade(CascadeMode.Stop);

            //RuleFor(request => request.OccuredAt)
            //    .Cascade(CascadeMode.Stop);


            RuleForEach(request => request.Tags)
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
    
    public static class CreateFollowUpRequestMapper
    {
        public static CreateFollowUpCommand MapToCommand(
            this CreateFollowUpRequest request, 
            DateTime dateTime)
        {
            return new CreateFollowUpCommand()
            {
                IdentifierKey = request.IdentifierKey,
                Author = (Author)request.Author,
                Contact = (Contact)request.Contact,
                Message = request.Message,
                CreatedAt = dateTime,
                OccuredAt = request.DateTime,
                TagIds = request.Tags,
            };
        }
    }
}
