using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowUp.Domain
{
    public class Author
    {
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
}
