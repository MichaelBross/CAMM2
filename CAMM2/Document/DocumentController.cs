 
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
            var uploadDocuments = new UploadDocumentsVM();
            uploadDocuments.LinkToType = linkToType;
            uploadDocuments.LinkToId = linkToId;
            uploadDocuments.LinkToCD = linkToCD;
            uploadDocuments.ItemSelectList = _documentService.LinkToSelectList();

            return View(uploadDocuments);
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {

            if (Request.Files.Count == 0)
            {
                return Json("Failed to upload files.");
            }

            HttpFileCollectionBase files = Request.Files;

            var docList = new List<DocumentDetailVM>();

            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase file = files[i];

                var path = GetPath(file);

                var uploadDirectory = "~/Uploads/";

                CreateDirectoryIfNotPresent(uploadDirectory);

                var newPath = CreateUniquePath(path, uploadDirectory);

                file.SaveAs(newPath);

                var fileNameWithoutExtention = Path.GetFileNameWithoutExtension(newPath);

                var documentVM = new DocumentDetailVM()
                {
                    FileName = Path.GetFileName(newPath),
                    Path = newPath,
                    Code = fileNameWithoutExtention,
                    Rev = "-",
                    Title = fileNameWithoutExtention                                       
                };

                var savedDoc = _documentService.Add(documentVM);
                docList.Add(savedDoc);
            }

             return Json(docList);
        }

        private string GetPath(HttpPostedFileBase file)
        {
            string fname;
            if (BrowserIsInternetExplorer(Request))
            {
                string[] testfiles = file.FileName.Split(new char[] { '\\' });
                fname = testfiles[testfiles.Length - 1];
            }
            else
            {
                fname = file.FileName;
            }

            return fname;
        }

        private bool BrowserIsInternetExplorer(HttpRequestBase Request)
        {
            return Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER";
        }

        private void CreateDirectoryIfNotPresent(string directoryPath)
        {
            if (!Directory.Exists(Server.MapPath(directoryPath)))
            {
                Directory.CreateDirectory(Server.MapPath(directoryPath));
            }
        }

        private string CreateUniquePath(string path, string uploadDirectory)
        {
            var fileName = Path.GetFileNameWithoutExtension(path);
            var extension = Path.GetExtension(path);
            //string fileName = Guid.NewGuid() + extension;
            string newPath;
            var fullName = fileName + extension;
            newPath = Path.Combine(Server.MapPath(uploadDirectory), fullName);

            var index = 1;
            while (System.IO.File.Exists(newPath) && index < 100)
            {
                fullName = fileName + "(" + index + ")" + extension;
                newPath = Path.Combine(Server.MapPath(uploadDirectory), fullName);
                index++;
            }

            return newPath;
        }

        [HttpPost]
        public JsonResult IdentifyDuplicateNames(List<string> fileNames)
        {
            var duplicateNames = new List<NameVM>();
            var uploadDirectory = "~/Uploads/";

            foreach(var fileName in fileNames)
            {
                var path = Path.Combine(Server.MapPath(uploadDirectory), fileName);
                if (System.IO.File.Exists(path))
                {
                    var nameVM = new NameVM();
                    nameVM.Duplicate = fileName;
                    var uniquePath = CreateUniquePath(path, uploadDirectory);
                    nameVM.Unique = Path.GetFileName(uniquePath);
                    duplicateNames.Add(nameVM);
                }
            }

            return Json(duplicateNames, JsonRequestBehavior.AllowGet);
        }

        //DocumentVM[] docs

        [HttpPost]
        public JsonResult UpdateDocuments(List<DocumentVM> revisedDocuments)
        {
            var updatedDocs = new List<DocumentDetailVM>();
            
            foreach (var doc in revisedDocuments)
            {
                var existing = _documentService.Get(doc.Id);
                if (doc.FileName != existing.FileName)
                {
                    var directory = Path.GetDirectoryName(existing.Path);
                    var newPath = Path.Combine(directory, doc.FileName);
                    if (!System.IO.File.Exists(newPath))
                    {
                        System.IO.File.Move(existing.Path, newPath);
                        existing.Path = newPath;
                    }

                }

                existing.Code = doc.Code;
                existing.DocType = (Domain.DocumentType)Enum.ToObject(typeof(Domain.DocumentType), doc.DocType);
                existing.FileName = doc.FileName;
                existing.Rev = doc.Rev;
                existing.Title = doc.Title;

                var updated = _documentService.Update(existing);
                updatedDocs.Add(updated);
            }

            return Json(updatedDocs, JsonRequestBehavior.AllowGet);
        }
    }
}
