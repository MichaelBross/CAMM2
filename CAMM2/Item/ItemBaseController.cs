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
    public class ItemBaseController : BaseController
    {              
        private readonly IItemService _itemService;        

        public ItemBaseController(IItemService itemService)
        { 
            _itemService = itemService;            
        }

        // GET: Item
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
            int totalrows = _itemService.GetAll().Count();
            
            // Search
            var searchResults = _itemService.Search(searchParams);
                       
            // Filtered record count
            int totalrowsafterfiltering = _itemService.SearchResultsCount(searchParams);

            return Json(new { data = searchResults, draw = Request.Form["draw"], recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering }, JsonRequestBehavior.AllowGet);
        }

        // POST: Item/Add
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Code,Description,UnitsOfMeasure,QtyOnHand")] ItemDetailVM itemVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _itemService.Add(itemVM);
                    return Json(new { success = true, model = itemVM });
                }
                catch (Exception ex)
                {
                    if(ex.Message.Contains("IX_Code"))
                        ModelState.AddModelError("Code", "This Item Number already exists. Duplicate Item Numbers are not allowed.");
                    else
                        ModelState.AddModelError(string.Empty, "The save failed.");
                }
            }
            return JsonErrorResult();
        }

        // POST: Item/Edit
        [HttpPost]
        [ValidateJsonAntiForgeryToken]        
        //public JsonResult Update([Bind(Include = "Id,Code,Description,UnitsOfMeasure,QtyOnHand")]ItemVM revised)
        public JsonResult Update(ItemDetailVM revised)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updated = _itemService.Update(revised);                     
                    return Json(new { success = true, model = updated });
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("IX_Code"))
                        ModelState.AddModelError("Code", "This Item Number already exists. Duplicate Item Numbers are not allowed.");
                    else
                        ModelState.AddModelError(string.Empty, "The save failed.");
                }
            }

            return JsonErrorResult();
        }

        // POST: Item/Delete
        [ValidateJsonAntiForgeryToken]
        [HttpPost]
        public JsonResult Delete([Bind(Include = "Id")] ItemDetailVM toBeRemoved)
        {
            if (toBeRemoved.Id == 0)
            {
                return Json(new { success = false, ErrorMessage = "Id cannot be zero." });
            }

            try
            {
                _itemService.Remove(toBeRemoved);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Item not found."))
                    ModelState.AddModelError(string.Empty, "The delete failed because the item was not found.");
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
            var result = _itemService.Search(searchParams).Select(i => new
            {
                label = i.Code,
                id = i.Id
            }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
