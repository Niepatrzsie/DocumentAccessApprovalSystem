using MediatR;

namespace Document_Access_Approval_System.Application.AccessRequests.Commands.RejectAccessRequest
{
    /// <summary>
    /// Command to reject a pending access request.
    /// </summary>
    public record RejectAccessRequestCommand(Guid RequestId, Guid ApproverId, string Comment) : IRequest<Unit>;
}
