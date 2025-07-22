using Document_Access_Approval_System.Application.Interfaces;
using Document_Access_Approval_System.Domain.Enums;
using MediatR;

namespace Document_Access_Approval_System.Application.AccessRequests.Commands.RejectAccessRequest
{
    public class RejectAccessRequestHandler : IRequestHandler<RejectAccessRequestCommand, Unit>
    {
        private readonly IAccessRequestRepository _accessRequestRepository;
        private readonly IUserRepository _userRepository;

        public RejectAccessRequestHandler(IAccessRequestRepository accessRequestRepository, IUserRepository userRepository)
        {
            _accessRequestRepository = accessRequestRepository;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(RejectAccessRequestCommand request, CancellationToken cancellationToken)
        {
            var accessRequest = await _accessRequestRepository.GetWithDetailsAsync(request.RequestId);

            if (accessRequest is null)
                throw new InvalidOperationException("Access request not found.");

            if (accessRequest.Status != RequestStatus.Pending)
                throw new InvalidOperationException("Access request is not pending.");

            var approver = await _userRepository.GetByIdAsync(request.ApproverId);

            if (approver is null || approver.Role != UserRole.Approver)
                throw new UnauthorizedAccessException("Only approvers can reject access requests.");

            accessRequest.Reject(request.Comment, request.ApproverId);

            await _accessRequestRepository.UpdateAsync(accessRequest);

            return Unit.Value;
        }
    }
}
