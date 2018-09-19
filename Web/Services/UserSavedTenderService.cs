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
    public class UserSavedTenderService:IUserSavedTenderService
    {
        private readonly IRepository<UserSavedTender> _userSavedTenderRepo;
        private readonly IUnitOfWork _unitOfWork;
        public UserSavedTenderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userSavedTenderRepo = _unitOfWork.Repository<UserSavedTender>();
        }
        public UserSavedTender GetUserSavedTenderById(long id)
        {
            return _userSavedTenderRepo.GetById(id);
        }

        public IEnumerable<UserSavedTender> GetAllUserSavedTender(long userId,int skip, int top)
        {
            return _userSavedTenderRepo.Select(ust => ust.UserId == userId).OrderByDescending(ust => ust.SavedTime).Skip(skip).Take(top); ;
        }

        public bool AddUserSavedTender(UserSavedTender userSavedTender)
        {
            if (!CheckUserSavedTender(userSavedTender.UserId, userSavedTender.TenderId))
            {
                userSavedTender.SavedTime = DateTime.Now;
                return _userSavedTenderRepo.Insert(userSavedTender);
            }
            return false;
        }

        public bool DeleteUserSavedTender(long userId, long tenderId)
        {
            return _userSavedTenderRepo.Delete(ust => ust.UserId == userId && ust.TenderId == tenderId);
        }

        private bool CheckUserSavedTender(long userId, long tenderId)
        {
            return _userSavedTenderRepo.Exists(ust => ust.UserId == userId && ust.TenderId == tenderId);
        }
        public bool SaveService()
        {
            return _unitOfWork.Save();
        }
    }
}