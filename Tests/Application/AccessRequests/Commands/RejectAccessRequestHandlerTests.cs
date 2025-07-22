using Document_Access_Approval_System.Application.AccessRequests.Commands.RejectAccessRequest;
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
    public class RejectAccessRequestHandlerTests
    {
        private readonly Mock<IAccessRequestRepository> _accessRequestRepoMock;
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly RejectAccessRequestHandler _handler;

        public RejectAccessRequestHandlerTests()
        {
            _accessRequestRepoMock = new Mock<IAccessRequestRepository>();
            _userRepoMock = new Mock<IUserRepository>();
            _handler = new RejectAccessRequestHandler(_accessRequestRepoMock.Object, _userRepoMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldRejectRequest_WhenRequestAndApproverExist()
        {
            // Arrange
            var requestId = Guid.NewGuid();
            var approverId = Guid.NewGuid();
            var comment = "Not acceptable.";

            var request = new AccessRequest(Guid.NewGuid(), Guid.NewGuid(), "For edit", AccessType.Edit);
            var approver = new User(approverId, "Approver", UserRole.Approver);

            _accessRequestRepoMock
                .Setup(r => r.GetWithDetailsAsync(requestId))
                .ReturnsAsync(request);

            _userRepoMock
                .Setup(r => r.GetByIdAsync(approverId))
                .ReturnsAsync(approver);

            _accessRequestRepoMock
                .Setup(r => r.UpdateAsync(It.IsAny<AccessRequest>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var command = new RejectAccessRequestCommand(requestId, approverId, comment);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            request.Status.Should().Be(RequestStatus.Rejected);
            request.Decision.Should().NotBeNull();
            request.Decision!.Status.Should().Be(RequestStatus.Rejected);
            request.Decision.Comment.Should().Be(comment);
            request.Decision.ApprovedBy.Should().Be(approverId);

            _accessRequestRepoMock.Verify(r => r.UpdateAsync(request), Times.Once);
        }
    }
}
