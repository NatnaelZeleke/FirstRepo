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
    public class NewsPaperService:INewsPaperService
    {

        private readonly IRepository<NewsPaper> _newsPaperRepo;
        private readonly IUnitOfWork _unitOfWork;

        public NewsPaperService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _newsPaperRepo = _unitOfWork.Repository<NewsPaper>();
        }
        public NewsPaper GetNewsPaperbyId(long id)
        {
            return _newsPaperRepo.GetById(id);
        }

        public IEnumerable<NewsPaper> GetAllNewsPapers()
        {
            return _newsPaperRepo.GetAll();
        }

        public bool AddNewsPaper(NewsPaper newsPaper)
        {
            return _newsPaperRepo.Insert(newsPaper);
        }

        public bool EditNewsPaper(NewsPaper newsPaper)
        {
            var old = _newsPaperRepo.GetById(newsPaper.Id);
            old.Name = newsPaper.Name;
            old.NameAmharic = newsPaper.NameAmharic;
            return _newsPaperRepo.InsertOrUpdate(newsPaper);
        }

        public bool DeleteNewsPaper(long id)
        {
            return _newsPaperRepo.DeleteById(id);
        }

        public bool SaveService()
        {
            return _unitOfWork.Save();
        }
    }
}