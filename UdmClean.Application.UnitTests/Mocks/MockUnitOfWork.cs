using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdmClean.Application.Contracts.Persistence;

namespace UdmClean.Application.UnitTests.Mocks
{
    public static class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GetUnitOfWork()
        {
            var mockUow = new Mock<IUnitOfWork>();

            var mockLeaveTypeRepo = MockLeaveTypeRepository.GetLeaveTypeRepository();
            var mockLeaveRequestRepo = MockLeaveRequestRepository.GetLeaveRequestRepository();
            var mockLeaveAllocationRepo = MockLeaveAllocationRepository.GetLeaveAllocationRepository();

            mockUow.Setup(r => r.LeaveTypeRepository).Returns(mockLeaveTypeRepo.Object);
            mockUow.Setup(r => r.LeaveRequestRepository).Returns(mockLeaveRequestRepo.Object);
            mockUow.Setup(r => r.LeaveAllocationRepository).Returns(mockLeaveAllocationRepo.Object);

            return mockUow;
        }
    }
}
