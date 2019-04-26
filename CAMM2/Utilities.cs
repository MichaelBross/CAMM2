using Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Maps Http Request parameters from jquery datatable ajax 
        /// to the SearchParameters object which is used to pass 
        /// Start, Length, SearchValue, SortColumn, and SortDirection parameters
        /// to Application and Repository search functions.
        /// </summary>
        /// <param name="Request"></param>
        /// <returns>SearchParameters object with Start, Length, SearchValue, SortColumn, and SortDirection properties</returns>
        public static SearchParameters MapDataTableRequestToSearchParams(HttpRequestBase Request)
        {
            var searchParams = new SearchParameters
            {
                Start = Convert.ToInt32(Request["start"]),
                Length = Convert.ToInt32(Request["length"]),
                SearchValue = Request["search[value]"],
                SortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"],
                SortDirection = Request["order[0][dir]"]
            };
            return searchParams;
        }

        public JsonResult JsonErrorResult()
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