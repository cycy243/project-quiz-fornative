using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.ForNative.Repository.Interfaces
{
    public interface IAuthRepository<T>
    {
        /// <summary>
        /// Register a user and return it
        /// </summary>
        /// <param name="user">User to register</param>
        /// <returns>
        /// The registered user
        /// </returns>
        Task<T> RegisterUser(T user);
    }
}
