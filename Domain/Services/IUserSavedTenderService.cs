using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Infrastructure;

namespace Domain.Services
{
    public interface IUserSavedTenderService
    {
        UserSavedTender GetUserSavedTenderById(long id);
        IEnumerable<UserSavedTender> GetAllUserSavedTender(long userId,int skip, int top);
        bool AddUserSavedTender(UserSavedTender userSavedTender);
        bool DeleteUserSavedTender(long userId, long tenderId);
        bool SaveService();
    }
}
