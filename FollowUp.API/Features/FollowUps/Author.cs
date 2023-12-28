using FluentValidation;
using Newtonsoft.Json;

namespace FollowUp.API.Features.FollowUps
{
    public class Author
    {
        public Author()
        {
        }

        private Author(
            int id, 
            string extension)
        {
            Id = id;
            Extension = extension;
        }

        [JsonProperty("id")]
        public int Id { get; private set; }

        [JsonProperty("extension")]
        public string Extension { get; private set; } = string.Empty;

        public static Author Create(
            int id, 
            string extension) 
        { 
            return new Author(
                id, 
                extension);
        }
    }
    
    public class AuthorValidator : AbstractValidator<Author>
    {
        public AuthorValidator()
        {
            RuleFor(author => author.Id)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0)
                .WithMessage("O Id: {PropertyValue} do autor é inválido");
            
            When(author => author.Extension != String.Empty, () =>
            {
                RuleFor(author => author.Extension)
                    .Cascade(CascadeMode.Stop)
                    .NotNull().WithMessage("O Ramal do autor não pode ser nulo")
                    .Must(ext => ext.Length <= 50)
                    .WithMessage("O Ramal do autor deve ter no máximo 50 caracteres");
            });
        }
    }
}
