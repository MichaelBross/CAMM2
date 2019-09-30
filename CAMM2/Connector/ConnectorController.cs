 
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
    public class ConnectorController : ConnectorBaseController
    {              
        private readonly IConnectorService _connectorService;        

        public ConnectorController(IConnectorService connectorService) : base(connectorService)
        { 
            _connectorService = connectorService;            
        }

        public ActionResult ConnectorDocuments(int connectorId)
        {
            var connector = _connectorService.GetConnectorAndDocuments(connectorId);
            return View(connector);
        }

        [HttpPost]
        public JsonResult GetDocuments(int connectorId)
        {
            var connector = _connectorService.GetConnectorAndDocuments(connectorId);
            var documents = connector.Documents;            
            return Json(new { data = documents, draw = Request["draw"] }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddExistingDocuments(int connectorId, IEnumerable<int> documentIdList)
        {  
            var result = "Success";
            foreach(int documentId in documentIdList)
            {
                if(_connectorService.LinkDocumentToConnector(connectorId, documentId) != "Success")
                {
                    result = "Failed";
                }
            }
            
            if (result == "Success")
            {
                return RedirectToAction("ConnectorDocuments", "Connector", new { connectorId });
            }
            else
            {
                return JsonErrorResult();
            }
        }

        public ActionResult RemoveDocuments(int connectorId, IEnumerable<int> documentIdList)
        {
            var result = "Success";
            foreach (int documentId in documentIdList)
            {
                if (_connectorService.RemoveDocumentFromConnector(connectorId, documentId) != "Success")
                {
                    result = "Failed";
                }
            }

            if (result == "Success")
            {
                return RedirectToAction("ConnectorDocuments", "Connector", new { connectorId });
            }
            else
            {
                return JsonErrorResult();
            }
        }

    }
}
