using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace API.Configuration
{
    public class AppUserStore : IUserStore<Employee>, IUserPasswordStore<Employee>, IUserEmailStore<Employee>, IUserRoleStore<Employee>
    {
        private readonly ApiContext _context;
        private readonly ILogger _logger;

        public AppUserStore(ApiContext context, ILogger<AppUserStore> logger) {
            _context = context;
            _logger = logger;
        }

        public Task AddToRoleAsync(Employee user, string roleName, CancellationToken cancellationToken)
        {
            DepartmentId departmentId = (DepartmentId)Enum.Parse(typeof(DepartmentId), roleName, true);
            user.Department = departmentId;
            _context.Employees.Update(user);
            _context.SaveChanges();
            
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> CreateAsync(Employee user, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Create user called: {}, {}", user.Username, user.Email);
            _context.Employees.Add(new Employee{
                Username = user.Username,
                Email = user.Email,
                RegisterDate = new System.DateTime(),
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PersonalCode = user.PersonalCode,
                Department = user.Department,
                Status = user.Status
            });
            _context.SaveChanges();
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(Employee user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {

        }

        public Task<Employee> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            return Task.FromResult(_context.Employees.FirstOrDefault(a => a.Email == normalizedEmail));
        }

        public Task<Employee> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return Task.FromResult(_context.Employees.FirstOrDefault(a => a.Id.ToString() == userId));
        }

        public Task<Employee> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            _logger.LogWarning("FindByNameAsync: {0} -> {}", normalizedUserName, _context.Employees.ToList());
            return Task.FromResult(_context.Employees.FirstOrDefault(a => a.Username == normalizedUserName));
        }

        public Task<string> GetEmailAsync(Employee user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(Employee user, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        public Task<string> GetNormalizedEmailAsync(Employee user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<string> GetNormalizedUserNameAsync(Employee user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Username);
        }

        public Task<string> GetPasswordHashAsync(Employee user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Password);
        }

        public Task<IList<string>> GetRolesAsync(Employee user, CancellationToken cancellationToken)
        {
            _logger.LogWarning("They asked me for roles: {} => {}", user.Username, user.Department.ToString());
            IList<string> roles = new List<string>() { user.Department.ToString() };
            return Task.FromResult(roles);
        }

        public Task<string> GetUserIdAsync(Employee user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(Employee user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Username);
        }

        public Task<IList<Employee>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            _logger.LogInformation("I got question who belongs to role: {}", roleName);
            IList<Employee> list = _context.Employees.Where(x => x.Department.ToString() == roleName).ToList();
            return Task.FromResult(list);
        }

        public Task<bool> HasPasswordAsync(Employee user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Password != null);
        }

        public Task<bool> IsInRoleAsync(Employee user, string roleName, CancellationToken cancellationToken)
        {
            _logger.LogWarning("they asked if user: {} belongs to {} ==> {}", user.Username, roleName, user.Department.ToString() == roleName);
            return Task.FromResult(user.Department.ToString() == roleName);
        }

        public Task RemoveFromRoleAsync(Employee user, string roleName, CancellationToken cancellationToken)
        {
            user.Department = DepartmentId.None;
            _context.Employees.Update(user);
            _context.SaveChanges();

            return Task.FromResult(IdentityResult.Success);
        }

        public Task SetEmailAsync(Employee user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.FromResult(IdentityResult.Success);
        }

        public Task SetEmailConfirmedAsync(Employee user, bool confirmed, CancellationToken cancellationToken)
        {
            return Task.FromResult(IdentityResult.Success);
        }

        public Task SetNormalizedEmailAsync(Employee user, string normalizedEmail, CancellationToken cancellationToken)
        {
            return Task.FromResult(IdentityResult.Success);
        }

        public Task SetNormalizedUserNameAsync(Employee user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.FromResult(IdentityResult.Success);
        }

        public Task SetPasswordHashAsync(Employee user, string passwordHash, CancellationToken cancellationToken)
        {
            user.Password = passwordHash;
            return Task.FromResult(IdentityResult.Success);
        }

        public Task SetUserNameAsync(Employee user, string userName, CancellationToken cancellationToken)
        {
            user.Username = userName;
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> UpdateAsync(Employee user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}