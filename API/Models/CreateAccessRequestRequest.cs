using System.ComponentModel.DataAnnotations;
using Document_Access_Approval_System.Domain.Enums;

namespace Document_Access_Approval_System.API.Models;

/// <summary>
/// Represents the data required to create an access request for a document.
/// </summary>
public class CreateAccessRequestRequest
{
    /// <summary>
    /// ID of the document the user wants access to.
    /// </summary>
    [Required]
    public Guid DocumentId { get; set; }

    /// <summary>
    /// ID of the user submitting the request.
    /// </summary>
    [Required]
    public Guid UserId { get; set; }

    /// <summary>
    /// Reason for requesting access.
    /// </summary>
    [Required]
    [MaxLength(300)]
    public string Reason { get; set; } = string.Empty;

    /// <summary>
    /// Type of access requested (Read or Edit).
    /// </summary>
    [Required]
    public AccessType AccessType { get; set; }
}
