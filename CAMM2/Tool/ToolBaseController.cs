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
    public class ToolBaseController : BaseController
    {              
        private readonly IToolService _toolService;        

        public ToolBaseController(IToolService toolService)
        { 
            _toolService = toolService;            
        }

        // GET: Tool
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
            int totalrows = _toolService.GetAll().Count();
            
            // Search
            var searchResults = _toolService.Search(searchParams);
                       
            // Filtered record count
            int totalrowsafterfiltering = _toolService.SearchResultsCount(searchParams);

            return Json(new { data = searchResults, draw = Request.Form["draw"], recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering }, JsonRequestBehavior.AllowGet);
        }

        // POST: Tool/Add
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Code,Description,UnitsOfMeasure,QtyOnHand")] ToolDetailVM toolVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _toolService.Add(toolVM);
                    return Json(new { success = true, model = toolVM });
                }
                catch (Exception ex)
                {
                    if(ex.Message.Contains("IX_Code"))
                        ModelState.AddModelError("Code", "This Tool Number already exists. Duplicate Tool Numbers are not allowed.");
                    else
                        ModelState.AddModelError(string.Empty, "The save failed.");
                }
            }
            return JsonErrorResult();
        }

        // POST: Tool/Edit
        [HttpPost]
        [ValidateJsonAntiForgeryToken]        
        //public JsonResult Update([Bind(Include = "Id,Code,Description,UnitsOfMeasure,QtyOnHand")]ToolVM revised)
        public JsonResult Update(ToolDetailVM revised)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updated = _toolService.Update(revised);                     
                    return Json(new { success = true, model = updated });
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("IX_Code"))
                        ModelState.AddModelError("Code", "This Tool Number already exists. Duplicate Tool Numbers are not allowed.");
                    else
                        ModelState.AddModelError(string.Empty, "The save failed.");
                }
            }

            return JsonErrorResult();
        }

        // POST: Tool/Delete
        [ValidateJsonAntiForgeryToken]
        [HttpPost]
        public JsonResult Delete([Bind(Include = "Id")] ToolDetailVM toBeRemoved)
        {
            if (toBeRemoved.Id == 0)
            {
                return Json(new { success = false, ErrorMessage = "Id cannot be zero." });
            }

            try
            {
                _toolService.Remove(toBeRemoved);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Tool not found."))
                    ModelState.AddModelError(string.Empty, "The delete failed because the tool was not found.");
                else
                    ModelState.AddModelError(string.Empty, "The delete failed.");

                return JsonErrorResult();
            }            
        }
    }
}
