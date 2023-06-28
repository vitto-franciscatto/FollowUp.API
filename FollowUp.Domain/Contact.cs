using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowUp.Domain
{
    public class Contact
    {
        internal Contact(
            string name, 
            string phoneNumber, 
            string job)
        {
            _name = name;
            _phoneNumber = phoneNumber;
            _job = job;
        }

        internal string _name { get; set; } = string.Empty;
        internal string _phoneNumber { get; set; }
        internal string _job { get; set; } = string.Empty;

        public string Name => _name;
        public string PhoneNumber => _phoneNumber;
        public string Job => _job;

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
