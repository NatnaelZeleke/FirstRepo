using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Infrastructure;

namespace Domain.Services
{
    public interface ITenderTypeService
    {
        TenderType GetTenderTypeById(long id);
        IEnumerable<TenderType> GetAllTenderType();
        bool AddTenderType(TenderType model);
        bool EditTenderType(TenderType newTenderType);
        bool DeleteTenderType(long id);
        bool SaveService();
    }
}
