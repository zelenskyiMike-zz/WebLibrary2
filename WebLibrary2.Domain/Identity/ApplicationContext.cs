using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.Domain.IdentityEntities;

namespace WebLibrary2.Domain.Identity
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(string connectionString) : base(connectionString){}
        public DbSet<ClientProfile> ClientProfiles { get; set; }
    }
}
