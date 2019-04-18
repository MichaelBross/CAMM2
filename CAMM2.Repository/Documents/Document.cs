using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Bases;
using Domain.Items;

namespace Domain.Documents
{
    public class Document : Base
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Rev { get; set; }
        public string Title { get; set; }
        public DocumentType DocType { get; set; }
        public IList<Item> Items { get; set; }
    }

    public enum DocumentType
    {
        Drawing = 1,
        PartsList = 2,
        DataSheet = 3,
        MSDS = 4,
        Catalog = 5
    }
}
