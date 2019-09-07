 
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
    public class ComponentController : ComponentBaseController
    {              
        private readonly IComponentService _componentService;        

        public ComponentController(IComponentService componentService) : base(componentService)
        { 
            _componentService = componentService;            
        }

    }
}
