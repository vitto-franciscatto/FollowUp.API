using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowUp.Domain
{
    public class Tag
    {
        private Tag() { }
        private Tag(
            int id, 
            string name) 
        {
            _id = id;
            _name = name;
        }
        private int _id { get; set; } = 0;
        private string _name { get; set; } = string.Empty;

        public int Id => _id;
        public string Name => _name;

        public static Tag Create(int id, string name)
        {
            return new Tag(id, name);
        }
    }
}
