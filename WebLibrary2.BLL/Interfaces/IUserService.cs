    using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.BLL.DTO;
using WebLibrary2.BLL.Infrastructure;

namespace WebLibrary2.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDTO);
        Task<ClaimsIdentity> Authentificate(UserDTO userDTO);
        Task SetInitialData(UserDTO adminDTO,List<string> roles);
    }
}
