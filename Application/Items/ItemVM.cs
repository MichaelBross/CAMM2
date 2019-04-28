using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Items;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Application.Items
{
    public class ItemVM
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Item Number")]
        public string Code { get; set; }

        public string Description { get; set; }

        [Display(Name = "Units of Measure")]
        public UnitsOfMeasure UnitsOfMeasure { get; set; }

        [Display(Name = "Qty on Hand")]
        public int QtyOnHand { get; set; }
        
        [Display(Name = "Date Updated")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime UpdateDate { get; set; }

        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime CreateDate { get; set; }
    }
}
