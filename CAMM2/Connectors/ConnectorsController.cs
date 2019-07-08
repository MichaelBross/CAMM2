﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application;
using Application.Interfaces;
using Application.Connectors;

namespace Presentation.Connectors
{
    public class ConnectorsController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IGetConnectorList _getConnectorList;
        private readonly IConnectorService _connectorService;        

        public ConnectorsController(IUnitOfWork unitOfWork, IConnectorService connectorService)
        {            
            _unitOfWork = unitOfWork;
            _connectorService = connectorService;            
        }

        // GET: Connectors
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SearchServerSide()
        {
            // Get Server Side Parameters from Request
            var searchParams = MapDataTableRequestToSearchParams(Request);

            // Total record count
            int totalrows = _unitOfWork.Connectors.GetAll().Count();
            
            // Search
            var searchResults = _connectorService.Search(searchParams);
                       
            // Filtered record count
            int totalrowsafterfiltering = searchResults.Count();

            return Json(new { data = searchResults, draw = Request["draw"], recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering }, JsonRequestBehavior.AllowGet);
        }

        // POST: Connectors/Add
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Code,Description,UnitsOfMeasure,QtyOnHand")] ConnectorDetailVM connectorVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _connectorService.Add(connectorVM);
                    return Json(new { success = true, model = connectorVM });
                }
                catch (Exception ex)
                {
                    if(ex.Message.Contains("IX_Code"))
                        ModelState.AddModelError("Code", "This Connector Number already exists. Duplicate Connector Numbers are not allowed.");
                    else
                        ModelState.AddModelError(string.Empty, "The save failed.");
                }
            }
            return JsonErrorResult();
        }

        // POST: Connectors/Edit
        [HttpPost]
        [ValidateJsonAntiForgeryToken]        
        //public JsonResult Update([Bind(Include = "Id,Code,Description,UnitsOfMeasure,QtyOnHand")]ConnectorVM revised)
        public JsonResult Update(ConnectorDetailVM revised)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updated = _connectorService.Update(revised);                     
                    return Json(new { success = true, model = updated });
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("IX_Code"))
                        ModelState.AddModelError("Code", "This Connector Number already exists. Duplicate Connector Numbers are not allowed.");
                    else
                        ModelState.AddModelError(string.Empty, "The save failed.");
                }
            }

            return JsonErrorResult();
        }

        // POST: Connectors/Delete
        [ValidateJsonAntiForgeryToken]
        [HttpPost]
        public JsonResult Delete([Bind(Include = "Id")] ConnectorDetailVM toBeRemoved)
        {
            if (toBeRemoved.Id == 0)
            {
                return Json(new { success = false, ErrorMessage = "Id cannot be zero." });
            }

            try
            {
                _connectorService.Remove(toBeRemoved);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Connector not found."))
                    ModelState.AddModelError(string.Empty, "The delete failed because the connector was not found.");
                else
                    ModelState.AddModelError(string.Empty, "The delete failed.");

                return JsonErrorResult();
            }            
        }



    }
}