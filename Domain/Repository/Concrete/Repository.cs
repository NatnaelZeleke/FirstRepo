using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Infrastructure;

namespace Domain.Repository.Concrete
{
    public class Repository<T>:IRepository<T> where T: class
    {

        private readonly DbSet<T> _dbSet;
        private readonly AddisBidContext _context;

        public Repository(AddisBidContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public long Count(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null) return 0;
            return _dbSet.Count(predicate);
        }

        #region Delete repository

        public bool Delete(IEnumerable<T> entities)
        {
            return entities != null && entities.All(Delete);
        }

        public bool Delete(T entities)
        {
            if (entities == null) return false;
            if (_context.Entry(entities).State == EntityState.Detached)
            {
                _dbSet.Attach(entities);
            }
            _dbSet.Remove(entities);
            return true;
        }

        public bool DeleteById(object id)
        {
            if (id == null) return false;
            var entity = _dbSet.Find(id);
            return Delete(entity);
        }

        public bool Delete(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null) return false;
            var entity = SelectSingle(predicate);
            return Delete(entity);
        }

        #endregion Delete repository
        public void Update(T entity)
        {
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
        }

        public bool Exists(Expression<Func<T, bool>> predicate)
        {
            return predicate != null && _dbSet.Any(predicate);
        }

        #region Get repositories
        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(object id)
        {
            return id == null ? null : _dbSet.Find(id);
        }

        public bool Insert(T entity)
        {
            if (entity == null) return false;
            try
            {
                _dbSet.Add(entity);
                return true;
            }
            catch (DbEntityValidationException)
            {
                return false;
            }
        }
        #endregion Get repositories

        public bool InsertOrUpdate(T entity)
        {
            if (entity == null) return false;
            try
            {
                _dbSet.AddOrUpdate(entity);
                return true;
            }
            catch (DbEntityValidationException)
            {
                return false;
            }
        }

        public bool InsertOrUpdate(IEnumerable<T> entities)
        {
            return entities != null && entities.All(InsertOrUpdate);
        }

        public T SelectSingle(Expression<Func<T, bool>> predicate)
        {
            return predicate == null ? null : _dbSet.Where(predicate).FirstOrDefault();
        }

        public IEnumerable<T> Select(Expression<Func<T, bool>> predicate)
        {
            return predicate == null ? null : _dbSet.Where(predicate).AsEnumerable();
        }

        public void Detach(T entity)
        {
            if (entity != null) _context.Entry(entity).State = EntityState.Deleted;
        }
    }
}
