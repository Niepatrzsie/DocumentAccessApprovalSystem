using Document_Access_Approval_System.Application.Interfaces;
using Document_Access_Approval_System.Domain.Entities;
using Document_Access_Approval_System.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Document_Access_Approval_System.Infrastructure.Repositories
{
    public class AccessRequestRepository : IAccessRequestRepository
    {
        private readonly AppDbContext _context;

        public AccessRequestRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AccessRequest accessRequest)
        {
            await _context.AccessRequests.AddAsync(accessRequest);
            await _context.SaveChangesAsync();
        }

        public async Task<List<AccessRequest>> GetByUserIdAsync(Guid userId)
        {
            return await _context.AccessRequests
                .Include(ar => ar.Document)
                .Where(ar => ar.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<AccessRequest>> GetPendingAsync()
        {
            return await _context.AccessRequests
                .Include(ar => ar.Document)
                .Include(ar => ar.User)
                .Where(ar => ar.Status == RequestStatus.Pending)
                .ToListAsync();
        }

        public async Task<AccessRequest?> GetWithDetailsAsync(Guid requestId)
        {
            return await _context.AccessRequests
                .Include(ar => ar.Document)
                .Include(ar => ar.User)
                .Include(ar => ar.Decision)
                .FirstOrDefaultAsync(ar => ar.Id == requestId);
        }

        public async Task UpdateAsync(AccessRequest accessRequest)
        {
            if (accessRequest.Decision is not null)
            {
                _context.Decisions.Add(accessRequest.Decision);
            }

            await _context.SaveChangesAsync();
        }

    }
}
