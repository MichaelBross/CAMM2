using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;

namespace Application.Base
{
    public class BaseListVM
    {
        [HiddenInput(DisplayValue = false)]
        [ReadOnly(true)]
        [Display(Name = "Date Updated")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime UpdateDate { get; set; }

        [HiddenInput(DisplayValue = false)]
        [ReadOnly(true)]
        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime CreateDate { get; set; }
    }
}
