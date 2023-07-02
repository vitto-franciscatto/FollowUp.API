using FollowUp.Domain;
using FollowUp.Infra.DALs;

namespace FollowUp.Infra
{
    internal static class FollowUpMapper
    {
        internal static Domain.FollowUp MapToFollowUp(
            this FollowUpDAL dal)
        {
            return Domain.FollowUp.Create(
                dal.Id, 
                dal.AssistanceId, 
                dal.AuthorId is null ? 
                    null : 
                    Author.Create(
                        (int)dal.AuthorId, 
                        dal.AuthorExtension), 
                Contact.Create(
                    dal.ContactName, 
                    dal.ContactPhoneNumber, 
                    dal.ContactJob), 
                dal.Message, 
                dal.CreatedAt, 
                dal.OccuredAt, 
                dal.Tags?
                    .Select(_ => _.Tag?.MapToTag())
                    .ToList());
        }

        internal static FollowUpDAL MapToFollowUpDAL(
            this Domain.FollowUp entity)
        {
            return new FollowUpDAL()
            {
                Id = entity.Id,
                AssistanceId = entity.AssistanceId,
                AuthorId = entity.Author?.Id,
                AuthorExtension = entity.Author is null ? 
                    string.Empty:  
                    entity.Author.Extension,
                ContactName = entity.Contact is null ? 
                    string.Empty : 
                    entity.Contact.Name,
                ContactJob = entity.Contact is null ? 
                    string.Empty : 
                    entity.Contact.Job,
                ContactPhoneNumber = entity.Contact is null ? 
                    string.Empty : 
                    entity.Contact.PhoneNumber,
                Message = entity.Message,
                CreatedAt = entity.CreatedAt,
                OccuredAt = entity.OccuredAt,
                Tags = entity.Tags?.Select(_ => 
                    new FollowUpTag() 
                    { 
                        FollowUpId = entity.Id, 
                        TagId = _.Id
                    }).ToList()
            };
        }
    }
}
