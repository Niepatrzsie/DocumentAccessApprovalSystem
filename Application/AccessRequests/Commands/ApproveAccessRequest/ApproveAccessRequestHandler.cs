using Document_Access_Approval_System.Application.Interfaces;
using Document_Access_Approval_System.Domain.Entities;
using Document_Access_Approval_System.Domain.Enums;
using MediatR;

namespace Document_Access_Approval_System.Application.AccessRequests.Commands.ApproveAccessRequest
{
    public class ApproveAccessRequestHandler : IRequestHandler<ApproveAccessRequestCommand, Unit>
    {
        private readonly IAccessRequestRepository _accessRequestRepository;
        private readonly IUserRepository _userRepository;

        public ApproveAccessRequestHandler(IAccessRequestRepository accessRequestRepository, IUserRepository userRepository)
        {
            _accessRequestRepository = accessRequestRepository;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(ApproveAccessRequestCommand request, CancellationToken cancellationToken)
        {
            var accessRequest = await _accessRequestRepository.GetWithDetailsAsync(request.RequestId);

            if (accessRequest == null)
                throw new InvalidOperationException("Access request not found.");

            if (accessRequest.Status != RequestStatus.Pending)
                throw new InvalidOperationException("Access request is not pending.");

            var approver = await _userRepository.GetByIdAsync(request.ApproverId);

            if (approver == null)
                throw new InvalidOperationException("Approver not found.");

            if (approver.Role != UserRole.Approver)
                throw new UnauthorizedAccessException("User is not authorized to approve requests.");

            accessRequest.Approve(request.Comment, request.ApproverId);

            await _accessRequestRepository.UpdateAsync(accessRequest);

            return Unit.Value;
        }
    }
}
