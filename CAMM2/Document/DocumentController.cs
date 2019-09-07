 
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
    public class DocumentController : DocumentBaseController
    {              
        private readonly IDocumentService _documentService;        

        public DocumentController(IDocumentService documentService) : base(documentService)
        { 
            _documentService = documentService;            
        }

    }
}
