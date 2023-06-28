using FollowUp.Infra.DALs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowUp.Infra
{
    public class TagDAL
    {
        public TagDAL() { }

        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;

        internal List<FollowUpTag>? FollowUps { get; set; }
    }
}
