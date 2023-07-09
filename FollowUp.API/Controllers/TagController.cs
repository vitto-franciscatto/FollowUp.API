using FollowUp.API.Requests;
using FollowUp.Application.Commands.CreateTag;
using FollowUp.Application.DTOs;
using FollowUp.Application.Queries;
using LanguageExt.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using FollowUp.API.Authentication;

namespace FollowUp.API.Controllers
{
    [ApiController]
    [ServiceFilter(typeof(ApiKeyAuthenticationFilter))]
    public class TagController : Controller
    {
        private readonly IMediator _mediator;
        public TagController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            Result<IEnumerable<TagDTO>?> response = await _mediator.Send(
                new GetAllTagsQuery(), 
                cancellationToken);

            return response.Match<IActionResult>(
                dtos => 
                {
                    if (dtos is null)
                    {
                        return StatusCode((int)HttpStatusCode.NoContent);
                    }

                    return StatusCode(
                        (int)HttpStatusCode.Created, 
                        dtos);
                },
                error => StatusCode(
                    (int)HttpStatusCode.UnprocessableEntity, 
                    error.Message));
        }

        [HttpPost]
        [Route("[controller]")]
        public async Task<IActionResult> Create(
            [FromBody] CreateTagRequest request, 
            CancellationToken cancellationToken)
        {
            Result<TagDTO> response = await _mediator.Send(
                request.MapToCommand(), 
                cancellationToken);

            return response.Match(
                dto => StatusCode(
                    (int)HttpStatusCode.Created, 
                    dto), 
                error => StatusCode(
                    (int)HttpStatusCode.UnprocessableEntity, 
                    error.Message));
        }
    }
}
