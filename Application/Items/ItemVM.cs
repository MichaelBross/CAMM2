using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Items;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;

namespace Application.Items
{
    public class ItemDetailVM
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

        [ReadOnly(true)]
        [Display(Name = "Date Updated")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime UpdateDate { get; set; }

        [ReadOnly(true)]
        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime CreateDate { get; set; }
    }

    public class ItemListVM
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
