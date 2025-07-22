namespace Document_Access_Approval_System.Domain.Entities
{
    public class Document
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; } = null!;
        public string Content { get; private set; } = null!;

        public ICollection<AccessRequest>? AccessRequests { get; private set; }


        private Document() { } // For EF Core

        public Document(string title, string content)
        {
            Id = Guid.NewGuid();
            Title = title;
            Content = content;
        }
    }
}
