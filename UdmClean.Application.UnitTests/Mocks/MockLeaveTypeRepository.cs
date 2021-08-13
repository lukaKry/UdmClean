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
    public static class MockLeaveTypeRepository
    {
        public static Mock<ILeaveTypeRepository> GetLeaveTypeRepository()
        {
            var leaveTypes = new List<LeaveType>()
            {
                new LeaveType
                {
                    Id = 1,
                    DefaultDays = 10,
                    Name = "Vacation"
                },
                new LeaveType
                {
                    Id = 2,
                    DefaultDays = 12,
                    Name = "Sick"
                }
            };

            var mockRepo = new Mock<ILeaveTypeRepository>();

            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(leaveTypes);

            mockRepo.Setup(r => r.GetAsync(It.IsAny<int>())).ReturnsAsync((int x) => leaveTypes.Find( p => p.Id == x));

            mockRepo.Setup(r => r.AddAsync(It.IsAny<LeaveType>())).ReturnsAsync((LeaveType leaveType) => 
            { 
                leaveTypes.Add(leaveType); return leaveType; 
            });

            mockRepo.Setup(r => r.DeleteAsync(It.IsAny<LeaveType>())).Callback((LeaveType leaveType) => leaveTypes.Remove(leaveType));

            mockRepo.Setup(r => r.UpdateAsync(It.IsAny<LeaveType>())).Callback((LeaveType leaveType) => 
            {
                leaveTypes[leaveTypes.FindIndex(p => p.Id == leaveType.Id)] = leaveType;
            });

            return mockRepo;
        }
    }
}
