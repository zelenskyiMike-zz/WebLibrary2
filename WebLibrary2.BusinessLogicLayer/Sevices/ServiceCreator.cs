using WebLibrary2.BusinessLogicLayer.Interfaces;
using WebLibrary2.DataAccessLayer.Concrete;

namespace WebLibrary2.BusinessLogicLayer.Sevices
{
    public class ServiceCreator
    {
        public IUserService CreateUserService(string conectionString)
        {
            return new UserService(new IdentityUnitOfWork(conectionString));
        }
    }
}
