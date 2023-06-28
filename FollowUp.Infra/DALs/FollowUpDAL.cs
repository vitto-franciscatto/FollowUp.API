using FollowUp.Infra.DALs;
using System.Collections.Generic;

namespace FollowUp.Infra
{
    public class FollowUpDAL
    {
        public FollowUpDAL() { }

        public int Id { get; set; } = 0;
        public int AssistanceId { get; set; } = 0;
        public int? AuthorId { get; set; }
        public string AuthorExtension { get; set; } = string.Empty;
        public string ContactName { get; set; } = string.Empty;
        public string ContactPhoneNumber { get; set; } = string.Empty;
        public string ContactJob { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.MinValue;
        public DateTime OccuredAt { get; set; } = DateTime.MinValue;
        internal List<FollowUpTag>? Tags { get; set; }
    }
}