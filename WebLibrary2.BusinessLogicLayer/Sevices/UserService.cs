using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebLibrary2.BusinessLogicLayer.DTO;
using WebLibrary2.BusinessLogicLayer.Infrastructure;
using WebLibrary2.BusinessLogicLayer.Interfaces;
using WebLibrary2.DataAccessLayer.Identity;
using WebLibrary2.DataAccessLayer.Interfaces;

namespace WebLibrary2.BusinessLogicLayer.Sevices
{
    public class UserService : IUserService
    {
        IIdentityUnitOfWork Database { get; set; }

        public UserService(IIdentityUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> Create(UserView userDto)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user != null)
            {
                return new OperationDetails(false, "User with the same login is already exists", "Email");
            }
            user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
            var result = await Database.UserManager.CreateAsync(user, userDto.Password);
            if (result.Errors.Count() > 0)
                return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

            await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);

            ClientProfile clientProfile = new ClientProfile { Id = user.Id, Address = userDto.Address, Name = userDto.Name };
            Database.ClientManager.Create(clientProfile);
            await Database.SaveAsync();
            return new OperationDetails(true, "Registration is succes", "");
        }

        public async Task<ClaimsIdentity> Authenticate(UserView userDto)
        {
            ClaimsIdentity claim = null;
            ApplicationUser user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);

            if (user != null)
            {
                claim = await Database.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }
            return claim;
        }

        public async Task SetInitialData(UserView adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
