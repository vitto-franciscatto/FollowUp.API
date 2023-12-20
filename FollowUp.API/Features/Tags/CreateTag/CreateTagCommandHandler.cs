using FluentValidation;
using LanguageExt.Common;
using MediatR;
using ILogger = Serilog.ILogger;

namespace FollowUp.API.Features.Tags.CreateTag
{
    public class CreateTagCommandHandler 
        : IRequestHandler<CreateTagCommand, Result<Tag>>
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

        public async Task<Result<Tag>> Handle(
            CreateTagCommand command, 
            CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = _validator.Validate(command);
                if (!validationResult.IsValid)
                {
                    return new Result<Tag>(
                        new ArgumentException(
                            validationResult.Errors.First().ErrorMessage));
                }

                Tag registeredTag = 
                    await _tagRepository.CreateAsync(command.MapToTag());

                await _publisher.Publish(
                    new TagAddedNotification() 
                    { 
                        Tag = registeredTag 
                    }, 
                    cancellationToken);

                return new Result<Tag>(registeredTag);
            }
            catch (Exception error)
            {
                _logger.Error(error, "Failed to handle {@CommandName}", nameof(CreateTagCommand));
                
                return new Result<Tag>(error);
            }
        }
    }
}
