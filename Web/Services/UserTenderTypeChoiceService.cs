using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Infrastructure;
using Domain.Repository;
using Domain.Services;
using Domain.UnitOfWork;

namespace Web.Services
{
    public class UserTenderTypeChoiceService:IUserTenderTypeChoiceService
    {
        private readonly IRepository<UserTenderTypeChoice> _userTenderTypeChoiceRepo;
        private readonly IUnitOfWork _unitOfWork;

        public UserTenderTypeChoiceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userTenderTypeChoiceRepo = _unitOfWork.Repository<UserTenderTypeChoice>();
        }
        public UserTenderTypeChoice GetUserTenderTypeChoiceById(long id)
        {
            return _userTenderTypeChoiceRepo.GetById(id);
        }

        public bool EditUserTenderTypeChoice(UserTenderTypeChoice userTenderTypeChoice)
        {
            var old = GetUserTenderTypeChoiceById(userTenderTypeChoice.Id);
            old.TenderTypeIds = userTenderTypeChoice.TenderTypeIds;
            return _userTenderTypeChoiceRepo.InsertOrUpdate(old);

        }

        public bool AddUserTenderTypeChoice(UserTenderTypeChoice userTenderTypeChoice)
        {
            return _userTenderTypeChoiceRepo.InsertOrUpdate(userTenderTypeChoice);
        }

        public bool SaveService()
        {
            return _unitOfWork.Save();
        }
    }
}