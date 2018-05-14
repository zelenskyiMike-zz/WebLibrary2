using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.DataAccessLayer.Interfaces;
using WebLibrary2.DataAccessLayer.Identity;

namespace WebLibrary2.DataAccessLayer.Concrete
{
    public class ClientManager : IClientManager
    {
        public DbContext Database { get; set; }
        public ClientManager(DbContext dataBase)
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
