using Microsoft.AspNet.Identity.EntityFramework;

namespace WebLibrary2.Domain.IdentityEntities
{
    public class ApplicationUser: IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }
    }
}
