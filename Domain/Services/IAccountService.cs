using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Infrastructure;
using Domain.Utils;

namespace Domain.Services
{
    public interface IAccountService
    {
        Account GetAccountById(long id);
        bool EditAccount(Account account);
        bool AddAccount(Account account);
        bool CreateAccount(long id);
        bool RenewAccount(long id,Enums.RenewalTimeSpan timeSpan);
        bool DeRenewAccount(long id);
        bool EditTenderTypeIdForEmail(long userId,long id);
        IEnumerable<Account> GetAccountsByTenderTypeIdForEmail(long tenderTypeIdForEmail);
        bool SaveService();
    }
}
