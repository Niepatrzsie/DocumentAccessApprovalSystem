using MediatR;

namespace Document_Access_Approval_System.Application.AccessRequests.Queries.GetAccessRequestDetails
{
    /// <summary>
    /// Query to get details of a specific access request.
    /// </summary>
    public record GetAccessRequestDetailsQuery(Guid RequestId) : IRequest<AccessRequestDetailsDto>;
}
