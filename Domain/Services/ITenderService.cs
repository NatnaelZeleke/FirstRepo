using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Infrastructure;

namespace Domain.Services
{
    public interface ITenderService
    {
        Tender GetTenderById(long id);
        IEnumerable<Tender> GetAllTenders();
        IEnumerable<Tender> GetSpecificTenders(long[] ids, int skip, int top);
        IEnumerable<Tender> GetGroupedTender(int groupedBy, int tenderSelectionId, int skip, int top);
        bool AddTender(Tender model);
        bool EditTender(Tender model);
        bool DeleteTender(long id);
        IEnumerable<IEnumerable<Tender>> GetTenderReport();
        IEnumerable<IEnumerable<Tender>> GetTenderTypeReport();
        Tender GetTenderByTenderTypeIdAndDate(long tenderId,DateTime date);
        bool SaveService();
    }
}
