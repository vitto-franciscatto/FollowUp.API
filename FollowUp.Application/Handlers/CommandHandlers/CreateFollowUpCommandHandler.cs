using FluentValidation;
using FollowUp.Application.Commands.CreateFollowUp;
using FollowUp.Application.DTOs;
using FollowUp.Application.Interfaces;
using FollowUp.Application.Notifications;
using LanguageExt.Common;
using MediatR;

namespace FollowUp.Application.Handlers.CommandHandlers
{
    public class CreateFollowUpCommandHandler 
        
        : IRequestHandler<CreateFollowUpCommand, Result<FollowUpDTO>>
    {
        private readonly IFollowUpRepository _repo;
        private readonly IValidator<CreateFollowUpCommand> _validator;
        private readonly IPublisher _publisher;

        public CreateFollowUpCommandHandler(
            IFollowUpRepository repo,
            IValidator<CreateFollowUpCommand> validator,
            IPublisher publisher)
        {
            _repo = repo;
            _validator = validator;
            _publisher = publisher;
        }

        public async Task<Result<FollowUpDTO>> Handle(
            CreateFollowUpCommand request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var validatioNResult = await _validator.ValidateAsync(
                    request, 
                    cancellationToken);
                if(!validatioNResult.IsValid)
                {
                    return new Result<FollowUpDTO>(
                        new ArgumentException(
                            validatioNResult.Errors.First().ErrorMessage));
                }

                Domain.FollowUp newFollowUp =  
                    await _repo.CreateAsync(request.MapToFollowUp());

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
                return new Result<FollowUpDTO>(error);
            }
        }
    }
}
