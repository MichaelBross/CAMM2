 
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
    public class AssemblyItemController : AssemblyItemBaseController
    {              
        private readonly IAssemblyItemService _assemblyitemService;        

        public AssemblyItemController(IAssemblyItemService assemblyitemService) : base(assemblyitemService)
        { 
            _assemblyitemService = assemblyitemService;            
        }

    }
}
