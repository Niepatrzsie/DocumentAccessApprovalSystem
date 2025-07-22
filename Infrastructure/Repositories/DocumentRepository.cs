using Document_Access_Approval_System.Application.Interfaces;
using Document_Access_Approval_System.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Document_Access_Approval_System.Infrastructure.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly AppDbContext _context;

        public DocumentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Document?> GetByIdAsync(Guid id)
        {
            return await _context.Documents.FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
