using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.ForNative.Repository.Interfaces
{
    public interface IRepository<T, S> where S : class
    {
        Task<bool> Add(T entity);

        Task<bool> Delete(T entity);

        Task<T> Update(T entity);

        Task<IEnumerable<T>> GetAll();

        Task<IEnumerable<T>> Search(S args);
    }
}
