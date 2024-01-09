using FluentValidation;
using Newtonsoft.Json;

namespace FollowUp.API.Features.FollowUps
{
    public class Author
    {
        private Author(){}

        public int Id { get; set; }

        public string Extension { get; set; } = string.Empty;

        public static Author Construct()
        {
            var author = new Author();

            return author;
        }

        public static Author Create(
            int id, 
            string extension)
        {
            var author = Construct();
            author.Id = id;
            author.Extension = extension;
            
            return author;
        }
    }
    
    public class AuthorDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("extension")] 
        public string Extension { get; set; } = default!;

        public static explicit operator Author(AuthorDTO? authorDto)
        {
            if (authorDto is null)
            {
                return default!;
            }
            
            var author = Author.Construct();
            author.Id = authorDto.Id;
            author.Extension = authorDto.Extension;

            return author;
        }
        
        public static explicit operator AuthorDTO(Author? author)
        {           
            if (author is null)
            {
                return default!;
            }
            
            var authorDto = new AuthorDTO
            {
                Id = author.Id,
                Extension = author.Extension
            };

            return authorDto;
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
