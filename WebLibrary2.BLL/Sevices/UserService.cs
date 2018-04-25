using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.BLL.DTO;
using WebLibrary2.BLL.Infrastructure;
using WebLibrary2.BLL.Interfaces;
using WebLibrary2.Domain.Abstract.AbstractIdentity;
using WebLibrary2.Domain.IdentityEntities;

namespace WebLibrary2.BLL.Sevices
{
    public class UserService : IUserService
    {
        IIdentityUnitOfWork Database { get; set; }
        public UserService(IIdentityUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        public async Task<ClaimsIdentity> Authentificate(UserDTO userDTO)
        {
            ClaimsIdentity claim = null;
            ApplicationUser applicationUser = await Database.UserManager.FindAsync(userDTO.Email, userDTO.Password);
            if (applicationUser != null)
            {
                claim = await Database.UserManager.CreateIdentityAsync(applicationUser, DefaultAuthenticationTypes.ApplicationCookie);
            }
            return claim;
        }

        public async Task<OperationDetails> Create(UserDTO userDTO)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDTO.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDTO.Email, UserName = userDTO.Name };
                var result = await Database.UserManager.CreateAsync(user,userDTO.Password);
                if (result.Errors.Count() > 0)
                {
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                }

                await Database.UserManager.AddToRoleAsync(user.Id,userDTO.Role);
                ClientProfile clientProfile = new ClientProfile { Id = user.Id, Address = userDTO.Address, UserName = userDTO.Name };
                Database.ClientManager.Create(clientProfile);
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public async Task SetInitialData(UserDTO adminDTO, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByIdAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDTO);
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
