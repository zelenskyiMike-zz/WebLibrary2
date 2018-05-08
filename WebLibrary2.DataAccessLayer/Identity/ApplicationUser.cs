using Microsoft.AspNet.Identity.EntityFramework;

namespace WebLibrary2.DataAccessLayer.Identity
{
    public class ApplicationUser: IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }
    }
}
