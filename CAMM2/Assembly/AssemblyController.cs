 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application;
using Application.Interfaces;
using Application.Service;

namespace Presentation
{ 
    public class AssemblyController : AssemblyBaseController
    {              
        private readonly IAssemblyService _assemblyService;        

        public AssemblyController(IAssemblyService assemblyService) : base(assemblyService)
        { 
            _assemblyService = assemblyService;            
        }

        public ActionResult GetAutoCompleteList(string term)
        {
            var searchParams = new SearchParameters();
            searchParams.SearchValue = term;
            searchParams.SortColumnName = "Code";
            searchParams.Length = 10;
            var result = _assemblyService.Search(searchParams).Select(i => new
            {
                label = i.Code,
                id = i.Id
            }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
