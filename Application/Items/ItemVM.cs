using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;
using Application.Base;

namespace Application.Items
{
    public class ItemDetailVM : BaseDetailVM
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Item Number")]
        public string Code { get; set; }

        public string Description { get; set; }

        [Display(Name = "Units of Measure")]
        public UnitsOfMeasure UnitsOfMeasure { get; set; }

        [Range(0, 2147483646, ErrorMessage = "Value must be between 0 and 2147483646.")]
        [Display(Name = "Qty on Hand")]        
        public int QtyOnHand { get; set; }

    }

    public class ItemListVM : BaseDetailVM
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

    }
}
