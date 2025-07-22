using Document_Access_Approval_System.Application.Interfaces;
using Document_Access_Approval_System.Domain.Entities;
using MediatR;

namespace Document_Access_Approval_System.Application.AccessRequests.Commands.CreateAccessRequest
{
    public class CreateAccessRequestHandler : IRequestHandler<CreateAccessRequestCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly IAccessRequestRepository _accessRequestRepository;

        public CreateAccessRequestHandler(
            IUserRepository userRepository,
            IDocumentRepository documentRepository,
            IAccessRequestRepository accessRequestRepository)
        {
            _userRepository = userRepository;
            _documentRepository = documentRepository;
            _accessRequestRepository = accessRequestRepository;
        }
        public async Task<Guid> Handle(CreateAccessRequestCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user is null)
                throw new Exception($"User with ID {request.UserId} not found.");

            var document = await _documentRepository.GetByIdAsync(request.DocumentId);
            if (document is null)
                throw new Exception($"Document with ID {request.DocumentId} not found.");

            var accessRequest = new AccessRequest(request.DocumentId, request.UserId, request.Reason, request.AccessType);

            await _accessRequestRepository.AddAsync(accessRequest);

            return accessRequest.Id;
        }
    }
}
