using Document_Access_Approval_System.Application.Interfaces;
using MediatR;

namespace Document_Access_Approval_System.Application.AccessRequests.Queries.GetAccessRequestDetails
{
    public class GetAccessRequestDetailsHandler : IRequestHandler<GetAccessRequestDetailsQuery, AccessRequestDetailsDto>
    {
        private readonly IAccessRequestRepository _accessRequestRepository;
        private readonly IUserRepository _userRepository;

        public GetAccessRequestDetailsHandler(IAccessRequestRepository accessRequestRepository, IUserRepository userRepository)
        {
            _accessRequestRepository = accessRequestRepository;
            _userRepository = userRepository;
        }

        public async Task<AccessRequestDetailsDto> Handle(GetAccessRequestDetailsQuery request, CancellationToken cancellationToken)
        {
            var accessRequest = await _accessRequestRepository.GetWithDetailsAsync(request.RequestId);

            if (accessRequest == null)
                throw new KeyNotFoundException("Access request not found.");

            var dto = new AccessRequestDetailsDto
            {
                Id = accessRequest.Id,
                DocumentTitle = accessRequest.Document?.Title ?? "[Unknown Document]",
                Reason = accessRequest.Reason,
                AccessType = accessRequest.AccessType,
                Status = accessRequest.Status,
                CreatedAt = accessRequest.CreatedAt,
                RequestedBy = accessRequest.User?.Name ?? "[Unknown User]"
            };

            if (accessRequest.Decision != null)
            {
                var approver = await _userRepository.GetByIdAsync(accessRequest.Decision.ApprovedBy);

                dto.Decision = new DecisionDto
                {
                    ApprovedBy = approver?.Name ?? "[Unknown Approver]",
                    Comment = accessRequest.Decision.Comment,
                    Status = accessRequest.Decision.Status,
                    CreatedAt = accessRequest.Decision.DecidedAt
                };
            }

            return dto;
        }
    }
}
