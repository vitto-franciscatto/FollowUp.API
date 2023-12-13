using FluentValidation;
using LanguageExt.Common;
using MediatR;
using ILogger = Serilog.ILogger;

namespace FollowUp.API.Features.Tags.CreateTag
{
    public class CreateTagCommandHandler 
        : IRequestHandler<CreateTagCommand, Result<TagDTO>>
    {
        private readonly ITagRepository _tagRepository;
        private readonly IValidator<CreateTagCommand> _validator;
        private readonly IPublisher _publisher;
        private readonly ILogger _logger;

        public CreateTagCommandHandler(
            ITagRepository tagRepository,
            IValidator<CreateTagCommand> validator,
            IPublisher publisher, ILogger logger)
        {
            _tagRepository = tagRepository;
            _validator = validator;
            _publisher = publisher;
            _logger = logger;
        }

        public async Task<Result<TagDTO>> Handle(
            CreateTagCommand command, 
            CancellationToken cancellationToken)
        {
            try
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
                    new TagAddedNotification() 
                    { 
                        Tag = newTag 
                    }, 
                    cancellationToken);

                return new Result<TagDTO>(newTag.MapToTagDTO());
            }
            catch (Exception error)
            {
                _logger.Error(error, "Failed to handle {@CommandName}", nameof(CreateTagCommand));
                
                return new Result<TagDTO>(error);
            }
        }
    }
}
