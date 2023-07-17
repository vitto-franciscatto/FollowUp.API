using Microsoft.AspNetCore.Mvc;
using System.Net;
using FollowUp.API.Authentication;
using FollowUp.API.Requests;
using LanguageExt.Common;
using FollowUp.Application.DTOs;
using MediatR;
using FollowUp.Application.Queries;
using ILogger = Serilog.ILogger;

namespace FollowUps.API.Controllers
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

        [HttpGet("[controller]/[action]/{assistanceId}")]
        public async Task<IActionResult> GetByAssistance(
            int assistanceId, 
            CancellationToken cancellationToken)
        {
            try
            {
                Result<IEnumerable<FollowUpDTO>> response = await _mediator.Send(
                    new GetFollowUpsByAssistanceQuery() 
                    { 
                        AssistanceId = assistanceId 
                    }, 
                    cancellationToken);

                return response.Match<IActionResult>(
                    dto =>
                    {
                        if (!dto.Any())
                        {
                            return StatusCode((int)HttpStatusCode.NoContent);
                        }

                        return StatusCode(
                            (int)HttpStatusCode.Created, 
                            dto);
                    },

                    error => StatusCode(
                        (int)HttpStatusCode.UnprocessableEntity, 
                        error.Message));
            }
            catch (Exception error)
            {
                _logger.Error(
                    error, 
                    "Failed to get followups for assistance {@AssistanceId}", 
                    assistanceId);
                
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
                Result<FollowUpDTO> response = await _mediator.Send
                
                (request.MapToCommand(DateTime.Now), 
                    cancellationToken);

                return response.Match<IActionResult>(
                    dto => StatusCode(
                        (int)HttpStatusCode.Created, 
                        dto), 
                    error => StatusCode(
                        (int)HttpStatusCode.UnprocessableEntity, 
                        error.Message));
            }
            catch (Exception error)
            {
                _logger.Error(
                    error, 
                    "Failed to create followup for assistance {@AssistanceId} by {@UserId}", 
                    request.AssistanceId, 
                    request.Author?.Id);
                
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}