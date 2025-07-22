namespace Document_Access_Approval_System.Application.AccessRequests.Queries.GetPendingAccessRequests
{
    /// <summary>
    /// DTO representing a pending access request for approver view.
    /// </summary>
    public class PendingAccessRequestDto
    {
        public Guid Id { get; set; }
        public string DocumentTitle { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public AccessType AccessType { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
