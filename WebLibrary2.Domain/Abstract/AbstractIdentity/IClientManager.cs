using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.Domain.IdentityEntities;

namespace WebLibrary2.Domain.Abstract.AbstractIdentity
{
    public interface IClientManager : IDisposable
    {
        void Create(ClientProfile clientProfile);
    }
}
