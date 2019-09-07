 
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
    public class ToolController : ToolBaseController
    {              
        private readonly IToolService _toolService;        

        public ToolController(IToolService toolService) : base(toolService)
        { 
            _toolService = toolService;            
        }

    }
}
