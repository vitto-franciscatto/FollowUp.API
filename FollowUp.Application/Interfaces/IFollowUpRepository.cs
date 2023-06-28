using FollowUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowUp.Application.Interfaces
{
    public interface IFollowUpRepository
    {
        Task<IEnumerable<Domain.FollowUp>?> GetByAssistance(int assistanceId);
        Task<Domain.FollowUp> CreateAsync(Domain.FollowUp entity);
    }
}
