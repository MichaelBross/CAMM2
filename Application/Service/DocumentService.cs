using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Domain;
using Application;
using Application.Interfaces;
using System.Web.Mvc;
using System.Web;
using System.IO;

namespace Application.Service
{
    public class DocumentService : DocumentServiceBase, IDocumentService
    {
	    private readonly IUnitOfWork _unitOfWork;

        public DocumentService(IUnitOfWork unitOfWork) : base(unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public List<SelectListItem> LinkToSelectList()
        {
            var list = new List<SelectListItem>();

            list.Add(new SelectListItem { Text = "-", Value = "" });
            list.Add(new SelectListItem { Text = "Assembly", Value = "Assembly" });
            list.Add(new SelectListItem { Text = "Component", Value = "Component" });
            list.Add(new SelectListItem { Text = "Connector", Value = "Connector" });
            list.Add(new SelectListItem { Text = "Contact", Value = "Contact" });
            list.Add(new SelectListItem { Text = "Item", Value = "Item" });
            list.Add(new SelectListItem { Text = "Tool", Value = "Tool" });            

            return list;
        }

        public List<DocumentDetailVM> UploadFiles(HttpRequestBase Request)
        {
            HttpFileCollectionBase files = Request.Files;

            var docList = new List<DocumentDetailVM>();

            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase file = files[i];

                var path = GetPath(file, Request);

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

                var savedDoc = Add(documentVM);
                docList.Add(savedDoc);
            }

            return docList;
        }

        private string GetPath(HttpPostedFileBase file, HttpRequestBase Request)
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
            if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(directoryPath)))
            {
                Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(directoryPath));
            }
        }

        private string CreateUniquePath(string path, string uploadDirectory)
        {
            var fileName = Path.GetFileNameWithoutExtension(path);
            var extension = Path.GetExtension(path);
            //string fileName = Guid.NewGuid() + extension;
            string newPath;
            var fullName = fileName + extension;
            newPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(uploadDirectory), fullName);

            var index = 1;
            while (System.IO.File.Exists(newPath) && index < 100)
            {
                fullName = fileName + "(" + index + ")" + extension;
                newPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(uploadDirectory), fullName);
                index++;
            }

            return newPath;
        }

        public List<NameVM> IdentifyDuplicateNames(List<string> fileNames)
        {
            var duplicateNames = new List<NameVM>();
            var uploadDirectory = "~/Uploads/";

            foreach (var fileName in fileNames)
            {
                var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(uploadDirectory), fileName);
                if (System.IO.File.Exists(path))
                {
                    var nameVM = new NameVM();
                    nameVM.Duplicate = fileName;
                    var uniquePath = CreateUniquePath(path, uploadDirectory);
                    nameVM.Unique = Path.GetFileName(uniquePath);
                    duplicateNames.Add(nameVM);
                }
            }

            return duplicateNames;
        }

        public List<DocumentDetailVM> UpdateDocuments(List<DocumentVM> revisedDocuments)
        {
            var updatedDocs = new List<DocumentDetailVM>();

            foreach (var doc in revisedDocuments)
            {
                var existing = Get(doc.Id);
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

                var updated = Update(existing);
                updatedDocs.Add(updated);
            }

            return updatedDocs;
        }
    }
}
