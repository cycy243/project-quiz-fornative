using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.ForNative.Repository.Interfaces
{
    public interface IRepository<T, S> where S : class
    {
        bool Add(T entity);

        bool Delete(T entity);

        T Update(T entity);

        IEnumerable<T> GetAll();

        IEnumerable<T> Search(S args);
    }
}
