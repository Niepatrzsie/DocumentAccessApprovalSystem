using Document_Access_Approval_System.Domain.Enums;

namespace Document_Access_Approval_System.Domain.Entities
{
    public class AccessRequest
    {
        public Guid Id { get; private set; }
        public Guid DocumentId { get; private set; }
        public Guid UserId { get; private set; }
        public string Reason { get; private set; } = null!;
        public AccessType AccessType { get; private set; }
        public RequestStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Decision? Decision { get; private set; }
        public User? User { get; private set; }
        public Document? Document { get; private set; }

        private AccessRequest() { } // for EF Core

        public AccessRequest(Guid documentId, Guid userId, string reason, AccessType accessType)
        {
            Id = Guid.NewGuid();
            DocumentId = documentId;
            UserId = userId;
            Reason = reason;
            AccessType = accessType;
            Status = RequestStatus.Pending;
            CreatedAt = DateTime.UtcNow;
        }

        public void Approve(string comment, Guid approverId)
        {
            if (Status != RequestStatus.Pending)
                throw new InvalidOperationException("Request already decided.");

            Decision = new Decision(this.Id, approverId, RequestStatus.Approved, comment);
            Status = RequestStatus.Approved;
        }

        public void Reject(string comment, Guid approverId)
        {
            if (Status != RequestStatus.Pending)
                throw new InvalidOperationException("Request already decided.");

            Decision = new Decision(this.Id, approverId, RequestStatus.Rejected, comment);
            Status = RequestStatus.Rejected;
        }
    }
}
