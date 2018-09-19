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
    public class UserService:IUserService
    {
        private readonly IRepository<User> _userRepo;
        private readonly IUnitOfWork _unitOfWork;


        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepo = _unitOfWork.Repository<User>();


        }
        public User GetUserById(long userId)
        {
            return _userRepo.GetById(userId);
        }

        public User GetUserByUserName(string userName)
        {
            return _userRepo.SelectSingle(u => u.UserName == userName);
        }

        public IEnumerable<User> GetUsersByType(Enums.ProfileType profileType)
        {
            return _userRepo.Select(u => u.Roles.Select(role => role.Name).Contains(profileType.ToString()));
        }

        public User GetUserByPhoneNumber(string phoneNumber)
        {
            return _userRepo.SelectSingle(u => u.PhoneNumber == phoneNumber);
        }

        public bool EditPhoneNumber(long userId, string phoneNumber)
        {
            var oldUser = _userRepo.GetById(userId);
            oldUser.PhoneNumber = phoneNumber;
            return _userRepo.InsertOrUpdate(oldUser);
        }

        public User GetUserByEmailAddress(string email)
        {
            return _userRepo.SelectSingle(u => u.Email == email);
        }

        public bool DeleteUser(long userId)
        {
            return _userRepo.DeleteById(userId);
        }



        public bool SaveService()
        {
            return _unitOfWork.Save();
        }
    }
}