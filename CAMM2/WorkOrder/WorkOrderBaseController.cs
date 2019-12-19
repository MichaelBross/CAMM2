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
    public class WorkOrderBaseController : BaseController
    {              
        private readonly IWorkOrderService _workorderService;        

        public WorkOrderBaseController(IWorkOrderService workorderService)
        { 
            _workorderService = workorderService;            
        }

        // GET: WorkOrder
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
            int totalrows = _workorderService.GetAll().Count();
            
            // Search
            var searchResults = _workorderService.Search(searchParams);
                       
            // Filtered record count
            int totalrowsafterfiltering = _workorderService.SearchResultsCount(searchParams);

            return Json(new { data = searchResults, draw = Request.Form["draw"], recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering }, JsonRequestBehavior.AllowGet);
        }

        // POST: WorkOrder/Add
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Code,Description,UnitsOfMeasure,QtyOnHand")] WorkOrderDetailVM workorderVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _workorderService.Add(workorderVM);
                    return Json(new { success = true, model = workorderVM });
                }
                catch (Exception ex)
                {
                    if(ex.Message.Contains("IX_Code"))
                        ModelState.AddModelError("Code", "This WorkOrder Number already exists. Duplicate WorkOrder Numbers are not allowed.");
                    else
                        ModelState.AddModelError(string.Empty, "The save failed.");
                }
            }
            return JsonErrorResult();
        }

        // POST: WorkOrder/Edit
        [HttpPost]
        [ValidateJsonAntiForgeryToken]        
        //public JsonResult Update([Bind(Include = "Id,Code,Description,UnitsOfMeasure,QtyOnHand")]WorkOrderVM revised)
        public JsonResult Update(WorkOrderDetailVM revised)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updated = _workorderService.Update(revised);                     
                    return Json(new { success = true, model = updated });
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("IX_Code"))
                        ModelState.AddModelError("Code", "This WorkOrder Number already exists. Duplicate WorkOrder Numbers are not allowed.");
                    else
                        ModelState.AddModelError(string.Empty, "The save failed.");
                }
            }

            return JsonErrorResult();
        }

        // POST: WorkOrder/Delete
        [ValidateJsonAntiForgeryToken]
        [HttpPost]
        public JsonResult Delete([Bind(Include = "Id")] WorkOrderDetailVM toBeRemoved)
        {
            if (toBeRemoved.Id == 0)
            {
                return Json(new { success = false, ErrorMessage = "Id cannot be zero." });
            }

            try
            {
                _workorderService.Remove(toBeRemoved);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("WorkOrder not found."))
                    ModelState.AddModelError(string.Empty, "The delete failed because the workorder was not found.");
                else
                    ModelState.AddModelError(string.Empty, "The delete failed.");

                return JsonErrorResult();
            }            
        }

    }
}
