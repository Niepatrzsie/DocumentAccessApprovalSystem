using Document_Access_Approval_System.Domain.Entities;
using Document_Access_Approval_System.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Domain.Entities
{
    public class AccessRequestTests
    {
        [Fact]
        public void Approve_ShouldSetStatusToApproved_WhenRequestIsPending() 
        {
            var request = new AccessRequest(Guid.NewGuid(), Guid.NewGuid(), "Need edit", AccessType.Edit);
            request.Approve("OK", Guid.NewGuid());
            Assert.Equal(RequestStatus.Approved, request.Status);
        }

        [Fact]
        public void Reject_ShouldSetStatusToRejected_WhenRequestIsPending()
        {
            var request = new AccessRequest(Guid.NewGuid(), Guid.NewGuid(), "Need read", AccessType.Read);
            request.Reject("Nope", Guid.NewGuid());
            Assert.Equal(RequestStatus.Rejected, request.Status);
        }

        [Fact]
        public void Approve_ShouldThrow_WhenAlreadyDecided()
        {
            var request = new AccessRequest(Guid.NewGuid(), Guid.NewGuid(), "Already decided", AccessType.Read);
            request.Approve("Initial approval", Guid.NewGuid());

            Assert.Throws<InvalidOperationException>(() =>
                request.Approve("Second approval", Guid.NewGuid()));
        }

        [Fact]
        public void Reject_ShouldThrow_WhenAlreadyDecided()
        {
            var request = new AccessRequest(Guid.NewGuid(), Guid.NewGuid(), "Already decided", AccessType.Read);
            request.Reject("Initial rejection", Guid.NewGuid());

            Assert.Throws<InvalidOperationException>(() =>
                request.Reject("Second rejection", Guid.NewGuid()));
        }
    }
}
