 
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
        private readonly IConnectorService _connectorService;

        public DocumentController(IDocumentService documentService, IConnectorService connectorService) : base(documentService)
        { 
            _documentService = documentService;
            _connectorService = connectorService;
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

        public ActionResult Linked(string type, int id, string code)
        {
            var parentVM = new ParentItemVM
            {
                Type = type,
                Id = id,
                Code = code
            };

            return View(parentVM);
        }

        [HttpPost]
        public JsonResult GetDocuments(int connectorId)
        {
            var connector = _connectorService.GetConnectorAndDocuments(connectorId);
            var documents = connector.Documents;
            return Json(new { data = documents, draw = Request["draw"] }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetDocumentsLinkedToItem(string type, int typeId)
        {
            var documents = _documentService.GetDocumentsLinkedToItem(type, typeId);

            return Json(new { data = documents, draw = Request["draw"] }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult LinkDocuments(string type, int typeId, IEnumerable<int> documentIdList)
        {           
            foreach (int documentId in documentIdList)
            {
                _documentService.LinkToItem(type, typeId, documentId);
            }

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveDocuments(string type, int typeId, IEnumerable<int> documentIdList)
        {
            var result = "success";
            foreach (int documentId in documentIdList)
            {
                if (_documentService.UnlinkDocument(type, typeId, documentId) != "success")
                {
                    result = "Failed";
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
