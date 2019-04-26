using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Bases;
using Domain.Documents;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Items
{
    public class Item : Base
    {
        public int Id { get; set; }
        [Index(IsUnique = true)]
        [StringLength(100)]
        public string Code { get; set; }
        public string Description { get; set; }
        public UnitsOfMeasure UnitsOfMeasure { get; set; }
        public int QtyOnHand { get; set; }
        public IList<Document> Documents { get; set; }
    }
}
