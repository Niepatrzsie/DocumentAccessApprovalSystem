using Document_Access_Approval_System.Application.Interfaces;
using MediatR;

namespace Document_Access_Approval_System.Application.AccessRequests.Queries.GetPendingAccessRequests
{
    public class GetPendingAccessRequestsHandler : IRequestHandler<GetPendingAccessRequestsQuery, List<PendingAccessRequestDto>>
    {
        private readonly IAccessRequestRepository _accessRequestRepository;

        public GetPendingAccessRequestsHandler(IAccessRequestRepository accessRequestRepository)
        {
            _accessRequestRepository = accessRequestRepository;
        }

        public async Task<List<PendingAccessRequestDto>> Handle(GetPendingAccessRequestsQuery request, CancellationToken cancellationToken)
        {
            var pendingRequests = await _accessRequestRepository.GetPendingAsync();

            var result = new List<PendingAccessRequestDto>();

            foreach (var ar in pendingRequests)
            {
                result.Add(new PendingAccessRequestDto
                {
                    Id = ar.Id,
                    DocumentTitle = ar.Document?.Title ?? "[Unknown Document]",
                    UserName = ar.User?.Name ?? "[Unknown User]",
                    AccessType = ar.AccessType,
                    Reason = ar.Reason,
                    CreatedAt = ar.CreatedAt
                });
            }
            return result;
        }
    }
}
