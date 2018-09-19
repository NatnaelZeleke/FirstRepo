using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Infrastructure;
using Domain.Repository;
using Domain.Repository.Concrete;

namespace Domain.UnitOfWork.Concrete
{
    public class UnitOfWork:IUnitOfWork
    {

        private readonly AddisBidContext _context;
        private bool _disposed;
        private readonly Dictionary<Type, object> _repositories;

        public UnitOfWork()
        {
            _repositories = new Dictionary<Type, object>();
            _context = new AddisBidContext();
        }

        public UnitOfWork(AddisBidContext context)
        {
            _repositories = new Dictionary<Type, object>();
            _context = context;
        }

        public bool Save()
        {
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                return false;
            }
        }

        public IRepository<T> Repository<T>() where T : class
        {
            if (_repositories.ContainsKey(typeof(T)))
                return _repositories[typeof(T)] as IRepository<T>;
            var repo = BuildRepository<T>();
            return repo;
        }

        private IRepository<T> BuildRepository<T>() where T : class
        {
            var repositoryType = typeof(Repository<>);
            var repositoryInstance =
                Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context) as IRepository<T>;
            _repositories.Add(typeof(T), repositoryInstance);
            return repositoryInstance;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
