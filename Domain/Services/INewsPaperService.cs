using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Infrastructure;

namespace Domain.Services
{
    public interface INewsPaperService
    {
        NewsPaper GetNewsPaperbyId(long id);
        IEnumerable<NewsPaper> GetAllNewsPapers();
        bool AddNewsPaper(NewsPaper newsPaper);
        bool EditNewsPaper(NewsPaper newsPaper);
        bool DeleteNewsPaper(long id);
        bool SaveService();
    }
}
