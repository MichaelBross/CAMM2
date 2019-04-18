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
    public class ItemsController : Controller
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
            var searchParams = Utilities.MapDataTableRequestToSearchParams(Request);

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
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Code,Description,UnitsOfMeasure,QtyOnHand")] ItemVM itemVM)
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
                    ModelState.AddModelError(String.Empty, ex.Message);                   
                }
            }
            return JsonErrorResult();
        }

        // POST: Items/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Update([Bind(Include = "Id,Code,Description,UnitsOfMeasure,QtyOnHand")]Item revised)
        {
            var req = Request;
            var id = Request.Form.Get("Id");
            if (ModelState.IsValid)
            {
                var retrieved = _unitOfWork.Items.Get(revised.Id);

                retrieved.Code = revised.Code;
                retrieved.Description = revised.Description;
                retrieved.UnitsOfMeasure = revised.UnitsOfMeasure;
                retrieved.QtyOnHand = revised.QtyOnHand;

                _unitOfWork.Complete();

                return Json(new { success = true, model = retrieved });
            }

            return JsonErrorResult();
        }

        // GET: Items/Delete
        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult Delete([Bind(Include = "Id")] Item toBeRemoved)
        {
            if (toBeRemoved.Id == 0)
            {
                return Json(new { success = false, ErrorMessage = "Id cannot be zero." });
            }

            try
            {
                var retrieved = _unitOfWork.Items.Get(toBeRemoved.Id);
                _unitOfWork.Items.Remove(retrieved);
                _unitOfWork.Complete();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, ErrorMessage = ex.Message });
            }
        }

        private JsonResult JsonErrorResult()
        {
            List<ValidationError> validationErrors = new List<ValidationError>();
            var propertyErrors = ModelState.Where(x => x.Value.Errors.Count > 0).ToList();
            foreach (KeyValuePair<string, ModelState> item in propertyErrors)
            {
                ValidationError validationError = new ValidationError();
                validationError.PropertyName = item.Key;
                foreach (ModelError error in item.Value.Errors)
                {
                    validationError.ErrorMessage = error.ErrorMessage;
                }
                validationErrors.Add(validationError);
            }
            return Json(new { success = false, errors = validationErrors }, JsonRequestBehavior.AllowGet);
        }

        public class ValidationError
        {
            public string PropertyName { get; set; }
            public string ErrorMessage { get; set; }
        }

    }
}