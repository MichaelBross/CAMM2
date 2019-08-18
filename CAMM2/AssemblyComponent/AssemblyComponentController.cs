 
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
    public class AssemblyComponentController : BaseController
    {              
        private readonly IAssemblyComponentService _assemblycomponentService;        

        public AssemblyComponentController(IAssemblyComponentService assemblycomponentService)
        { 
            _assemblycomponentService = assemblycomponentService;            
        }

        // GET: AssemblyComponent
        public ActionResult Index()
        {
            return View();
        }


        // POST: AssemblyComponent/Add
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Code,Description,UnitsOfMeasure,QtyOnHand")] AssemblyComponentDetailVM assemblycomponentVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _assemblycomponentService.Add(assemblycomponentVM);
                    return Json(new { success = true, model = assemblycomponentVM });
                }
                catch (Exception ex)
                {
                    if(ex.Message.Contains("IX_Code"))
                        ModelState.AddModelError("Code", "This AssemblyComponent Number already exists. Duplicate AssemblyComponent Numbers are not allowed.");
                    else
                        ModelState.AddModelError(string.Empty, "The save failed.");
                }
            }
            return JsonErrorResult();
        }

        // POST: AssemblyComponent/Edit
        [HttpPost]
        [ValidateJsonAntiForgeryToken]        
        //public JsonResult Update([Bind(Include = "Id,Code,Description,UnitsOfMeasure,QtyOnHand")]AssemblyComponentVM revised)
        public JsonResult Update(AssemblyComponentDetailVM revised)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updated = _assemblycomponentService.Update(revised);                     
                    return Json(new { success = true, model = updated });
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("IX_Code"))
                        ModelState.AddModelError("Code", "This AssemblyComponent Number already exists. Duplicate AssemblyComponent Numbers are not allowed.");
                    else
                        ModelState.AddModelError(string.Empty, "The save failed.");
                }
            }

            return JsonErrorResult();
        }

        // POST: AssemblyComponent/Delete
        [ValidateJsonAntiForgeryToken]
        [HttpPost]
        public JsonResult Delete([Bind(Include = "Id")] AssemblyComponentDetailVM toBeRemoved)
        {
            if (toBeRemoved.Id == 0)
            {
                return Json(new { success = false, ErrorMessage = "Id cannot be zero." });
            }

            try
            {
                _assemblycomponentService.Remove(toBeRemoved);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("AssemblyComponent not found."))
                    ModelState.AddModelError(string.Empty, "The delete failed because the assemblycomponent was not found.");
                else
                    ModelState.AddModelError(string.Empty, "The delete failed.");

                return JsonErrorResult();
            }            
        }
    }
}
