using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UdmClean.Application.Modules.Identity;

namespace UdmClean.Application.Contracts.Identity
{
    public interface IUserService
    {
        Task<List<Employee>> GetEmployees();
    }
}
