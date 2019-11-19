// Generated Code! Do not manually edit. Adjust template (BaseControllersGenerator.tt) to make changes to this file. 
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
    public class ConnectorBaseController : BaseController
    {              
        private readonly IConnectorService _connectorService;        

        public ConnectorBaseController(IConnectorService connectorService)
        { 
            _connectorService = connectorService;            
        }

        // GET: Connector
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
            int totalrows = _connectorService.GetAll().Count();
            
            // Search
            var searchResults = _connectorService.Search(searchParams);
                       
            // Filtered record count
            int totalrowsafterfiltering = _connectorService.SearchResultsCount(searchParams);

            return Json(new { data = searchResults, draw = Request.Form["draw"], recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering }, JsonRequestBehavior.AllowGet);
        }

        // POST: Connector/Add
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

        // POST: Connector/Edit
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

        // POST: Connector/Delete
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
