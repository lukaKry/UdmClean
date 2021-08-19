using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdmClean.Application.UnitTests.Mocks
{
    public static class MockHttpContextAccessor
    {
        public static Mock<IHttpContextAccessor> GetHttpContextAccessor()
        {
            var mockAccessor = new Mock<IHttpContextAccessor>();

            // setup...


            return mockAccessor;
        }
    }
}
