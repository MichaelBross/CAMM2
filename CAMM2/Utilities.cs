using Application;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
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
       
    public class Build
    {
        public static string JsonKoObjectFromViewModel(object viewModel)
        {
            var jsonKoObject = "{";
            foreach (var p in viewModel.GetType().GetProperties())
            {
                if (p.PropertyType == typeof(string))
                {
                    jsonKoObject = jsonKoObject + p.Name + ": ko.observable(),";
                }

                if (p.PropertyType == typeof(DateTime))
                {
                    jsonKoObject = jsonKoObject + p.Name + ": ko.observable(new Date()),";
                }

                if (p.PropertyType == typeof(int))
                {
                    jsonKoObject = jsonKoObject + p.Name + ": ko.observable(),";
                }

                if (p.PropertyType.BaseType == typeof(Enum))
                {
                    jsonKoObject = jsonKoObject + p.Name + ": ko.observable(),";
                }

            }
            jsonKoObject = jsonKoObject + "}";

            return jsonKoObject;
        }

        public static string ClearModelFunctionFromViewModel(object viewModel)
        {
            var jsonKoObject = "{";
            foreach (var p in viewModel.GetType().GetProperties())
            {
                if (p.PropertyType == typeof(string))
                {
                    jsonKoObject = jsonKoObject + "vm.Item." + p.Name + "('');";
                }

                if (p.PropertyType == typeof(DateTime))
                {
                    jsonKoObject = jsonKoObject + "vm.Item." + p.Name + "(vm.today());";
                }

                if (p.PropertyType == typeof(int))
                {
                    jsonKoObject = jsonKoObject + "vm.Item." + p.Name + "('');";
                }

                if (p.PropertyType.BaseType == typeof(Enum))
                {
                    jsonKoObject = jsonKoObject + "vm.Item." + p.Name + "('');";
                }

            }
            jsonKoObject = jsonKoObject + "}";

            return jsonKoObject;
        }

        public static MvcHtmlString SelectListFromEnum(Type type)
        {
            var enumList = from Object e in Enum.GetValues(type)
                           select new enumItem
                           {
                               id = (int)e,
                               name = e.ToString()
                           };

            return new MvcHtmlString(JsonConvert.SerializeObject(enumList));
        }
    }

    public class enumItem
    {
        public int id { get; set; }
        public string name { get; set; }
    }

}