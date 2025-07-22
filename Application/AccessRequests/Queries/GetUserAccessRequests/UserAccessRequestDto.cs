namespace Document_Access_Approval_System.Application.AccessRequests.Queries.GetUserAccessRequests
{

    /// <summary>
    /// DTO representing a user's access request for display purposes.
    /// </summary>
    public class UserAccessRequestDto
    {
        public Guid Id { get; set; }
        public string DocumentTitle { get; set; } = string.Empty;
        public AccessType AccessType { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // Pending, Approved, Rejected
        public DateTime CreatedAt { get; set; }
    }

}
