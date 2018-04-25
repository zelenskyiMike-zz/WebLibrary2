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
    public class ClientManager : IClientManager
    {
        public ApplicationContext Database { get; set; }
        public ClientManager(ApplicationContext dataBase)
        {
            Database = dataBase;
        }
        public void Create(ClientProfile clientProfile)
        {
            Database.ClientProfiles.Add(clientProfile);
            Database.SaveChanges();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
