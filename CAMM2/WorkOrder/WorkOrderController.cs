 
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
    public class WorkOrderController : WorkOrderBaseController
    {              
        private readonly IWorkOrderService _workorderService;        

        public WorkOrderController(IWorkOrderService workorderService) : base(workorderService)
        { 
            _workorderService = workorderService;            
        }

    }
}
