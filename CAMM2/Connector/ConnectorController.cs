 
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application;
using Application.Interfaces;
using Application.Service;
using Application.ViewModels;

namespace Presentation
{ 
    public class ConnectorController : ConnectorBaseController
    {              
        private readonly IConnectorService _connectorService;        

        public ConnectorController(IConnectorService connectorService) : base(connectorService)
        { 
            _connectorService = connectorService;            
        }

        [HttpPost]
        public JsonResult GetDocuments(int connectorId)
        {
            var connector = _connectorService.GetConnectorAndDocuments(connectorId);
            var documents = connector.Documents;
            return Json(new { data = documents, draw = Request["draw"] }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAutoCompleteList(string term)
        {
            var searchParams = new SearchParameters();
            searchParams.SearchValue = term;
            searchParams.SortColumnName = "Code";
            searchParams.Length = 10;
            var result = _connectorService.Search(searchParams).Select(i => new
            {
                label = i.Code,
                id = i.Id
            }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
