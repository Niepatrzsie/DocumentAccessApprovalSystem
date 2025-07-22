using Document_Access_Approval_System.Domain.Entities;

namespace Document_Access_Approval_System.Application.Interfaces
{
    public interface IAccessRequestRepository
    {
        Task AddAsync(AccessRequest accessRequest);

        Task<List<AccessRequest>> GetByUserIdAsync(Guid userId);

        Task<List<AccessRequest>> GetPendingAsync();

        Task<AccessRequest?> GetWithDetailsAsync(Guid requestId);

        Task UpdateAsync(AccessRequest accessRequest);

    }
}
