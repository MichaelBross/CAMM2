 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application;
using Application.Interfaces;
using Application.Service;
using Application.ViewModels;

namespace Presentation
{ 
    public class ToolController : ToolBaseController
    {              
        private readonly IToolService _toolService;        

        public ToolController(IToolService toolService) : base(toolService)
        { 
            _toolService = toolService;            
        }

        public ActionResult Linked(string type, int id, string code)
        {
            var parentVM = new ParentItemVM
            {
                Type = type,
                Id = id,
                Code = code,
                ItemSelectList = _toolService.LinkToSelectList()
            };

            return View(parentVM);
        }

        public JsonResult GetToolsLinkedToItem(string type, int typeId)
        {

            var tools = _toolService.GetToolsLinkedToItem(type, typeId);

            return Json(new { data = tools, draw = Request["draw"] }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LinkTools(string type, int typeId, IEnumerable<int> toolIdList)
        {
            foreach (int toolId in toolIdList)
            {
                _toolService.LinkToItem(type, typeId, toolId);
            }

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveToolLinks(string type, int typeId, IEnumerable<int> toolIdList)
        {
            var result = "success";
            foreach (int toolId in toolIdList)
            {
                if (_toolService.UnlinkTool(type, typeId, toolId) != "success")
                {
                    result = "Failed";
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
