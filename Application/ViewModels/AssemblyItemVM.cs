using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class AssemblyItemVM
    {
        public int AssemblyItemId { get; set; }

        public int AssemblyId { get; set; }

        public int ItemId { get; set; }

        [Display(Name = "Line #", Order = -100)]
        public int LineNumber { get; set; }

        [Display(Name = "Qty Required", Order = -90)]
        public decimal Qty { get; set; }

        [Display(Name = "Item Number", Order = -80)]
        public string Code { get; set; }

        [Display(Name = "Description", Order = -70)]
        public string Description { get; set; }

        [Display(Name = "Reference", Order = -60)]
        public string Reference { get; set; }

        public bool IsObsolete { get; set; }
    }
}
