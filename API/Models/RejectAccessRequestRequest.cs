namespace Document_Access_Approval_System.API.Models
{

    /// <summary>
    /// Data required to reject an access request.
    /// </summary>
    public class RejectAccessRequestRequest
    {
        public Guid ApproverId { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}
