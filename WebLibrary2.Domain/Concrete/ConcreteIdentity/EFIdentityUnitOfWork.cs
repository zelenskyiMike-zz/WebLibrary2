﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.Domain.Abstract.AbstractIdentity;
using WebLibrary2.Domain.Identity;
using WebLibrary2.Domain.IdentityEntities;

namespace WebLibrary2.Domain.Concrete.ConcreteIdentity
{
    public class EFIdentityUnitOfWork : IIdentityUnitOfWork
    {
        private EFDbContext db;

        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private IClientManager clientManager;

        public EFIdentityUnitOfWork(string connectionString)
        {
            db = new EFDbContext(connectionString);
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
            clientManager = new ClientManager(db);
        }

        public ApplicationUserManager UserManager
        {
            get { return userManager; }
        }

        public IClientManager ClientManager
        {
            get { return clientManager; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return roleManager; }
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userManager.Dispose();
                    roleManager.Dispose();
                    clientManager.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
