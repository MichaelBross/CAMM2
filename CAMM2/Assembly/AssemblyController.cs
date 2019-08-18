 
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
    public class AssemblyController : BaseController
    {              
        private readonly IAssemblyService _assemblyService;        

        public AssemblyController(IAssemblyService assemblyService)
        { 
            _assemblyService = assemblyService;            
        }

        // GET: Assembly
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
            int totalrows = _assemblyService.GetAll().Count();
            
            // Search
            var searchResults = _assemblyService.Search(searchParams);
                       
            // Filtered record count
            int totalrowsafterfiltering = searchResults.Count();

            return Json(new { data = searchResults, draw = Request["draw"], recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering }, JsonRequestBehavior.AllowGet);
        }

        // POST: Assembly/Add
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Code,Description,UnitsOfMeasure,QtyOnHand")] AssemblyDetailVM assemblyVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _assemblyService.Add(assemblyVM);
                    return Json(new { success = true, model = assemblyVM });
                }
                catch (Exception ex)
                {
                    if(ex.Message.Contains("IX_Code"))
                        ModelState.AddModelError("Code", "This Assembly Number already exists. Duplicate Assembly Numbers are not allowed.");
                    else
                        ModelState.AddModelError(string.Empty, "The save failed.");
                }
            }
            return JsonErrorResult();
        }

        // POST: Assembly/Edit
        [HttpPost]
        [ValidateJsonAntiForgeryToken]        
        //public JsonResult Update([Bind(Include = "Id,Code,Description,UnitsOfMeasure,QtyOnHand")]AssemblyVM revised)
        public JsonResult Update(AssemblyDetailVM revised)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updated = _assemblyService.Update(revised);                     
                    return Json(new { success = true, model = updated });
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("IX_Code"))
                        ModelState.AddModelError("Code", "This Assembly Number already exists. Duplicate Assembly Numbers are not allowed.");
                    else
                        ModelState.AddModelError(string.Empty, "The save failed.");
                }
            }

            return JsonErrorResult();
        }

        // POST: Assembly/Delete
        [ValidateJsonAntiForgeryToken]
        [HttpPost]
        public JsonResult Delete([Bind(Include = "Id")] AssemblyDetailVM toBeRemoved)
        {
            if (toBeRemoved.Id == 0)
            {
                return Json(new { success = false, ErrorMessage = "Id cannot be zero." });
            }

            try
            {
                _assemblyService.Remove(toBeRemoved);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Assembly not found."))
                    ModelState.AddModelError(string.Empty, "The delete failed because the assembly was not found.");
                else
                    ModelState.AddModelError(string.Empty, "The delete failed.");

                return JsonErrorResult();
            }            
        }
    }
}
