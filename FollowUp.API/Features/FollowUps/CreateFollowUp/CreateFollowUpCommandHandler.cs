using FluentValidation;
using FollowUp.API.Features.Tags;
using LanguageExt.Common;
using MediatR;
using ILogger = Serilog.ILogger;

namespace FollowUp.API.Features.FollowUps.CreateFollowUp
{
    public class CreateFollowUpCommandHandler 
        
        : IRequestHandler<CreateFollowUpCommand, Result<FollowUp>>
    {
        private readonly IFollowUpRepository _repo;
        private readonly ITagRepository _tagRepository;
        private readonly IValidator<CreateFollowUpCommand> _validator;
        private readonly IPublisher _publisher;
        private readonly ILogger _logger;

        public CreateFollowUpCommandHandler(
            IFollowUpRepository repo,
            IValidator<CreateFollowUpCommand> validator,
            IPublisher publisher, ILogger logger, 
            ITagRepository tagRepository)
        {
            _repo = repo;
            _validator = validator;
            _publisher = publisher;
            _logger = logger;
            _tagRepository = tagRepository;
        }

        public async Task<Result<FollowUp>> Handle(
            CreateFollowUpCommand command, 
            CancellationToken cancellationToken)
        {
            try
            {
                var validatioNResult = await _validator.ValidateAsync(
                    command, 
                    cancellationToken);
                if(!validatioNResult.IsValid)
                {
                    return new Result<FollowUp>(
                        new ArgumentException(
                            validatioNResult.Errors.First().ErrorMessage));
                }

                var chosenTags = new List<Tag>();
                if (command.TagIds is not null && command.TagIds.Any())
                {
                    chosenTags = 
                        (await _tagRepository.Get())?
                        .Where(tag => command.TagIds.Contains(tag.Id))
                        .ToList();
                }

                var registeredFollowup = FollowUp.Create(
                    0,
                    command.IdentifierKey,
                    command.Author.MapToAuthor(),
                    command.Contact.MapToContact(),
                    command.Message,
                    command.CreatedAt,
                    command.OccuredAt,
                    chosenTags
                );
                
                bool wasFollowUpRegistered = await _repo.AddAsync(registeredFollowup);
                if (!wasFollowUpRegistered)
                {
                    _logger.Error("Failed to persist followup to database");
                    
                    return new Result<FollowUp>(new Exception("FollowUp não foi salvo"));
                }

                await _publisher.Publish(
                    new FollowUpAddedNotification() 
                    { 
                        FollowUp = registeredFollowup 
                    }, 
                    cancellationToken);

                return new Result<FollowUp>(
                    registeredFollowup);
            }
            catch (Exception error)
            {
                _logger.Error(error, "Failed to handle {@CommandName}", nameof(CreateFollowUpCommand));
                
                return new Result<FollowUp>(error);
            }
        }
    }
}
