using FollowUp.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowUp.Application.Notifications
{
    public class TagAddedNotification : INotification
    {
        public Tag Tag { get; set; }
    }
}
