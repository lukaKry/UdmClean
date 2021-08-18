using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdmClean.UI.Models;

namespace UdmClean.UI.Contracts
{
    public interface IAuthenticationService
    {
        Task<bool> Authenticate(string email, string password);
        Task<bool> Register(RegisterVM registration);
        Task Logout();
    }
}
