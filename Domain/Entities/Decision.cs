using Document_Access_Approval_System.Domain.Enums;

namespace Document_Access_Approval_System.Domain.Entities
{
    //zero to one with AccessRequest - one or zero AccessRequest could have one Decision
    public class Decision
    {
        public Guid Id { get; private set; }
        public Guid AccessRequestId { get; private set; }
        public Guid ApprovedBy { get; private set; }
        public RequestStatus Status { get; private set; }
        public string Comment { get; private set; } = null!;
        public DateTime DecidedAt { get; private set; }

        public AccessRequest? AccessRequest { get; private set; }

        private Decision() { } // For EF Core

        public Decision(Guid accessRequestId, Guid approvedBy, RequestStatus status, string comment)
        {
            if (status != RequestStatus.Approved && status != RequestStatus.Rejected)
                throw new ArgumentException("Decision must be Approved or Rejected.");

            Id = Guid.NewGuid();
            AccessRequestId = accessRequestId;
            ApprovedBy = approvedBy;
            Status = status;
            Comment = comment;
            DecidedAt = DateTime.UtcNow;
        }
    }
}
