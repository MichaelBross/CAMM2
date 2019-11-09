using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Application.ViewModels
{
    public class UploadDocumentsVM
    {
        public List<SelectListItem> ItemSelectList { get; set; }        
        public string LinkToType { get; set; }
        public int LinkToId { get; set; }
        public string LinkToCD { get; set; }
        public List<string> DocumentIdList { get; set; }
    }
}
