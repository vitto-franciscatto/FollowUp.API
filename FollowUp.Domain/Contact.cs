using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowUp.Domain
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
}
