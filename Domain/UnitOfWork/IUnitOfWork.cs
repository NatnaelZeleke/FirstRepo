using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Repository;

namespace Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        bool Save();
        IRepository<T> Repository<T>() where T : class;
    }
}
