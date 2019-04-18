using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Bases;
using Domain.Documents;
using System.ComponentModel.DataAnnotations;

namespace Domain.Items
{
    public class Item : Base
    {
        public int Id { get; set; }
        [Display(Name ="Item Number")]
        public string Code { get; set; }
        public string Description { get; set; }
        [Display(Name ="UOM")]
        public UnitsOfMeasure UnitsOfMeasure { get; set; }
        public int QtyOnHand { get; set; }
        public IList<Document> Documents { get; set; }
    }

    public enum UnitsOfMeasure
    {
        Each = 1,
        Inches = 2,
        Feet = 3,
        Meters = 4,
        Ounces = 5,
        Pounds = 6
    }
}
