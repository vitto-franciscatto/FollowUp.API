using FollowUp.API.Requests;
using FollowUp.Application.Commands.CreateTag;
using FollowUp.Application.DTOs;
using FollowUp.Application.Queries;
using LanguageExt.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using FollowUp.API.Authentication;
using ILogger = Serilog.ILogger;

namespace FollowUp.API.Controllers
{
    [ApiController]
    [ServiceFilter(typeof(ApiKeyAuthenticationFilter))]
    public class TagController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public TagController(IMediator mediator, ILogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                Result<IEnumerable<TagDTO>?> response = await _mediator.Send(
                    new GetAllTagsQuery(), 
                    cancellationToken);

                return response.Match<IActionResult>(
                    dtos => 
                    {
                        if (dtos is null || dtos.Length() == 0)
                        {
                            return StatusCode((int)HttpStatusCode.NoContent);
                        }

                        return StatusCode(
                            (int)HttpStatusCode.OK, 
                            dtos);
                    },
                    error => StatusCode(
                        (int)HttpStatusCode.UnprocessableEntity, 
                        error.Message));
            }
            catch (Exception error)
            {
                _logger.Error(error, "Failed to get tags");
                
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("[controller]")]
        public async Task<IActionResult> Create(
            [FromBody] CreateTagRequest request, 
            CancellationToken cancellationToken)
        {
            try
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
            catch (Exception error)
            {
                _logger.Error(error, "Failed to create Tag with name {@TagName}", request.Name);
                
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
