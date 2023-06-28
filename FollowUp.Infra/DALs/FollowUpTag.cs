using FollowUp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowUp.Infra.DALs
{
    internal class FollowUpTag
    {
        public int FollowUpId { get; set; }
        public FollowUpDAL? FollowUp { get; set; }
        public int TagId { get; set; }
        public TagDAL? Tag { get; set; }
    }
}
