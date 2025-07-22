using Document_Access_Approval_System.Application.Interfaces;
using Document_Access_Approval_System.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Document_Access_Approval_System.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
