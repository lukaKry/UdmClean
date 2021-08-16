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
    public class MockLeaveAllocationRepository
    {
        public static Mock<ILeaveAllocationRepository> GetLeaveAllocationRepository()
        {
            var leaveAllocations = new List<LeaveAllocation>()
            {
                new LeaveAllocation
                {
                    Id = 1,
                    Period = 2,
                    LeaveType = new LeaveType
                    {
                        Id = 1,
                        DefaultDays = 10,
                        Name = "Vacation"
                    },
                    LeaveTypeId = 1
                },
                new LeaveAllocation
                {
                    Id = 2,
                    Period = 3,
                    LeaveType = new LeaveType
                    {
                        Id = 2,
                        DefaultDays = 12,
                        Name = "Sick"
                    },
                    LeaveTypeId = 2
                },
            };

            var mockRepo = new Mock<ILeaveAllocationRepository>();

            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(leaveAllocations);

            mockRepo.Setup(r => r.GetAsync(It.IsAny<int>())).ReturnsAsync((int x) => leaveAllocations.Find(p => p.Id == x));

            mockRepo.Setup(r => r.AddAsync(It.IsAny<LeaveAllocation>())).ReturnsAsync((LeaveAllocation leaveAllocation) =>
            {
                leaveAllocations.Add(leaveAllocation); return leaveAllocation;
            });

            mockRepo.Setup(r => r.DeleteAsync(It.IsAny<LeaveAllocation>())).Callback((LeaveAllocation leaveAllocation) => leaveAllocations.Remove(leaveAllocation));

            mockRepo.Setup(r => r.UpdateAsync(It.IsAny<LeaveAllocation>())).Callback((LeaveAllocation leaveAllocation) =>
            {
                leaveAllocations[leaveAllocations.FindIndex(p => p.Id == leaveAllocation.Id)] = leaveAllocation;
            });

            return mockRepo;
        }
    }
}
