using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Infrastructure;
using Domain.Utils;

namespace Domain.Services
{
    public interface IRoleService
    {
        Role GetRoleByType(Enums.ProfileType profileType);
        Role GetUserRole(long userId);
        bool SaveRole();
    }
}
