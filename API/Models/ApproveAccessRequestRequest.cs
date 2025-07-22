namespace Document_Access_Approval_System.API.Models
{
    /// <summary>
    /// Data required to approve an access request.
    /// </summary>
    public class ApproveAccessRequestRequest
    {
        public Guid ApproverId { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}
