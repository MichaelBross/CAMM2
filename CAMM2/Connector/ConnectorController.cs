 
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

        public ActionResult AddExistingDocument(int connectorId, int documentId)
        {
            var result = _connectorService.LinkDocumentToConnector(connectorId, documentId);
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
