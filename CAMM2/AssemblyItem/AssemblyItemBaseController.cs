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
    public class AssemblyItemBaseController : BaseController
    {              
        private readonly IAssemblyItemService _assemblyitemService;        

        public AssemblyItemBaseController(IAssemblyItemService assemblyitemService)
        { 
            _assemblyitemService = assemblyitemService;            
        }

        // GET: AssemblyItem
        public ActionResult Index()
        {
            return View();
        }


        // POST: AssemblyItem/Add
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Code,Description,UnitsOfMeasure,QtyOnHand")] AssemblyItemDetailVM assemblyitemVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _assemblyitemService.Add(assemblyitemVM);
                    return Json(new { success = true, model = assemblyitemVM });
                }
                catch (Exception ex)
                {
                    if(ex.Message.Contains("IX_Code"))
                        ModelState.AddModelError("Code", "This AssemblyItem Number already exists. Duplicate AssemblyItem Numbers are not allowed.");
                    else
                        ModelState.AddModelError(string.Empty, "The save failed.");
                }
            }
            return JsonErrorResult();
        }

        // POST: AssemblyItem/Edit
        [HttpPost]
        [ValidateJsonAntiForgeryToken]        
        //public JsonResult Update([Bind(Include = "Id,Code,Description,UnitsOfMeasure,QtyOnHand")]AssemblyItemVM revised)
        public JsonResult Update(AssemblyItemDetailVM revised)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updated = _assemblyitemService.Update(revised);                     
                    return Json(new { success = true, model = updated });
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("IX_Code"))
                        ModelState.AddModelError("Code", "This AssemblyItem Number already exists. Duplicate AssemblyItem Numbers are not allowed.");
                    else
                        ModelState.AddModelError(string.Empty, "The save failed.");
                }
            }

            return JsonErrorResult();
        }

        // POST: AssemblyItem/Delete
        [ValidateJsonAntiForgeryToken]
        [HttpPost]
        public JsonResult Delete([Bind(Include = "Id")] AssemblyItemDetailVM toBeRemoved)
        {
            if (toBeRemoved.Id == 0)
            {
                return Json(new { success = false, ErrorMessage = "Id cannot be zero." });
            }

            try
            {
                _assemblyitemService.Remove(toBeRemoved);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("AssemblyItem not found."))
                    ModelState.AddModelError(string.Empty, "The delete failed because the assemblyitem was not found.");
                else
                    ModelState.AddModelError(string.Empty, "The delete failed.");

                return JsonErrorResult();
            }            
        }
    }
}
