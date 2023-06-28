using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowUp.Domain
{
    public class Author
    {
        public Author(
            int id, 
            string extension)
        {
            _id = id;
            _extension = extension;
        }

        internal int _id { get; set; } = 0;
        internal string _extension { get; set; } = string.Empty;

        public int Id => _id;
        public string Extension => _extension;

        public static Author Create(
            int id, 
            string extension) 
        { 
            return new Author(
                id, 
                extension);
        }
    }
}
