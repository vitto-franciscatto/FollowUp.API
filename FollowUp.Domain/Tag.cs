using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowUp.Domain
{
    public class Tag
    {
        private Tag(
            int id, 
            string name) 
        {
            Id = id;
            Name = name;
        }
        
        public int Id { get; private set; }
        public string Name { get; private set; }

        public static Tag Create(int id, string name)
        {
            return new Tag(id, name);
        }
    }
}
