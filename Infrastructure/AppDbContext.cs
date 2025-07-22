using Document_Access_Approval_System.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Document_Access_Approval_System.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Document> Documents => Set<Document>();
        public DbSet<AccessRequest> AccessRequests => Set<AccessRequest>();
        public DbSet<Decision> Decisions => Set<Decision>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // AccessRequest -> Decision (1:1)
            modelBuilder.Entity<AccessRequest>()
                .HasOne(ar => ar.Decision)
                .WithOne(d => d.AccessRequest)
                .HasForeignKey<Decision>(d => d.AccessRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            // AccessRequest -> User (many-to-one)
            modelBuilder.Entity<AccessRequest>()
                .HasOne(ar => ar.User)
                .WithMany()
                .HasForeignKey(ar => ar.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // AccessRequest -> Document (many-to-one)
            modelBuilder.Entity<AccessRequest>()
                .HasOne(ar => ar.Document)
                .WithMany()
                .HasForeignKey(ar => ar.DocumentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Document>()
                .HasMany(d => d.AccessRequests)
                .WithOne(ar => ar.Document)
                .HasForeignKey(ar => ar.DocumentId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
