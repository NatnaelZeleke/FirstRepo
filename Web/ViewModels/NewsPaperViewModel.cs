using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class NewsPaperViewModel
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string NameAmharic { get; set; }

        public bool Selected { get; set; }
        public int  Count { get; set; }
    }
}