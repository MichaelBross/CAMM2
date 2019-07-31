using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Connector : Item
    {
        public string Family { get; set; }
        public string Comments { get; set; }
        public IList<Tool> Tools { get; set; }
    }
}
