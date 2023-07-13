using Cinema.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CinemaNS.Abstract
{
    public interface IRepository<T> where T : IEntity
    {
        void Create(T entity);
        T Read(long Id);
        IEnumerable<T> Read(Expression<Func<T, bool>> predicate);
        void Delete(long? Id);
        void Update(T entity);
        IEnumerable<T> ReadAll();
    }
}
