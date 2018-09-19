using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IRepository<T> where T:class
    {
        long Count(Expression<Func<T, bool>> predicate);
        bool Delete(IEnumerable<T> entities);
        bool Delete(T entities);
        bool DeleteById(object id);
        bool Delete(Expression<Func<T, bool>> predicate);
        void Update(T entity);
        bool Exists(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        T GetById(object id);
        bool Insert(T entity);
        bool InsertOrUpdate(T entity);
        bool InsertOrUpdate(IEnumerable<T> entities);
        T SelectSingle(Expression<Func<T, bool>> predicate);
        IEnumerable<T> Select(Expression<Func<T, bool>> predicate);
        void Detach(T entity);
    }
}
