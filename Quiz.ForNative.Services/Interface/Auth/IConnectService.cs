using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.ForNative.Services.Interface.Auth
{
    public interface IConnectService<T>
    {
        Task<T?> ConnectWithCredentialsAsync(string login, string password);
    }
}
