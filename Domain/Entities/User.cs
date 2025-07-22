using Document_Access_Approval_System.Domain.Enums;

namespace Document_Access_Approval_System.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;
        public UserRole Role { get; private set; }

        private User() { } // For EF Core

        public User(Guid id, string name, UserRole role)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));

            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            Name = name;
            Role = role;
        }
    }
}
