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
    public class RoleService:IRoleService
    {
        private readonly IRepository<Role> _roleRepo;
        private readonly IUnitOfWork _unitOfWork;
        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _roleRepo = _unitOfWork.Repository<Role>();
        }
        public Role GetRoleByType(Enums.ProfileType profileType)
        {
            switch (profileType)
            {
                case Enums.ProfileType.User:
                    return _roleRepo.SelectSingle(r => r.Name.Equals("User"));
                case Enums.ProfileType.Admin:
                    return _roleRepo.SelectSingle(r => r.Name.Equals("Admin"));
                case Enums.ProfileType.SubAdmin:
                    return _roleRepo.SelectSingle(r => r.Name.Equals("SubAdmin"));
                default:
                    return null;
            }
        }

        public Role GetUserRole(long userId)
        {
            return _roleRepo.SelectSingle(r => r.Users.Any(u => u.Id == userId));
        }

        public bool SaveRole()
        {
            return _unitOfWork.Save();
        }
    }
}