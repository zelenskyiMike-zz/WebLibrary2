﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.Domain.Concrete;
using WebLibrary2.Domain.IdentityEntities;

namespace WebLibrary2.Domain.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store){  }

        //public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        //{
        //    var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<EFDbContext>()));
        //    return manager;
        //}

        //public Task<ClaimsIdentity> Authenticate(WebLibrary2.BLL.DTO.UserDTO userDTO)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
