using FollowUp.Application.DTOs;
using LanguageExt.Common;
using MediatR;

namespace FollowUp.Application.Commands.CreateFollowUp
{
    public class CreateFollowUpCommand : IRequest<Result<FollowUpDTO>>
    {
        public int AssistanceId { get; set; } = 0;
        public AuthorDTO? Author { get; set; }
        public ContactDTO? Contact { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.MinValue;
        public DateTime OccuredAt { get; set; } = DateTime.MinValue;
        public IEnumerable<int>? TagIds { get; set; }
    }
}
