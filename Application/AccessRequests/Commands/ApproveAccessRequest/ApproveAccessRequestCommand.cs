using MediatR;

namespace Document_Access_Approval_System.Application.AccessRequests.Commands.ApproveAccessRequest
{
    /// <summary>
    /// Command to approve a pending access request.
    /// </summary>
    /// <param name="RequestId">The ID of the access request</param>
    /// <param name="ApproverId">The ID of the user approving the request</param>
    /// <param name="Comment">Optional comment</param>
    public record ApproveAccessRequestCommand(Guid RequestId, Guid ApproverId, string Comment) : IRequest<Unit>;
}
