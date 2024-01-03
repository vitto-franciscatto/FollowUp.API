using System.Net;
using FollowUp.API.Features.Authentication;
using FollowUp.API.Features.FollowUps.CreateFollowUp;
using FollowUp.API.Features.FollowUps.GetFollowUpsByAssistance;
using FollowUp.API.Features.Tags;
using LanguageExt.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace FollowUp.API.Features.FollowUps
{
    [ApiController]
    [ServiceFilter(typeof(ApiKeyAuthenticationFilter))]
    public class FollowUpController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public FollowUpController(
            IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("[controller]/[action]/{*identifierKey}")]
        public async Task<IActionResult> GetByIdentifier(
            string identifierKey, 
            CancellationToken cancellationToken)
        {
            try
            {
                Result<IEnumerable<FollowUp>> response = await _mediator.Send(
                    new GetFollowUpsByAssistanceQuery() 
                    { 
                        IdentifierKey = identifierKey 
                    }, 
                    cancellationToken);

                return response.Match<IActionResult>(
                    retrievedFollowups =>
                    {
                        if (!retrievedFollowups.Any())
                        {
                            return StatusCode((int)HttpStatusCode.NoContent);
                        }

                        return StatusCode(
                            (int)HttpStatusCode.Created, 
                            retrievedFollowups.Select(followup => new
                            {
                                Id = followup.Id, 
                                IdentifierKey = followup.IdentifierKey, 
                                Author = (AuthorDTO)followup.Author, 
                                Contact = (ContactDTO)followup.Contact, 
                                Message = followup.Message, 
                                CreatedAt = followup.CreatedAt, 
                                OccuredAt = followup.OccuredAt,
                                Tags = followup.Tags?
                                    .Select(tag => (TagDTO)tag)
                            }));
                    },

                    error => StatusCode(
                        (int)HttpStatusCode.UnprocessableEntity, 
                        error.Message));
            }
            catch (Exception error)
            {
                _logger.Error(
                    error, 
                    "Failed to get followups for identifier {@IdentifierKey}", 
                    identifierKey);
                
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost("[controller]")]
        public async Task<IActionResult> Create(
            [FromBody] CreateFollowUpRequest request, 
            CancellationToken cancellationToken)
        {
            try
            {
                Result<FollowUp> response = await _mediator.Send
                
                (request.MapToCommand(DateTime.Now), 
                    cancellationToken);

                return response.Match<IActionResult>(
                    followup => StatusCode(
                        (int)HttpStatusCode.Created, 
                        new
                        {
                            Id = followup.Id, 
                            IdentifierKey = followup.IdentifierKey, 
                            Author = (AuthorDTO)followup.Author, 
                            Contact = (ContactDTO)followup.Contact, 
                            Message = followup.Message, 
                            CreatedAt = followup.CreatedAt, 
                            OccuredAt = followup.OccuredAt,
                            Tags = followup.Tags?
                                .Select(tag => (TagDTO)tag)
                        }), 
                    error => StatusCode(
                        (int)HttpStatusCode.UnprocessableEntity, 
                        error.Message));
            }
            catch (Exception error)
            {
                _logger.Error(
                    error, 
                    "Failed to create followup for identifier {@IdentifierKey} by {@UserId}", 
                    request.IdentifierKey, 
                    request.Author?.Id);
                
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}