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
    public class TenderService:ITenderService
    {
        private readonly IRepository<Tender> _tenderRepo;
        private readonly IUnitOfWork _unitOfWork;

        public TenderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _tenderRepo = _unitOfWork.Repository<Tender>();
        }
        public Tender GetTenderById(long id)
        {
            return _tenderRepo.GetById(id);
        }

        public IEnumerable<Tender> GetAllTenders()
        {
            return _tenderRepo.GetAll().OrderByDescending(t => t.PostedOn);
        }

        public IEnumerable<Tender> GetSpecificTenders(long[] ids, int skip, int top)
        {
            return _tenderRepo.Select(t => ids.Contains(t.TenderTypeId)).OrderByDescending(t => t.PostedOn).Skip(skip).Take(top);
        }

        public IEnumerable<Tender> GetGroupedTender(int groupedBy, int tenderSelectionId, int skip, int top)
        {
             
            switch (groupedBy)
            {
                case 1://select based on newspaper 
                    return _tenderRepo.Select(t => t.NewsPaperId == tenderSelectionId).OrderByDescending(t => t.PostedOn).Skip(skip).Take(top);
                case 2://select based on tender type 
                    return _tenderRepo.Select(t => t.TenderTypeId == tenderSelectionId).OrderByDescending(t => t.PostedOn).Skip(skip).Take(top);
            }
            return null;
        }

        public bool AddTender(Tender model)
        {
            return _tenderRepo.Insert(model);
        }

        public bool EditTender(Tender model)
        {
            var oldTender = _tenderRepo.GetById(model.Id);
            oldTender.TenderTypeId = model.TenderTypeId;
            oldTender.Address = model.Address;
            oldTender.ClosingDay = model.ClosingDay;
            oldTender.OpeningDay = model.OpeningDay;
            oldTender.Description = model.Description;
            oldTender.TenderTitle = model.TenderTitle;
            oldTender.NewsPaperId = model.NewsPaperId;
            oldTender.NewsPaperPublishDate = model.NewsPaperPublishDate;
            return _tenderRepo.InsertOrUpdate(oldTender);
        }

        public bool DeleteTender(long id)
        {
            return _tenderRepo.DeleteById(id);
        }

        public IEnumerable<IEnumerable<Tender>> GetTenderReport()
        {
            return _tenderRepo.GetAll().GroupBy(t => t.NewsPaperId).Select(grp => grp.ToList());
        }

        public IEnumerable<IEnumerable<Tender>> GetTenderTypeReport()
        {
            return _tenderRepo.GetAll().GroupBy(t => t.TenderTypeId).Select(grp => grp.ToList());
        }

        public Tender GetTenderByTenderTypeIdAndDate(long tenderId,DateTime date)
        {
            return _tenderRepo.SelectSingle(t => t.TenderTypeId == tenderId && t.NewsPaperPublishDate == date);
        }

        public bool SaveService()
        {
            return _unitOfWork.Save();
        }
    }
}