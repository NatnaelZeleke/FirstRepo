using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Domain.Infrastructure;

namespace Web.ViewModels
{
    public  class TenderTypeViewModel
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string NameAmharic { get; set; }

        public bool Selected { get; set; }
        public int Count { get; set; }
    }

    public  class TenderViewModel
    {
        public long Id { get; set; }         
        public long TenderTypeId { get; set; }
        
        public System.DateTime ?OpeningDay { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public System.DateTime PostedOn { get; set; }
        [Required]
        public string TenderTitle { get; set; }
        [Required]
        public string Description { get; set; }
        
        public System.DateTime ?ClosingDay { get; set; }
        [Required]
        public long NewsPaperId { get; set; }
        public DateTime NewsPaperPublishDate { get; set; }
        public TenderTypeViewModel TenderType { get; set; }

        public IEnumerable<TenderTypeViewModel> TenderTypes { get; set; }

        public NewsPaperViewModel NewsPaper { get; set; }

        public IEnumerable<NewsPaperViewModel> NewsPapers { get; set; }
        public bool IsSavedByUser { get; set; }
        public bool IsOpen { get; set; }
         
    }

    public class SelectTenderTypeViewModel
    {
        public long[] SelectedTenderTypeIds { get; set; }

        public IEnumerable<TenderTypeViewModel> AllTenderTypes { get; set; }
        public long SelectedTenderTypeIdForEmail { get; set; }

    }

    public class TenderWithStatusViewModel
    {
        public IEnumerable<TenderViewModel> Tenders { get; set; }
        public int Status { get; set; }
    }

    public class GetTenderViewModel
    {
        public int Skip { get; set; }
        public int Top { get; set; }
        public int GroupBy { get; set; }
        public int TenderSelectionId { get; set; }
    }



    public class TenderReport
    {
        public IEnumerable<NewsPaperViewModel> TenderWithNewsPaperReport { get; set; }
        public IEnumerable<TenderTypeViewModel> TenderWithTenderTypeReport { get; set; }
    }

    public class GroupedTenderViewModel
    {
        public int GroupBy { get; set; }
        public int TenderSelectionId { get; set; }
        public string HeaderName { get; set; }
    }
}