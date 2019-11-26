 
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

    }
}
