using FluentValidation;
using Newtonsoft.Json;

namespace FollowUp.API.Features.FollowUps
{
    public class Contact
    {
        public Contact()
        {
        }

        private Contact(
            string name, 
            string phoneNumber, 
            string job)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Job = job;
        }

        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Job { get; private set; }

        public static Contact Create(
            string name,
            string phoneNumber,
            string job)
        {
            return new Contact(
                name, 
                phoneNumber, 
                job);
        }
    }
    
    public class ContactDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; } = string.Empty;

        [JsonProperty("job")]
        public string Job { get; set; } = string.Empty;
    }
    
    public class ContactDTOValidator : AbstractValidator<ContactDTO>
    {
        public ContactDTOValidator()
        {
            When(contact => !string.IsNullOrEmpty(contact.Name), () => 
            {
                RuleFor(_ => _.Name)
                    .Cascade(CascadeMode.Stop)
                    .Must(name => name.Length >= 1)
                    .WithMessage("O Nome do contato deve ter no mínimo 1 caracter")
                    .Must(name => name.Length <= 500)
                    .WithMessage("O Nome do contato deve ter no máximo 500 caracteres");
            });

            When(contact => !string.IsNullOrEmpty(contact.PhoneNumber), () =>
            {
                RuleFor(_ => _.PhoneNumber)
                    .Cascade(CascadeMode.Stop)
                    .Must(phoneNumber => phoneNumber.Length >= 11)
                    .WithMessage("O Número de Telefone do contato deve ter no mínimo 11 caracteres")
                    .Must(phoneNumber => phoneNumber.Length <= 19)
                    .WithMessage("O Número de Telefone do contato deve ter no máximo 19 caracteres");
            });

            When(contact => !string.IsNullOrEmpty(contact.Job), () =>
            {
                RuleFor(_ => _.Job)
                    .Cascade(CascadeMode.Stop)
                    .Must(job => job.Length >= 1)
                    .WithMessage("O Cargo do contato deve ter no mínimo 1 caracter")
                    .Must(job => job.Length <= 255)
                    .WithMessage("O Cargo do contato deve ter no máximo 255 caracteres");
            });
        }
    }
    
    public static class ContactMapper
    {
        public static Contact? MapToContact (
            this ContactDTO? dto)
        {
            if (dto is null)
            {
                return null;
            }

            return Contact.Create(
                dto.Name, 
                dto.PhoneNumber, 
                dto.Job);
        }

        public static ContactDTO? MapToContact(
            this Contact? entity)
        {
            if (entity is null)
            {
                return null;
            }

            return new ContactDTO() 
            { 
                Name = entity.Name, 
                PhoneNumber = entity.PhoneNumber, 
                Job = entity.Job 
            };
        }
    }
}
