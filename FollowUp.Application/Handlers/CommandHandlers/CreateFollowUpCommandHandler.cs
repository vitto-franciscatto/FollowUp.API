using FluentValidation;
using FollowUp.Application.Commands.CreateFollowUp;
using FollowUp.Application.DTOs;
using FollowUp.Application.Interfaces;
using FollowUp.Application.Notifications;
using FollowUp.Domain;
using LanguageExt.Common;
using MediatR;
using Serilog;

namespace FollowUp.Application.Handlers.CommandHandlers
{
    public class CreateFollowUpCommandHandler 
        
        : IRequestHandler<CreateFollowUpCommand, Result<FollowUpDTO>>
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

        public async Task<Result<FollowUpDTO>> Handle(
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
                    return new Result<FollowUpDTO>(
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

                Domain.FollowUp newFollowUp = Domain.FollowUp.Create(
                    0,
                    command.AssistanceId,
                    command.Author.MapToAuthor(),
                    command.Contact.MapToContact(),
                    command.Message,
                    command.CreatedAt,
                    command.OccuredAt,
                    chosenTags
                );
                
                bool wasFollowUpRegistered = await _repo.AddAsync(newFollowUp);
                if (!wasFollowUpRegistered)
                {
                    _logger.Error("Failed to persist followup to database");
                    
                    return new Result<FollowUpDTO>(new Exception("FollowUp não foi salvo"));
                }

                await _publisher.Publish(
                    new FollowUpAddedNotification() 
                    { 
                        FollowUp = newFollowUp 
                    }, 
                    cancellationToken);

                return new Result<FollowUpDTO>(
                    newFollowUp.MapToFollowUpDTO());
            }
            catch (Exception error)
            {
                _logger.Error(error, "Failed to handle {@CommandName}", nameof(CreateFollowUpCommand));
                
                return new Result<FollowUpDTO>(error);
            }
        }
    }
}
