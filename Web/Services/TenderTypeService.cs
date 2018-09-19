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
    public class TenderTypeService:ITenderTypeService
    {

        private readonly IRepository<TenderType> _tenderTypeRepo;
        private readonly IUnitOfWork _unitOfWork;

        public TenderTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _tenderTypeRepo = _unitOfWork.Repository<TenderType>();
        }
        public TenderType GetTenderTypeById(long id)
        {
            return _tenderTypeRepo.GetById(id);
        }

        public IEnumerable<TenderType> GetAllTenderType()
        {
            return _tenderTypeRepo.GetAll();
        }

        public bool AddTenderType(TenderType model)
        {
            return _tenderTypeRepo.Insert(model);
        }

        public bool EditTenderType(TenderType newTenderType)
        {
            var oldTender = _tenderTypeRepo.GetById(newTenderType.Id);
            oldTender.Name = newTenderType.Name;
            oldTender.NameAmharic = newTenderType.NameAmharic;
            return _tenderTypeRepo.InsertOrUpdate(oldTender);
        }

        public bool DeleteTenderType(long id)
        {
            return _tenderTypeRepo.DeleteById(id);
        }

        public bool SaveService()
        {
            return _unitOfWork.Save();
        }
    }
}