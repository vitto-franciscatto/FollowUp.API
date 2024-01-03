using FluentValidation;
using Newtonsoft.Json;

namespace FollowUp.API.Features.FollowUps
{
    public class Contact
    {
        private Contact(){}
        
        private string _name = default!;
        private string _phoneNumber = default!;
        private string _job = default!;

        public static Contact Construct()
        {
            var contact = new Contact();

            return contact;
        }

        public string Name
        {
            get => _name; 
            set => _name = value;
        }

        public string PhoneNumber
        {
            get => _phoneNumber; 
            set => _phoneNumber = value;
        }

        public string Job
        {
            get => _job; 
            set => _job = value;
        }

        public static Contact Create(
            string name,
            string phoneNumber,
            string job)
        {
            var contact = Construct();
            contact._name = name;
            contact._phoneNumber = phoneNumber;
            contact._job = job;

            return contact;
        }
    }
    
    public class ContactDTO
    {
        [JsonProperty("name")]
        public string Name { get; set; } = default!;

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; } = default!;

        [JsonProperty("job")]
        public string Job { get; set; } = default!;
        
        public static explicit operator Contact(ContactDTO? contactDto)
        {
            if (contactDto is null)
            {
                return default!;
            }
            
            var contact = Contact.Construct();
            contact.Name = contactDto.Name;
            contact.PhoneNumber = contactDto.PhoneNumber;
            contact.Job = contactDto.Job;

            return contact;
        }
        
        public static explicit operator ContactDTO(Contact? contact)
        {
            if (contact is null)
            {
                return default!;
            }
            
            var contactDto = new ContactDTO
            {
                Name = contact.Name,
                PhoneNumber = contact.PhoneNumber,
                Job = contact.Job
            };

            return contactDto;
        }
    }
    
    public class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            When(contact => !string.IsNullOrEmpty(contact.Name), () => 
            {
                RuleFor(contact => contact.Name)
                    .Cascade(CascadeMode.Stop)
                    .Must(name => name.Length >= 1)
                    .WithMessage("O Nome do contato deve ter no mínimo 1 caracter")
                    .Must(name => name.Length <= 500)
                    .WithMessage("O Nome do contato deve ter no máximo 500 caracteres");
            });

            When(contact => !string.IsNullOrEmpty(contact.PhoneNumber), () =>
            {
                RuleFor(contact => contact.PhoneNumber)
                    .Cascade(CascadeMode.Stop)
                    .Must(phoneNumber => phoneNumber.Length >= 11)
                    .WithMessage("O Número de Telefone do contato deve ter no mínimo 11 caracteres")
                    .Must(phoneNumber => phoneNumber.Length <= 19)
                    .WithMessage("O Número de Telefone do contato deve ter no máximo 19 caracteres");
            });

            When(contact => !string.IsNullOrEmpty(contact.Job), () =>
            {
                RuleFor(contact => contact.Job)
                    .Cascade(CascadeMode.Stop)
                    .Must(job => job.Length >= 1)
                    .WithMessage("O Cargo do contato deve ter no mínimo 1 caracter")
                    .Must(job => job.Length <= 255)
                    .WithMessage("O Cargo do contato deve ter no máximo 255 caracteres");
            });
        }
    }
}
