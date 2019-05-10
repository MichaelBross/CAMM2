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
            var jsonKoObject = "vm.Item = {\r\n";
            foreach (var p in viewModel.GetType().GetProperties())
            {
                if (p.PropertyType == typeof(string))
                {
                    jsonKoObject = jsonKoObject + p.Name + ": ko.observable(),\r\n";
                    continue;
                }

                if (p.PropertyType == typeof(DateTime))
                {
                    jsonKoObject = jsonKoObject + p.Name + ": ko.observable(new Date()),\r\n";
                    continue;
                }

                if (p.PropertyType == typeof(int))
                {
                    jsonKoObject = jsonKoObject + p.Name + ": ko.observable(),\r\n";
                    continue;
                }

                if (p.PropertyType.BaseType == typeof(Enum))
                {
                    jsonKoObject = jsonKoObject + p.Name + ": ko.observable(),\r\n";
                }

            }
            jsonKoObject = jsonKoObject + "}\r\n";

            return jsonKoObject;
        }

        public static string ClearModelFunction(object viewModel)
        {
            var clearModelFunc = "vm.clearModel = function () {\r\n";
            foreach (var p in viewModel.GetType().GetProperties())
            {
                if (p.PropertyType == typeof(string))
                {
                    clearModelFunc = clearModelFunc + "vm.Item." + p.Name + "('');\r\n";
                    continue;
                }

                if (p.PropertyType == typeof(DateTime))
                {
                    clearModelFunc = clearModelFunc + "vm.Item." + p.Name + "(vm.today());\r\n";
                    continue;
                }

                if (p.PropertyType == typeof(int))
                {
                    clearModelFunc = clearModelFunc + "vm.Item." + p.Name + "('');\r\n";
                    continue;
                }

                if (p.PropertyType.BaseType == typeof(Enum))
                {
                    clearModelFunc = clearModelFunc + "vm.Item." + p.Name + "('');\r\n";
                }

            }
            clearModelFunc = clearModelFunc + "}";

            return clearModelFunc;
        }

        public static string LoadDetailsFunction(object viewModel)
        {
            var loadDetailsFunc = "vm.loadDetails = function (data) {\r\n";
            loadDetailsFunc += "if (data === undefined){\r\n";
            loadDetailsFunc += "    vm.clearModel();\r\n";
            loadDetailsFunc += "    return;\r\n";
            loadDetailsFunc += "}\r\n";

            foreach (var p in viewModel.GetType().GetProperties())
            {
                if (p.PropertyType == typeof(DateTime))
                {
                    loadDetailsFunc += "vm.Item." + p.Name + "(vm.formatDate(data['" + p.Name + "']));\r\n";
                    continue;
                }
                else
                {
                    loadDetailsFunc += "vm.Item." + p.Name + "(data['" + p.Name + "']);\r\n";
                }


            }
            loadDetailsFunc = loadDetailsFunc + "}";

            return loadDetailsFunc;
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

                if (p.PropertyType == typeof(DateTime))
                {
                    var render = "function(data, type, row, meta) { " +
                        "return vm.formatDate(row['" + p.Name + "']);" +
                        "}";

                    columnsList.Add(new JsColumn(render, p.Name, p.Name, visible));
                    continue;
                }
                
                if (p.PropertyType.BaseType == typeof(Enum))
                {                    
                    var render = "function(data, type, row, meta) { " +
                        "return vm.getEnumItemName('" + p.Name + "', row['" + p.Name + "']);" +
                        "}";

                    columnsList.Add(new JsColumn(render, p.Name, p.Name, visible));
                    continue;
                }

                columnsList.Add(new JsColumn(p.Name, p.Name, p.Name, visible));
            }            
            
            var columnsMvcHtmlString = new MvcHtmlString(JsonConvert.SerializeObject(columnsList));
            var editableVersion = columnsMvcHtmlString.ToString();
            var unwantedString = "data\":\"function";
            var withThis = "render\": function";
            var corrected = editableVersion.Replace(unwantedString, withThis);
            unwantedString = "]);}\"";
            withThis = "]);}";
            var finished = corrected.Replace(unwantedString, withThis);
            //unwantedString = "},";
            //withThis = "},\r\n";
            //finished = corrected.Replace(unwantedString, withThis);
            var backToMvcHtmlString = new MvcHtmlString(finished);


            return backToMvcHtmlString;
        }

        public static string SelectLists(object detailVM)
        {
            var selectLists = "";
            foreach (var p in detailVM.GetType().GetProperties())
            {
                if (p.PropertyType.BaseType == typeof(Enum))
                {
                    var SelectListName = p.Name + "List";
                        
                        selectLists += "vm." + SelectListName + " = ko.observableArray(" + SelectListFromEnum(p.PropertyType) + ")";
                        
                    }

            }
            return selectLists;
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