using FluentValidation;
using Newtonsoft.Json;

namespace FollowUp.API.Features.FollowUps
{
    public class Contact
    {
        public Contact()
        {
        }

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; private set; }

        [JsonProperty("job")]
        public string Job { get; private set; }

        public static Contact Create(
            string name,
            string phoneNumber,
            string job)
        {
            var contact = new Contact
            {
                Name = name, 
                PhoneNumber = phoneNumber, 
                Job = job
            };

            return contact;
        }
    }
    
    public class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator()
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
}
