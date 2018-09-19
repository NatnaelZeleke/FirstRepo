using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Infrastructure;

namespace Domain.Services
{
    public interface IUsersBySalesService
    {
        IEnumerable<UsersBySale> GetUserBySalesId(long id);
        bool AddUserBySales(UsersBySale userBySale);
        bool EditUserBySales(UsersBySale userBySale);
        bool DeleteUserBySales(long id);
        bool SaveService();
    }
}
