namespace Document_Access_Approval_System.Application.AccessRequests.Commands.CreateAccessRequest
{
    using Document_Access_Approval_System.Domain.Enums;
    using MediatR;
    public record CreateAccessRequestCommand(Guid DocumentId, Guid UserId, string Reason, AccessType AccessType) : IRequest<Guid>;
}
