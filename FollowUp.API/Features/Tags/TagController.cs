using System.Net;
using FollowUp.API.Features.Authentication;
using FollowUp.API.Features.Tags.CreateTag;
using FollowUp.API.Features.Tags.GetAllTags;
using LanguageExt.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace FollowUp.API.Features.Tags
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
                Result<IEnumerable<Tag>?> response = await _mediator.Send(
                    new GetAllTagsQuery(), 
                    cancellationToken);

                return response.Match<IActionResult>(
                    tags => 
                    {
                        if (tags is null || tags.Length() == 0)
                        {
                            return StatusCode((int)HttpStatusCode.NoContent);
                        }

                        return StatusCode(
                            (int)HttpStatusCode.OK, 
                            tags);
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
                Result<Tag> response = await _mediator.Send(
                    request.MapToCommand(), 
                    cancellationToken);

                return response.Match(
                    tag => StatusCode(
                        (int)HttpStatusCode.Created, 
                        new
                        {
                            Id = tag.Id, 
                            Name = tag.Name
                        }), 
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
