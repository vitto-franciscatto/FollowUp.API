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

        public int Id { get; private set; }
        public string Extension { get; private set; }

        public static Author Create(
            int id, 
            string extension) 
        { 
            return new Author(
                id, 
                extension);
        }
    }
    
    public class AuthorDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; } = 0;

        [JsonProperty("extension")]
        public string Extension { get; set; } = string.Empty;
    }
    
    public class AuthorDTOValidator : AbstractValidator<AuthorDTO>
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
    
    public static class AuthorMapper
    {
        public static Author? MapToAuthor(
            this AuthorDTO? dto)
        {
            if(dto is null)
            {
                return null;
            }

            return Author.Create(
                dto.Id, 
                dto.Extension);
        }

        public static AuthorDTO? MapToAuthor(
            this Author? entity)
        {
            if (entity is null)
            {
                return null;
            }

            return new AuthorDTO() 
            { 
                Id = entity.Id, 
                Extension = entity.Extension 
            };
        }
    }
}
