using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdmClean.Application.Contracts.Persistance;
using UdmClean.Domain;

namespace UdmClean.Application.UnitTests.Mocks
{
    public class MockLeaveRequestRepository
    {
        public static Mock<ILeaveRequestRepository> GetLeaveRequestRepository()
        {
            var leaveRequests = new List<LeaveRequest>()
            {
                new LeaveRequest
                {
                    Id = 1,
                    StartDate = DateTime.Now - new TimeSpan (1,0,0,0,0),
                    EndDate = DateTime.Now + new TimeSpan (1,0,0,0,0),
                    DateRequested = DateTime.Now - new TimeSpan (2,0,0,0,0),
                    RequestComments = "leave request comment",
                    LeaveType = new LeaveType
                    {
                        Id = 1,
                        DefaultDays = 10,
                        Name = "Vacation"
                    },
                    LeaveTypeId = 1
                },
                new LeaveRequest
                {
                    Id = 1,
                    StartDate = DateTime.Now - new TimeSpan (1,0,0,0,0),
                    EndDate = DateTime.Now + new TimeSpan (1,0,0,0,0),
                    DateRequested = DateTime.Now - new TimeSpan (2,0,0,0,0),
                    RequestComments = "leave request comment",
                    LeaveType = new LeaveType
                    {
                        Id = 2,
                        DefaultDays = 12,
                        Name = "Sick"
                    },
                    LeaveTypeId = 2
                },
            };

            var mockRepo = new Mock<ILeaveRequestRepository>();

            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(leaveRequests);

            mockRepo.Setup(r => r.GetAsync(It.IsAny<int>())).ReturnsAsync((int x) => leaveRequests.Find(p => p.Id == x));

            mockRepo.Setup(r => r.AddAsync(It.IsAny<LeaveRequest>())).ReturnsAsync((LeaveRequest leaveRequest) =>
            {
                leaveRequests.Add(leaveRequest); return leaveRequest;
            });

            mockRepo.Setup(r => r.DeleteAsync(It.IsAny<LeaveRequest>())).Callback((LeaveRequest leaveRequest) => leaveRequests.Remove(leaveRequest));

            mockRepo.Setup(r => r.UpdateAsync(It.IsAny<LeaveRequest>())).Callback((LeaveRequest leaveRequest) =>
            {
                leaveRequests[leaveRequests.FindIndex(p => p.Id == leaveRequest.Id)] = leaveRequest;
            });

            mockRepo.Setup(r => r.GetLeaveRequestsWithDetailsAsync()).ReturnsAsync(leaveRequests);
            mockRepo.Setup(r => r.GetLeaveRequestWithDetailsAsync(It.IsAny<int>())).ReturnsAsync((int x) => leaveRequests.Find(p => p.Id == x));

            return mockRepo;
        }
    }
}
