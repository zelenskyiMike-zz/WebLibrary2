using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.DataAccessLayer.Identity;

namespace WebLibrary2.DataAccessLayer.Interfaces
{
    public interface IClientManager : IDisposable
    {
        void Create(ClientProfile clientProfile);
    }
}
