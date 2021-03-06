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
    public class DocumentBaseController : BaseController
    {              
        private readonly IDocumentService _documentService;        

        public DocumentBaseController(IDocumentService documentService)
        { 
            _documentService = documentService;            
        }

        // GET: Document
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
            int totalrows = _documentService.GetAll().Count();
            
            // Search
            var searchResults = _documentService.Search(searchParams);
                       
            // Filtered record count
            int totalrowsafterfiltering = _documentService.SearchResultsCount(searchParams);

            return Json(new { data = searchResults, draw = Request.Form["draw"], recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering }, JsonRequestBehavior.AllowGet);
        }

        // POST: Document/Add
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Code,Description,UnitsOfMeasure,QtyOnHand")] DocumentDetailVM documentVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _documentService.Add(documentVM);
                    return Json(new { success = true, model = documentVM });
                }
                catch (Exception ex)
                {
                    if(ex.Message.Contains("IX_Code"))
                        ModelState.AddModelError("Code", "This Document Number already exists. Duplicate Document Numbers are not allowed.");
                    else
                        ModelState.AddModelError(string.Empty, "The save failed.");
                }
            }
            return JsonErrorResult();
        }

        // POST: Document/Edit
        [HttpPost]
        [ValidateJsonAntiForgeryToken]        
        //public JsonResult Update([Bind(Include = "Id,Code,Description,UnitsOfMeasure,QtyOnHand")]DocumentVM revised)
        public JsonResult Update(DocumentDetailVM revised)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updated = _documentService.Update(revised);                     
                    return Json(new { success = true, model = updated });
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("IX_Code"))
                        ModelState.AddModelError("Code", "This Document Number already exists. Duplicate Document Numbers are not allowed.");
                    else
                        ModelState.AddModelError(string.Empty, "The save failed.");
                }
            }

            return JsonErrorResult();
        }

        // POST: Document/Delete
        [ValidateJsonAntiForgeryToken]
        [HttpPost]
        public JsonResult Delete([Bind(Include = "Id")] DocumentDetailVM toBeRemoved)
        {
            if (toBeRemoved.Id == 0)
            {
                return Json(new { success = false, ErrorMessage = "Id cannot be zero." });
            }

            try
            {
                _documentService.Remove(toBeRemoved);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Document not found."))
                    ModelState.AddModelError(string.Empty, "The delete failed because the document was not found.");
                else
                    ModelState.AddModelError(string.Empty, "The delete failed.");

                return JsonErrorResult();
            }            
        }

    }
}
