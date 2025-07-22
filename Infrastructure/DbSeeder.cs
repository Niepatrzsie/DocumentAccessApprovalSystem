using Document_Access_Approval_System.Domain.Entities;
using Document_Access_Approval_System.Domain.Enums;

namespace Document_Access_Approval_System.Infrastructure
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (context.Users.Any() || context.Documents.Any())
            {
                return; //already added
            }

            var user1 = new User (Guid.NewGuid(), "Marian", UserRole.User);
            var user2 = new User(Guid.NewGuid(), "Marysia", UserRole.User);
            var user3 = new User(Guid.NewGuid(), "Michal", UserRole.User);
            var user4 = new User(Guid.NewGuid(), "Bob", UserRole.User);
            var user5 = new User(Guid.NewGuid(), "KonRafal", UserRole.User);
            var approver = new User(Guid.NewGuid(), "Manager Marek", UserRole.Approver);


            context.Users.AddRange(user1,user2,user3,user4,user5, approver);

            var document1 = new Document("Document 1.", "Content 1");
            var document2 = new Document("Document 2.", "Content 2");
            var document3 = new Document("Document 3.", "Content 3");

            context.Documents.AddRange(document1, document2, document3);

            // --- AccessRequests ---
            //3 people ask for document1
            var accessRequest1 = new AccessRequest(document1.Id, user1.Id, "I need to read this...", AccessType.Read);
            var accessRequest2 = new AccessRequest(document1.Id, user2.Id, "I need add holiday inside excel file", AccessType.Edit);
            var accessRequest3 = new AccessRequest(document1.Id, user3.Id, "Verification...", AccessType.Read);

            //2 people ask for document2
            var accessRequest4 = new AccessRequest(document2.Id, user4.Id, "Issues check...", AccessType.Edit);
            var accessRequest5 = new AccessRequest(document2.Id, user5.Id, "For presentation...", AccessType.Read);

            context.AccessRequests.AddRange(accessRequest1, accessRequest2, accessRequest3, accessRequest4, accessRequest5);

            context.SaveChanges();
        }
    }
}
