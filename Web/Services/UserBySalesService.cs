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
    public class UserBySalesService:IUsersBySalesService
    {
        private readonly IRepository<UsersBySale> _userBySalesRepo;
        private readonly IUnitOfWork _unitOfWork;

        public UserBySalesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userBySalesRepo = _unitOfWork.Repository<UsersBySale>();

        }
        public IEnumerable<UsersBySale> GetUserBySalesId(long id)
        {
            return _userBySalesRepo.Select(us => us.UserId == id);
        }

        public bool AddUserBySales(UsersBySale userBySale)
        {
            return _userBySalesRepo.Insert(userBySale);
        }

        public bool EditUserBySales(UsersBySale userBySale)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUserBySales(long id)
        {
            throw new NotImplementedException();
        }

        public bool SaveService()
        {
            return _unitOfWork.Save();
        }
    }
}