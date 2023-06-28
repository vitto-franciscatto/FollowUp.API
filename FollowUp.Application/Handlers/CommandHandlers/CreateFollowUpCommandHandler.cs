using FluentValidation;
using FollowUp.Application.Commands.CreateFollowUp;
using FollowUp.Application.DTOs;
using FollowUp.Application.Interfaces;
using LanguageExt.Common;
using MediatR;

namespace FollowUp.Application.Handlers.CommandHandlers
{
    public class CreateFollowUpCommandHandler : IRequestHandler<CreateFollowUpCommand, Result<FollowUpDTO>>
    {
        private readonly IFollowUpRepository _repo;
        private readonly IValidator<CreateFollowUpCommand> _validator;

        public CreateFollowUpCommandHandler(
            IFollowUpRepository repo, 
            IValidator<CreateFollowUpCommand> validator)
        {
            _repo = repo;
            _validator = validator;
        }

        public async Task<Result<FollowUpDTO>> Handle(CreateFollowUpCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validatioNResult = await _validator.ValidateAsync(request);
                if(!validatioNResult.IsValid)
                {
                    return new Result<FollowUpDTO>(new ArgumentException(validatioNResult.Errors.First().ErrorMessage));
                }

                Domain.FollowUp newFollowUp =  await _repo.CreateAsync(request.MapToFollowUp());

                return new Result<FollowUpDTO>(newFollowUp.MapToFollowUpDTO());
            }
            catch (Exception error)
            {
                return new Result<FollowUpDTO>(error);
            }
        }
    }
}
