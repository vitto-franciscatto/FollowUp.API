using System.Collections.Generic;

namespace FollowUp.Domain
{
    public class FollowUp
    {
        public FollowUp()
        {
        }

        private FollowUp(
            int id, 
            int assistanceId, 
            Author? author, 
            Contact? contact, 
            string message, 
            DateTime createdAt, 
            DateTime occuredAt, 
            List<Tag>? tags)
        {
            Id = id;
            AssistanceId = assistanceId;
            Author = author;
            Contact = contact;
            Message = message;
            CreatedAt = createdAt;
            OccuredAt = occuredAt;
            Tags = tags;
        }

        public int Id { get; private set; }
        public int AssistanceId { get; private set; }
        public Author? Author { get; private set; }
        public Contact? Contact { get; private set; }
        public string Message { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime OccuredAt { get; private set; }
        public List<Tag>? Tags { get; private set; }

        public static FollowUp Create(
            int id, 
            int assistanceId,
            Author? author,
            Contact? contact,
            string message,
            DateTime createdAt,
            DateTime occuredAt,
            List<Tag>? tags)
        {
            return new FollowUp(
                id,
                assistanceId,
                author,
                contact,
                message,
                createdAt,
                occuredAt,
                tags);
        }
    }
}