using FluentValidation;
using FollowUp.Application.Commands.CreateTag;
using FollowUp.Application.DTOs;
using FollowUp.Application.Interfaces;
using FollowUp.Application.Notifications;
using FollowUp.Domain;
using LanguageExt.Common;
using MediatR;

namespace FollowUp.Application.Handlers.CommandHandlers
{
    public class CreateTagCommandHandler 
        : IRequestHandler<CreateTagCommand, Result<TagDTO>>
    {
        private readonly ITagRepository _tagRepository;
        private readonly IValidator<CreateTagCommand> _validator;
        private readonly IPublisher _publisher;

        public CreateTagCommandHandler(
            ITagRepository tagRepository,
            IValidator<CreateTagCommand> validator,
            IPublisher publisher)
        {
            _tagRepository = tagRepository;
            _validator = validator;
            _publisher = publisher;
        }

        public async Task<Result<TagDTO>> Handle(
            CreateTagCommand command, 
            CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(command);
            if (!validationResult.IsValid)
            {
                return new Result<TagDTO>(
                    new ArgumentException(
                        validationResult.Errors.First().ErrorMessage));
            }

            Tag newTag = 
                await _tagRepository.CreateAsync(command.MapToTag());

            await _publisher.Publish(
                new TagAddNotification() 
                { 
                    Tag = newTag 
                }, 
                cancellationToken);

            return new Result<TagDTO>(newTag.MapToTagDTO());
        }
    }
}
