using System.Collections.Generic;

namespace FollowUp.Domain
{
    public class FollowUp
    {
        private FollowUp(
            int id, 
            int assistanceId, 
            Author? Author, 
            Contact? contact, 
            string message, 
            DateTime createdAt, 
            DateTime occuredAt, 
            List<Tag>? tags)
        {
            _id = id;
            _assistanceId = assistanceId;
            _author = Author;
            _contact = contact;
            _message = message;
            _createdAt = createdAt;
            _occuredAt = occuredAt;
            _tags = tags;
        }

        private int _id { get; set; } = 0;
        private int _assistanceId { get; set; } = 0;
        private Author? _author { get; set; }
        private Contact? _contact { get; set; }
        private string _message { get; set; } = string.Empty;
        private DateTime _createdAt { get; set; } = DateTime.MinValue;
        private DateTime _occuredAt { get; set; } = DateTime.MinValue;
        private List<Tag>? _tags { get; set; }

        public int Id => _id;
        public int AssistanceId => _assistanceId;
        public Author? Author => _author;
        public Contact? Contact => _contact;
        public string Message => _message;
        public DateTime CreatedAt => _createdAt;
        public DateTime OccuredAt => _occuredAt;
        public List<Tag>? Tags => _tags;

        public static FollowUp Create(
            int id, 
            int assistanceId,
            Author? Author,
            Contact? contact,
            string message,
            DateTime createdAt,
            DateTime occuredAt,
            List<Tag>? tags)
        {
            return new FollowUp(
                id,
                assistanceId,
                Author,
                contact,
                message,
                createdAt,
                occuredAt,
                tags);
        }
    }
}