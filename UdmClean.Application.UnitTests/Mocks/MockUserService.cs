using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdmClean.Application.Contracts.Identity;
using UdmClean.Application.Modules.Identity;

namespace UdmClean.Application.UnitTests.Mocks
{
    public static class MockUserService
    {
        public static Mock<IUserService> GetUserService()
        {
            var mockUserService = new Mock<IUserService>();

            var users = new List<Employee>()
            {
               new ()
               {
                    Id = "1",
                    Email = "user1@bla.com",
                    FristName = "user",
                    LastName = "one"
               },
                new()
                {
                    Id = "2",
                    Email = "user2@bla.com",
                    FristName = "user",
                    LastName = "two"
                }
            };

            mockUserService.Setup(q => q.GetEmployee(It.IsAny<string>())).ReturnsAsync(users[0]);

            mockUserService.Setup(q => q.GetEmployees()).ReturnsAsync(users);

            return mockUserService;
        }
    }
}
