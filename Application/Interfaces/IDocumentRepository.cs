using Document_Access_Approval_System.Domain.Entities;

namespace Document_Access_Approval_System.Application.Interfaces
{
    public interface IDocumentRepository
    {
        Task<Document?> GetByIdAsync(Guid id);
    }
}
