using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Application.ViewModels
{
    public class ParentItemVM
    {
        public string Type { get; set; }
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public List<SelectListItem> ItemSelectList { get; set; }
    }
}
