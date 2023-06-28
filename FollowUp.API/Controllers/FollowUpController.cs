using Microsoft.AspNetCore.Mvc;
using System.Net;
using FollowUp.API.Requests;
using LanguageExt.Common;
using FollowUp.Application.DTOs;
using MediatR;
using FollowUp.Application.Queries.GetFollowUpByAssistance;

namespace FollowUps.API.Controllers
{
    [ApiController]
    public class FollowUpController : Controller
    {
        private readonly IMediator _mediator;
        public FollowUpController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[controller]/[action]/{assistanceId}")]
        public async Task<IActionResult> GetByAssistance(int assistanceId, CancellationToken cancellationToken)
        {
            Result<IEnumerable<FollowUpDTO>> response = await _mediator.Send(new GetFollowUpsByAssistanceQuery() { AssistanceId = assistanceId }, cancellationToken);

            return response.Match<IActionResult>(
                dto =>
                {
                    if (!dto.Any())
                    {
                        return StatusCode((int)HttpStatusCode.NoContent);
                    }

                    return StatusCode((int)HttpStatusCode.Created, dto);
                },

                error => StatusCode((int)HttpStatusCode.UnprocessableEntity, error.Message));
        }

        [HttpPost("[controller]")]
        public async Task<IActionResult> Create([FromBody] CreateFollowUpRequest request, CancellationToken cancellationToken)
        {
            Result<FollowUpDTO> response = await _mediator.Send(request.MapToCommand(DateTime.Now), cancellationToken);

            return response.Match<IActionResult>(
                dto => StatusCode((int)HttpStatusCode.Created, dto), 
                error => StatusCode((int)HttpStatusCode.UnprocessableEntity, error.Message));
        }
    }
}