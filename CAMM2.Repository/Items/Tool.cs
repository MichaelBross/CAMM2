using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Items
{
    public class Tool : Item
    {
        public string Manufacturer { get; set; }
        public string BinNumber { get; set; }
        public string MilitarySpecification { get; set; }
        public string SerialNumber { get; set; }
        public string Comments { get; set; }
        public IList<Connector> Connectors { get; set; }
        public IList<Contact> Contacts { get; set; }
    }
}
