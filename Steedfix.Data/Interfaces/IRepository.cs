using System;
using System.Linq;
using System.Linq.Expressions;

namespace Steedfix.Data.Interfaces
{
    public interface IRepository<T>: IDisposable
    {
        IQueryable<T> All { get; }
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        T Find(Guid id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(Guid id);
        void Save();
    }
}
