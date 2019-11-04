 
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
    public class ConnectorController : ConnectorBaseController
    {              
        private readonly IConnectorService _connectorService;        

        public ConnectorController(IConnectorService connectorService) : base(connectorService)
        { 
            _connectorService = connectorService;            
        }

        public ActionResult ConnectorDocuments(int connectorId)
        {
            var connector = _connectorService.GetConnectorAndDocuments(connectorId);
            return View(connector);
        }

        [HttpPost]
        public JsonResult GetDocuments(int connectorId)
        {
            var connector = _connectorService.GetConnectorAndDocuments(connectorId);
            var documents = connector.Documents;            
            return Json(new { data = documents, draw = Request["draw"] }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddExistingDocuments(int connectorId, IEnumerable<int> documentIdList)
        {  
            var result = "Success";
            foreach(int documentId in documentIdList)
            {
                if(_connectorService.LinkDocumentToConnector(connectorId, documentId) != "Success")
                {
                    result = "Failed";
                }
            }
            
            if (result == "Success")
            {
                return RedirectToAction("ConnectorDocuments", "Connector", new { connectorId });
            }
            else
            {
                return JsonErrorResult();
            }
        }

        public ActionResult RemoveDocuments(int connectorId, IEnumerable<int> documentIdList)
        {
            var result = "Success";
            foreach (int documentId in documentIdList)
            {
                if (_connectorService.RemoveDocumentFromConnector(connectorId, documentId) != "Success")
                {
                    result = "Failed";
                }
            }

            if (result == "Success")
            {
                return RedirectToAction("ConnectorDocuments", "Connector", new { connectorId });
            }
            else
            {
                return JsonErrorResult();
            }
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {

            if(Request.Files.Count == 0)
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

        public ActionResult GetConnectors(string term)
        {
            var searchParams = new SearchParameters();
            searchParams.SearchValue = term;
            searchParams.SortColumnName = "Code";
            searchParams.Length = 10;
            var result = _connectorService.Search(searchParams).Select(i => new
            {
                label = i.Code,
                id = i.Id
            }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
