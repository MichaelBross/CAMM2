using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Items;
using System.ComponentModel.DataAnnotations;

namespace Application.Items
{
    public class ItemVM
    {
        public int Id { get; set; }
        [Display(Name = "Item Number")]
        public string Code { get; set; }
        public string Description { get; set; }
        [Display(Name = "Units of Measure")]
        public UnitsOfMeasure UnitsOfMeasure { get; set; }
        [Display(Name = "Qty on Hand")]
        public int QtyOnHand { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
