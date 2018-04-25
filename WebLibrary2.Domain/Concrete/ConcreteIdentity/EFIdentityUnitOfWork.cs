using Microsoft.AspNet.Identity.EntityFramework;
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
        private ApplicationContext dataBase;
        private bool disposed = false;

        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private IClientManager clientManager;

        public EFIdentityUnitOfWork(string connectionString)
        {
            dataBase = new ApplicationContext(connectionString);
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(dataBase));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(dataBase));
            clientManager = new ClientManager(dataBase);
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

        public void Dispose()
        {
            Dispose(true);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    roleManager.Dispose();
                    userManager.Dispose();
                    clientManager.Dispose();
                }
                this.disposed = true;
            }
        }
        public async Task SaveAsync()
        {
            await dataBase.SaveChangesAsync();
        }
    }
}
