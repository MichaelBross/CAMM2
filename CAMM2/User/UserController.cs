// Generated Code! Do not manually edit. Adjust template (Controllers.tt) to make changes to this file.
 
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
    public class UserController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;      
        private readonly IUserService _userService;        

        public UserController(IUnitOfWork unitOfWork, IUserService userService)
        {            
            _unitOfWork = unitOfWork;
            _userService = userService;            
        }

        // GET: User
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
            int totalrows = _unitOfWork.Users.GetAll().Count();
            
            // Search
            var searchResults = _userService.Search(searchParams);
                       
            // Filtered record count
            int totalrowsafterfiltering = searchResults.Count();

            return Json(new { data = searchResults, draw = Request["draw"], recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering }, JsonRequestBehavior.AllowGet);
        }

        // POST: User/Add
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Code,Description,UnitsOfMeasure,QtyOnHand")] UserDetailVM userVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _userService.Add(userVM);
                    return Json(new { success = true, model = userVM });
                }
                catch (Exception ex)
                {
                    if(ex.Message.Contains("IX_Code"))
                        ModelState.AddModelError("Code", "This User Number already exists. Duplicate User Numbers are not allowed.");
                    else
                        ModelState.AddModelError(string.Empty, "The save failed.");
                }
            }
            return JsonErrorResult();
        }

        // POST: User/Edit
        [HttpPost]
        [ValidateJsonAntiForgeryToken]        
        //public JsonResult Update([Bind(Include = "Id,Code,Description,UnitsOfMeasure,QtyOnHand")]UserVM revised)
        public JsonResult Update(UserDetailVM revised)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updated = _userService.Update(revised);                     
                    return Json(new { success = true, model = updated });
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("IX_Code"))
                        ModelState.AddModelError("Code", "This User Number already exists. Duplicate User Numbers are not allowed.");
                    else
                        ModelState.AddModelError(string.Empty, "The save failed.");
                }
            }

            return JsonErrorResult();
        }

        // POST: User/Delete
        [ValidateJsonAntiForgeryToken]
        [HttpPost]
        public JsonResult Delete([Bind(Include = "Id")] UserDetailVM toBeRemoved)
        {
            if (toBeRemoved.Id == 0)
            {
                return Json(new { success = false, ErrorMessage = "Id cannot be zero." });
            }

            try
            {
                _userService.Remove(toBeRemoved);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("User not found."))
                    ModelState.AddModelError(string.Empty, "The delete failed because the user was not found.");
                else
                    ModelState.AddModelError(string.Empty, "The delete failed.");

                return JsonErrorResult();
            }            
        }
    }
}
