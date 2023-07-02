using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowUp.Application.Notifications
{
    public class FollowUpAddedNotification : INotification
    {
        public Domain.FollowUp FollowUp { get; set; }
    }
}
