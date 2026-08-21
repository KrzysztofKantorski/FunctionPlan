using API.Extensions;
using Application.Comments.Commands.CreateCommentCommand;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [EnableRateLimiting("GlobalLimit")]
    [Route("api/meetings/")]
    
    public sealed class CommentsController : ControllerBase
    {
        private ISender _sender;
        public CommentsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("{MeetingId}/comments")]
        public async Task<IActionResult> CreateComment(
            [FromRoute] int MeetingId,
            [FromBody] CreateCommentUserRequest request,
            CancellationToken cancellationToken
            )
        {
            var command = new CreateCommentCommand(
                MeetingId,
                User.GetUserId(),
                request.Content,
                request.ParentCommentId
            );

            await _sender.Send(command, cancellationToken);

            return NoContent();
        }

    }
}
