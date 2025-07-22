using Document_Access_Approval_System.API.Models;
using Document_Access_Approval_System.Application.AccessRequests.Commands.ApproveAccessRequest;
using Document_Access_Approval_System.Application.AccessRequests.Commands.CreateAccessRequest;
using Document_Access_Approval_System.Application.AccessRequests.Commands.RejectAccessRequest;
using Document_Access_Approval_System.Application.AccessRequests.Queries.GetAccessRequestDetails;
using Document_Access_Approval_System.Application.AccessRequests.Queries.GetPendingAccessRequests;
using Document_Access_Approval_System.Application.AccessRequests.Queries.GetUserAccessRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Document_Access_Approval_System.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccessRequestsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccessRequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Send a new access request for a document.
        /// </summary>
        /// <param name="request">Access request details</param>
        /// <returns>The ID of the newly created access request</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAccessRequest([FromBody] CreateAccessRequestRequest request)
        {
            var command = new CreateAccessRequestCommand(DocumentId: request.DocumentId, UserId: request.UserId, Reason: request.Reason, AccessType: request.AccessType);

            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = id }, new { id });
        }

        /// <summary>
        /// Retrieves detailed information about a specific access request,
        /// including its status, associated document, requester, and decision (if any).
        /// </summary>
        /// <param name="id">The ID of the access request</param>
        /// <returns>Detailed access request information</returns>
        /// <response code="200">Returns the access request details</response>
        /// <response code="404">If the access request was not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var query = new GetAccessRequestDetailsQuery(id);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }


        /// <summary>
        /// Returns all access requests made by a given user.
        /// </summary>
        [HttpGet("user/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserRequests(Guid userId)
        {
            var result = await _mediator.Send(new GetUserAccessRequestsQuery(userId));
            return Ok(result);
        }

        /// <summary>
        /// Returns all pending access requests waiting for approval. (Approval role)
        /// </summary>
        /// <returns>List of pending access requests</returns>
        [HttpGet("pending")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPendingRequests()
        {
            var result = await _mediator.Send(new GetPendingAccessRequestsQuery());
            return Ok(result);
        }

        /// <summary>
        /// Approves a pending access request. (Approval Role)
        /// </summary>
        /// <param name="id">The ID of the access request</param>
        /// <param name="request">Approval details (approver + comment)</param>
        /// <returns>No content on success</returns>
        [HttpPost("{id}/approve")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ApproveRequest(Guid id, [FromBody] ApproveAccessRequestRequest request)
        {
            var command = new ApproveAccessRequestCommand(RequestId: id,ApproverId: request.ApproverId, Comment: request.Comment);

            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Rejects a pending access request. (Approval Role)
        /// </summary>
        /// <param name="id">The ID of the access request</param>
        /// <param name="request">Rejection details</param>
        /// <returns>No content on success</returns>
        [HttpPost("{id}/reject")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RejectRequest(Guid id, [FromBody] RejectAccessRequestRequest request)
        {
            var command = new RejectAccessRequestCommand(
                RequestId: id,
                ApproverId: request.ApproverId,
                Comment: request.Comment
            );

            await _mediator.Send(command);

            return NoContent();
        }


    }
}
