using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Infrastructure;
using Domain.Repository;
using Domain.Services;
using Domain.UnitOfWork;
using Domain.Utils;

namespace Web.Services
{
    public class AccountService:IAccountService
    {
        private readonly IRepository<Account> _accountRepo;
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _accountRepo = _unitOfWork.Repository<Account>();
        }
        public Account GetAccountById(long id)
        {
            var account = _accountRepo.GetById(id);
            if (account == null)
            {
                CreateAccount(id);
                SaveService();
                return _accountRepo.GetById(id);
            }
            return account;
        }

        public bool EditAccount(Account account)
        {
            var old = GetAccountById(account.Id);
            old.AccountExpirationDate = account.AccountExpirationDate;
            return _accountRepo.InsertOrUpdate(account);
        }

        public bool CreateAccount(long id)
        {
            return _accountRepo.Insert(new Account { Id = id,RegisteredOn = DateTime.Now,AccountExpirationDate = DateTime.Now});
             
        }

         

        public bool RenewAccount(long id, Enums.RenewalTimeSpan timeSpan)
        {
            var old = GetAccountById(id);
            if (timeSpan == Enums.RenewalTimeSpan.SixMonth)
            {
                //if the user account is not expired add time to it
                old.AccountExpirationDate = old.AccountExpirationDate > DateTime.Now
                    ? old.AccountExpirationDate.AddMonths(6)
                    : DateTime.Now.AddMonths(6);
            }
            else
            {
                old.AccountExpirationDate = old.AccountExpirationDate > DateTime.Now ? old.AccountExpirationDate.AddYears(1) : DateTime.Now.AddYears(1);
            }
            
            return _accountRepo.InsertOrUpdate(old);
        }

        public bool DeRenewAccount(long id)
        {
            var old = GetAccountById(id);
            old.AccountExpirationDate = old.AccountExpirationDate.AddYears(-1);
            return _accountRepo.InsertOrUpdate(old);
        }

        public bool EditTenderTypeIdForEmail(long userId,long tenderTypeId)
        {
            long? ttId = tenderTypeId == 0 ?   (long?) null : tenderTypeId;
            var oldAccount = GetAccountById(userId);
            oldAccount.TenderTypeIdForEmail = ttId;
            return _accountRepo.InsertOrUpdate(oldAccount);
        }

        public IEnumerable<Account> GetAccountsByTenderTypeIdForEmail(long tenderTypeIdForEmail)
        {
            return _accountRepo.Select(a => a.TenderTypeIdForEmail == tenderTypeIdForEmail);
        }

        public bool AddAccount(Account account)
        {
            return _accountRepo.Insert(account);
        }

        public bool SaveService()
        {
            return _unitOfWork.Save();
        }
    }
}