using Document_Access_Approval_System.Application.AccessRequests.Commands.CreateAccessRequest;
using Document_Access_Approval_System.Application.Interfaces;
using Document_Access_Approval_System.Domain.Entities;
using Document_Access_Approval_System.Domain.Enums;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Application.AccessRequests.Commands
{
    public class CreateAccessRequestHandlerTests
    {
        private readonly Mock<IAccessRequestRepository> _accessRequestRepoMock;
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly Mock<IDocumentRepository> _documentRepoMock;
        private readonly CreateAccessRequestHandler _handler;

        public CreateAccessRequestHandlerTests()
        {
            _accessRequestRepoMock = new Mock<IAccessRequestRepository>();
            _userRepoMock = new Mock<IUserRepository>();
            _documentRepoMock = new Mock<IDocumentRepository>();

            _handler = new CreateAccessRequestHandler(
                _userRepoMock.Object,
                _documentRepoMock.Object,
                _accessRequestRepoMock.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldCreateAccessRequest_WhenUserAndDocumentExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var documentId = Guid.NewGuid();
            var command = new CreateAccessRequestCommand(documentId, userId, "Need access", AccessType.Read);

            var user = new User(userId, "Test User", UserRole.User);
            var document = new Document("Test Doc", "Sample Content");

            // Ustawiamy ręcznie ID dokumentu, jeśli konstruktor nie pozwala
            typeof(Document)
                .GetProperty("Id")!
                .SetValue(document, documentId);

            AccessRequest? createdRequest = null;
            _accessRequestRepoMock
                .Setup(r => r.AddAsync(It.IsAny<AccessRequest>()))
                .Callback<AccessRequest>(ar => createdRequest = ar);

            _userRepoMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);
            _documentRepoMock.Setup(r => r.GetByIdAsync(documentId)).ReturnsAsync(document);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            result.Should().NotBeEmpty();
            createdRequest.Should().NotBeNull();
            createdRequest!.DocumentId.Should().Be(command.DocumentId);
            createdRequest.UserId.Should().Be(command.UserId);
            createdRequest.Reason.Should().Be(command.Reason);
            createdRequest.AccessType.Should().Be(command.AccessType);
        }
    }
}
