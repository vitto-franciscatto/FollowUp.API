using FollowUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowUp.Application.Interfaces
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> Get();
        Task<Tag?> Get(int tagId);
        Task<Tag> CreateAsync(Tag tag);
    }
}
