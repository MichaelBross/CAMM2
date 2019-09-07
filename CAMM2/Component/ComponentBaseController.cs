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
    public class ComponentBaseController : BaseController
    {              
        private readonly IComponentService _componentService;        

        public ComponentBaseController(IComponentService componentService)
        { 
            _componentService = componentService;            
        }

        // GET: Component
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
            int totalrows = _componentService.GetAll().Count();
            
            // Search
            var searchResults = _componentService.Search(searchParams);
                       
            // Filtered record count
            int totalrowsafterfiltering = searchResults.Count();

            return Json(new { data = searchResults, draw = Request["draw"], recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering }, JsonRequestBehavior.AllowGet);
        }

        // POST: Component/Add
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Code,Description,UnitsOfMeasure,QtyOnHand")] ComponentDetailVM componentVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _componentService.Add(componentVM);
                    return Json(new { success = true, model = componentVM });
                }
                catch (Exception ex)
                {
                    if(ex.Message.Contains("IX_Code"))
                        ModelState.AddModelError("Code", "This Component Number already exists. Duplicate Component Numbers are not allowed.");
                    else
                        ModelState.AddModelError(string.Empty, "The save failed.");
                }
            }
            return JsonErrorResult();
        }

        // POST: Component/Edit
        [HttpPost]
        [ValidateJsonAntiForgeryToken]        
        //public JsonResult Update([Bind(Include = "Id,Code,Description,UnitsOfMeasure,QtyOnHand")]ComponentVM revised)
        public JsonResult Update(ComponentDetailVM revised)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updated = _componentService.Update(revised);                     
                    return Json(new { success = true, model = updated });
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("IX_Code"))
                        ModelState.AddModelError("Code", "This Component Number already exists. Duplicate Component Numbers are not allowed.");
                    else
                        ModelState.AddModelError(string.Empty, "The save failed.");
                }
            }

            return JsonErrorResult();
        }

        // POST: Component/Delete
        [ValidateJsonAntiForgeryToken]
        [HttpPost]
        public JsonResult Delete([Bind(Include = "Id")] ComponentDetailVM toBeRemoved)
        {
            if (toBeRemoved.Id == 0)
            {
                return Json(new { success = false, ErrorMessage = "Id cannot be zero." });
            }

            try
            {
                _componentService.Remove(toBeRemoved);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Component not found."))
                    ModelState.AddModelError(string.Empty, "The delete failed because the component was not found.");
                else
                    ModelState.AddModelError(string.Empty, "The delete failed.");

                return JsonErrorResult();
            }            
        }
    }
}
