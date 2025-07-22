using MediatR;

namespace Document_Access_Approval_System.Application.AccessRequests.Queries.GetUserAccessRequests
{
    /// <summary>
    /// Query to retrieve all access requests made by a specific user.
    /// </summary>
    /// <param name="UserId">The ID of the user</param>
    public record GetUserAccessRequestsQuery(Guid UserId) : IRequest<List<UserAccessRequestDto>>;
}
