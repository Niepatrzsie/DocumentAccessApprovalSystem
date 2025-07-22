using Document_Access_Approval_System.Application.Interfaces;
using MediatR;

namespace Document_Access_Approval_System.Application.AccessRequests.Queries.GetUserAccessRequests
{
    public class GetUserAccessRequestsHandler : IRequestHandler<GetUserAccessRequestsQuery, List<UserAccessRequestDto>>
    {
        private readonly IAccessRequestRepository _accessRequestRepository;

        public GetUserAccessRequestsHandler(IAccessRequestRepository accessRequestRepository)
        {
            _accessRequestRepository = accessRequestRepository;
        }

        public async Task<List<UserAccessRequestDto>> Handle(GetUserAccessRequestsQuery request, CancellationToken cancellationToken)
        {
            var accessRequests = await _accessRequestRepository.GetByUserIdAsync(request.UserId);

            var result = new List<UserAccessRequestDto>();

            foreach (var ar in accessRequests)
            {
                var dto = new UserAccessRequestDto
                {
                    Id = ar.Id,
                    DocumentTitle = ar.Document?.Title ?? "[Unknown Document]",
                    AccessType = ar.AccessType,
                    Reason = ar.Reason,
                    Status = ar.Status.ToString(),
                    CreatedAt = ar.CreatedAt
                };

                result.Add(dto);
            }

            return result;
        }
    }
}
