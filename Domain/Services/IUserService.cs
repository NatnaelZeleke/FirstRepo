using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Infrastructure;
using Domain.Utils;

namespace Domain.Services
{
    public interface IUserService
    {
        User GetUserById(long userId);
        User GetUserByUserName(string userName);
        IEnumerable<User> GetUsersByType(Enums.ProfileType profileType);
        User GetUserByPhoneNumber(string phoneNumber);
        bool EditPhoneNumber(long userId, string phoneNumber);
        User GetUserByEmailAddress(string email);
        bool DeleteUser(long userId);
        bool SaveService();
    }
}
