using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Items;

namespace Application.Items
{
    public class ItemVM
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public UnitsOfMeasure UnitsOfMeasure { get; set; }
        public int QtyOnHand { get; set; }
    }
}
