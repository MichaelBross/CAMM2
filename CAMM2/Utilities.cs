using Application;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        public static string JsonKoObject(object viewModel)
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

        public static string ClearModelFunction(object viewModel)
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

        public static MvcHtmlString TableColumns(object viewModel)
        {
            var columnsList = new List<JsColumn>();
            foreach (var p in viewModel.GetType().GetProperties())
            {
                var visible = true;
                var hiddenInput = p.GetCustomAttribute(typeof(HiddenInputAttribute)) as HiddenInputAttribute;
                if (hiddenInput != null)
                {
                    visible = hiddenInput.DisplayValue;
                }

                if (p.Name == "Id")
                {
                    columnsList.Add(new JsColumn(p.Name, p.Name, p.Name, visible));
                }

                if (p.PropertyType == typeof(string))
                {
                    columnsList.Add(new JsColumn(p.Name, p.Name, p.Name, visible));
                }

                if (p.PropertyType == typeof(DateTime))
                {
                    var render = "function(data, type, row, meta) { " +
                        "return vm.formatDate(row['" + p.Name + "']);" +
                        "}";

                    columnsList.Add(new JsColumn(render, p.Name, p.Name, visible));
                }

                if (p.PropertyType == typeof(int) && p.Name != "Id")
                {
                    columnsList.Add(new JsColumn(p.Name, p.Name, p.Name, visible));
                }

                if (p.PropertyType.BaseType == typeof(Enum))
                {                    
                    var render = "function(data, type, row, meta) { " +
                        "return vm.getEnumItemName('" + p.Name + "', row['" + p.Name + "']);" +
                        "}";

                    columnsList.Add(new JsColumn(render, p.Name, p.Name, visible));
                }

            }            
            
            var columnsMvcHtmlString = new MvcHtmlString(JsonConvert.SerializeObject(columnsList));
            var editableVersion = columnsMvcHtmlString.ToString();
            var unwantedString = "data\":\"function";
            var withThis = "render\": function";
            var corrected = editableVersion.Replace(unwantedString, withThis);
            unwantedString = "]);}\"";
            withThis = "]);}";
            var finished = corrected.Replace(unwantedString, withThis);
            var backToMvcHtmlString = new MvcHtmlString(finished);


            return backToMvcHtmlString;
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

    public class JsColumn
    {
        public JsColumn(string _data, string _name, string _title, bool _visible = true)
        {
            data = _data;
            name = _name;
            title = _title;
            visible = _visible;
        }
        public string data { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public bool visible { get; set; }
    }

}