using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Items
{
    public class Contact : Item
    {
        public string Size { get; set; }
        public string Family { get; set; }
        public int WireGageMin { get; set; }
        public int WireGageMax { get; set; }
        public string Comments { get; set; }
        public IList<Tool> Tools { get; set; }
    }
}
