using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdmClean.Application.Contracts.Identity;
using UdmClean.Application.Modules.Identity;
using UdmClean.Identity.Models;

namespace UdmClean.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Employee> GetEmployee(string userId)
        {
            var employee = await _userManager.FindByIdAsync(userId);
            return new Employee
            {
                Email = employee.Email,
                Id = employee.Id,
                FristName = employee.FirstName,
                LastName = employee.LastName
            };
        }

        public async Task<List<Employee>> GetEmployees()
        {
            var employees = await _userManager.GetUsersInRoleAsync("Employee");
            return employees.Select(q => new Employee
            {
                Id = q.Id,
                Email = q.Email,
                FristName = q.FirstName,
                LastName = q.LastName
            }).ToList();
        }
    }
}
