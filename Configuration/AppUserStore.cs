
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
        private readonly ApiContext context;
        private readonly ILogger logger;

        public AppUserStore(ApiContext context, ILogger<AppUserStore> logger) {
            this.context = context;
            this.logger = logger;
        }

        public Task AddToRoleAsync(Employee user, string roleName, CancellationToken cancellationToken)
        {
            // WE can assing only single role :D 
            // user.Department = DepartmentId.Admin;
            // throw new System.NotImplementedException();
            DepartmentId departmentId = (DepartmentId)Enum.Parse(typeof(DepartmentId), roleName, true);
            user.Department = departmentId;
            context.Employees.Update(user);
            context.SaveChanges();
            
            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> CreateAsync(Employee user, CancellationToken cancellationToken)
        {
            logger.LogInformation("Create user called: {}, {}", user.Username, user.Email);
            context.Employees.Add(new Employee{
                Username = user.Username,
                Email = user.Email,
                RegisterDate = new System.DateTime(), /// CIA
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PersonalCode = user.PersonalCode,
                Department = user.Department
            });
            context.SaveChanges();
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
            return Task.FromResult(context.Employees.Where(a => a.Email == normalizedEmail).FirstOrDefault<Employee>());
        }

        public Task<Employee> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return Task.FromResult(context.Employees.Where(a => a.Id.ToString() == userId).FirstOrDefault<Employee>());
        }

        public Task<Employee> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            logger.LogWarning("FindByNameAsync: {0} -> {}", normalizedUserName, context.Employees.ToList());
            return Task.FromResult(context.Employees.Where(a => a.Username == normalizedUserName).FirstOrDefault<Employee>());
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
            logger.LogWarning("They asked me for roles: {} => {}", user.Username, user.Department.ToString());
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
            logger.LogInformation("I got question who belongs to role: {}", roleName);
            IList<Employee> list = context.Employees.Where(x => x.Department.ToString() == roleName).ToList();
            return Task.FromResult(list);
        }

        public Task<bool> HasPasswordAsync(Employee user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Password != null);
        }

        public Task<bool> IsInRoleAsync(Employee user, string roleName, CancellationToken cancellationToken)
        {
            logger.LogWarning("they asked if user: {} belongs to {} ==> {}", user.Username, roleName, user.Department.ToString() == roleName);
            return Task.FromResult(user.Department.ToString() == roleName);
        }

        public Task RemoveFromRoleAsync(Employee user, string roleName, CancellationToken cancellationToken)
        {
            // Well we can't do that;
            throw new System.NotImplementedException();
        }

        public Task SetEmailAsync(Employee user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.FromResult(IdentityResult.Success);
        }

        public Task SetEmailConfirmedAsync(Employee user, bool confirmed, CancellationToken cancellationToken)
        {
            // no code here
            return Task.FromResult(IdentityResult.Success);
        }

        public Task SetNormalizedEmailAsync(Employee user, string normalizedEmail, CancellationToken cancellationToken)
        {
            // i do nothing here
            return Task.FromResult(IdentityResult.Success);
        }

        public Task SetNormalizedUserNameAsync(Employee user, string normalizedName, CancellationToken cancellationToken)
        {
            // yes, i do nothing here;
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