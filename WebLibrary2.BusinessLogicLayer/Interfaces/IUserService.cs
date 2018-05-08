using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.BusinessLogicLayer.DTO;
using WebLibrary2.BusinessLogicLayer.Infrastructure;

namespace WebLibrary2.BusinessLogicLayer.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDTO);
        Task<ClaimsIdentity> Authenticate(UserDTO userDTO);
        Task SetInitialData(UserDTO adminDTO,List<string> roles);
    }
}
