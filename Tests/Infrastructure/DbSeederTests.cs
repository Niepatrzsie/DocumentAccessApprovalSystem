using Document_Access_Approval_System.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Tests.Infrastructure
{
    public class DbSeederTests
    {
        [Fact]
        public void Seed_ShouldAddUsersAndDocuments_WhenDatabaseIsEmpty()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new AppDbContext(options))
            {
                // Act
                DbSeeder.Seed(context);

                // Assert
                Assert.True(context.Users.Any());
                Assert.True(context.Documents.Any());
            }
        }
    }
}
