using System;
using System.Collections.Generic;
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
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Qty { get; set; }
        public string Reference { get; set; }
        public bool IsObsolete { get; set; }
    }
}
