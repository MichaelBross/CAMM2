using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application;
using Application.Interfaces;
using Domain.Items;
//using Application.Items.Queries.GetItemList;
using Application.Items;

namespace Presentation.Items
{
    public class ItemsController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IGetItemList _getItemList;
        private readonly IItemService _itemService;        

        public ItemsController(IUnitOfWork unitOfWork, IItemService itemService)
        {            
            _unitOfWork = unitOfWork;
            _itemService = itemService;            
        }

        // GET: Items
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
            int totalrows = _unitOfWork.Items.GetAll().Count();
            
            // Search
            var searchResults = _itemService.Search(searchParams);
                       
            // Filtered record count
            int totalrowsafterfiltering = searchResults.Count();

            return Json(new { data = searchResults, draw = Request["draw"], recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering }, JsonRequestBehavior.AllowGet);
        }

        // POST: Items/Add
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

        // POST: Items/Edit
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

        // POST: Items/Delete
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



    }
}