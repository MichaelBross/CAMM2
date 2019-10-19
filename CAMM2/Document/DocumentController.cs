 
using System;
using System.Collections.Generic;
using System.IO;
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

        public ActionResult UploadDocuments()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {

            if (Request.Files.Count == 0)
            {
                return Json("Failed to upload files.");
            }

            HttpFileCollectionBase files = Request.Files;

            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase file = files[i];

                var path = GetPath(file);

                var uploadDirectory = "~/Uploads/";

                CreateDirectoryIfNotPresent(uploadDirectory);

                var newPath = CreateUniquePath(path, uploadDirectory);

                file.SaveAs(newPath);

                //Here you can write code for save this information in your database if you want
            }
            return Json("file uploaded successfully");
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
    }
}
