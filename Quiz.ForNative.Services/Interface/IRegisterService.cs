using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.ForNative.Services.Interface
{
    /// <summary>
    /// Service that help us to register user
    /// </summary>
    /// <typeparam name="T">Type that represents a user</typeparam>
    public interface RegisterService<T>
    {
        /// <summary>
        /// Register a user
        /// </summary>
        /// <param name="entity">User to register</param>
        /// <returns>
        /// Return the added user
        /// </returns>
        Task<T> RegisterUser(T entity);
    }
}
