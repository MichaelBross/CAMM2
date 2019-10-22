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
    public class ContactBaseController : BaseController
    {              
        private readonly IContactService _contactService;        

        public ContactBaseController(IContactService contactService)
        { 
            _contactService = contactService;            
        }

        // GET: Contact
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
            int totalrows = _contactService.GetAll().Count();
            
            // Search
            var searchResults = _contactService.Search(searchParams);
                       
            // Filtered record count
            int totalrowsafterfiltering = _contactService.SearchResultsCount(searchParams);

            return Json(new { data = searchResults, draw = Request.Form["draw"], recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering }, JsonRequestBehavior.AllowGet);
        }

        // POST: Contact/Add
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Code,Description,UnitsOfMeasure,QtyOnHand")] ContactDetailVM contactVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _contactService.Add(contactVM);
                    return Json(new { success = true, model = contactVM });
                }
                catch (Exception ex)
                {
                    if(ex.Message.Contains("IX_Code"))
                        ModelState.AddModelError("Code", "This Contact Number already exists. Duplicate Contact Numbers are not allowed.");
                    else
                        ModelState.AddModelError(string.Empty, "The save failed.");
                }
            }
            return JsonErrorResult();
        }

        // POST: Contact/Edit
        [HttpPost]
        [ValidateJsonAntiForgeryToken]        
        //public JsonResult Update([Bind(Include = "Id,Code,Description,UnitsOfMeasure,QtyOnHand")]ContactVM revised)
        public JsonResult Update(ContactDetailVM revised)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updated = _contactService.Update(revised);                     
                    return Json(new { success = true, model = updated });
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("IX_Code"))
                        ModelState.AddModelError("Code", "This Contact Number already exists. Duplicate Contact Numbers are not allowed.");
                    else
                        ModelState.AddModelError(string.Empty, "The save failed.");
                }
            }

            return JsonErrorResult();
        }

        // POST: Contact/Delete
        [ValidateJsonAntiForgeryToken]
        [HttpPost]
        public JsonResult Delete([Bind(Include = "Id")] ContactDetailVM toBeRemoved)
        {
            if (toBeRemoved.Id == 0)
            {
                return Json(new { success = false, ErrorMessage = "Id cannot be zero." });
            }

            try
            {
                _contactService.Remove(toBeRemoved);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Contact not found."))
                    ModelState.AddModelError(string.Empty, "The delete failed because the contact was not found.");
                else
                    ModelState.AddModelError(string.Empty, "The delete failed.");

                return JsonErrorResult();
            }            
        }
    }
}
