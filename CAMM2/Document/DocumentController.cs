 
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application;
using Application.Interfaces;
using Application.Service;
using Application.ViewModels;

namespace Presentation
{ 
    public class DocumentController : DocumentBaseController
    {              
        private readonly IDocumentService _documentService;        

        public DocumentController(IDocumentService documentService) : base(documentService)
        { 
            _documentService = documentService;            
        }

        public ActionResult UploadDocuments(string linkToType = "", int linkToId = 0, string linkToCD = "")
        {
            var uploadDocuments = new UploadDocumentsVM
            {
                LinkToType = linkToType,
                LinkToId = linkToId,
                LinkToCD = linkToCD,
                ItemSelectList = _documentService.LinkToSelectList()
            };

            return View(uploadDocuments);
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {

            if (Request.Files.Count == 0)
            {
                return Json("Failed to upload files.");
            }

            var docList = _documentService.UploadFiles(Request);

             return Json(docList);
        }
                                    
        [HttpPost]
        public JsonResult IdentifyDuplicateNames(List<string> fileNames)
        {
            List<NameVM> duplicateNames = _documentService.IdentifyDuplicateNames(fileNames);

            return Json(duplicateNames, JsonRequestBehavior.AllowGet);
        }

        //DocumentVM[] docs

        [HttpPost]
        public JsonResult UpdateDocuments(List<DocumentVM> revisedDocuments)
        {
            List<DocumentDetailVM> updatedDocs = _documentService.UpdateDocuments(revisedDocuments);

            return Json(updatedDocs, JsonRequestBehavior.AllowGet);
        }
    }
}
