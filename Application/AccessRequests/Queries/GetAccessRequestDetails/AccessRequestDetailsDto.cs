using Document_Access_Approval_System.Domain.Enums;

namespace Document_Access_Approval_System.Application.AccessRequests.Queries.GetAccessRequestDetails
{
    public class AccessRequestDetailsDto
    {
        public Guid Id { get; set; }
        public string DocumentTitle { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public AccessType AccessType { get; set; }
        public RequestStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public string RequestedBy { get; set; } = string.Empty;

        public DecisionDto? Decision { get; set; }
    }
}
