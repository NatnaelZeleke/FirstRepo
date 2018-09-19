using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Infrastructure;

namespace Domain.Services
{
    public interface IUserTenderTypeChoiceService
    {
        UserTenderTypeChoice GetUserTenderTypeChoiceById(long id);
        bool EditUserTenderTypeChoice(UserTenderTypeChoice userTenderTypeChoice);
        bool AddUserTenderTypeChoice(UserTenderTypeChoice userTenderTypeChoice);        
        bool SaveService();
    }
}
