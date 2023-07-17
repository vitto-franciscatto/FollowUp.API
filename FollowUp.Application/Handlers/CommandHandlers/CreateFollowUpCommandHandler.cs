using FluentValidation;
using FollowUp.Application.Commands.CreateFollowUp;
using FollowUp.Application.DTOs;
using FollowUp.Application.Interfaces;
using FollowUp.Application.Notifications;
using LanguageExt.Common;
using MediatR;
using Serilog;

namespace FollowUp.Application.Handlers.CommandHandlers
{
    public class CreateFollowUpCommandHandler 
        
        : IRequestHandler<CreateFollowUpCommand, Result<FollowUpDTO>>
    {
        private readonly IFollowUpRepository _repo;
        private readonly IValidator<CreateFollowUpCommand> _validator;
        private readonly IPublisher _publisher;
        private readonly ILogger _logger;

        public CreateFollowUpCommandHandler(
            IFollowUpRepository repo,
            IValidator<CreateFollowUpCommand> validator,
            IPublisher publisher, ILogger logger)
        {
            _repo = repo;
            _validator = validator;
            _publisher = publisher;
            _logger = logger;
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

                Domain.FollowUp newFollowUp =  
                    await _repo.CreateAsync(command.MapToFollowUp());

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
