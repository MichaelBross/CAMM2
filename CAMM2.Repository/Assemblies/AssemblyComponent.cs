using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AssemblyComponent : Base
    {
        public int Id { get; set; }
        public Assembly Assembly { get; set; }
        public Component Component { get; set; }
        public decimal Qty { get; set; }
    }
}
