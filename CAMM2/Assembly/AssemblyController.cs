 
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
    public class AssemblyController : AssemblyBaseController
    {              
        private readonly IAssemblyService _assemblyService;        

        public AssemblyController(IAssemblyService assemblyService) : base(assemblyService)
        { 
            _assemblyService = assemblyService;            
        }

    }
}
