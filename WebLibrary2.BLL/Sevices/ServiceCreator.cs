using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.BLL.Interfaces;
using WebLibrary2.Domain.Concrete.ConcreteIdentity;

namespace WebLibrary2.BLL.Sevices
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService(string conectionString)
        {
            return new UserService(new EFIdentityUnitOfWork(conectionString));
        }
    }
}
