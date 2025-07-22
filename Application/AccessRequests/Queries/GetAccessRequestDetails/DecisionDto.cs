using Document_Access_Approval_System.Domain.Enums;

namespace Document_Access_Approval_System.Application.AccessRequests.Queries.GetAccessRequestDetails
{
    public class DecisionDto
    {
        public string ApprovedBy { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public RequestStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
