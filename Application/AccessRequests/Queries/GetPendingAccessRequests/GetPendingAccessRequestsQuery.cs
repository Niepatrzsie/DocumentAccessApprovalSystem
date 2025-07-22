using MediatR;

namespace Document_Access_Approval_System.Application.AccessRequests.Queries.GetPendingAccessRequests
{
    /// <summary>
    /// Query to get all access requests that are still pending approval.
    /// </summary>
    public record GetPendingAccessRequestsQuery() : IRequest<List<PendingAccessRequestDto>>;
}
