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
            if (uploadDirectory.Contains("~"))
            {
                uploadDirectory = System.Web.HttpContext.Current.Server.MapPath(uploadDirectory);
            }
            newPath = Path.Combine(uploadDirectory, fullName);

            var index = 1;
            while (System.IO.File.Exists(newPath) && index < 1000)
            {
                fullName = fileName + "(" + index + ")" + extension;
                newPath = Path.Combine(uploadDirectory, fullName);
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

            foreach (var revisedVM in revisedDocuments)
            {
                var retrieved = Get(revisedVM.Id);
                if (revisedVM.FileName != retrieved.FileName)
                {
                    retrieved.Path = ChangeFileName(revisedVM.FileName, retrieved.Path);
                }

                retrieved.Code = revisedVM.Code;
                retrieved.DocType = (Domain.DocumentType)Enum.ToObject(typeof(Domain.DocumentType), revisedVM.DocType);
                retrieved.FileName = revisedVM.FileName;
                retrieved.Rev = revisedVM.Rev;
                retrieved.Title = revisedVM.Title;

                var updated = Update(retrieved);
                updatedDocs.Add(updated);
            }

            return updatedDocs;
        }

        private string ChangeFileName(string newFileName, string existingPath)
        {            
            var directory = Path.GetDirectoryName(existingPath);
            var newPath = Path.Combine(directory, newFileName);
            newPath = CreateUniquePath(newPath, directory);
            if (!System.IO.File.Exists(newPath))
            {
                System.IO.File.Move(existingPath, newPath);                
            }
            else
            {
                throw new Exception("Unique path creation failed for file name " + newFileName + ".");
            }
            return newPath;
        }

        public new DocumentDetailVM Update(DocumentDetailVM revisedVM)
        {
            try
            {
                var retrieved = _unitOfWork.Documents.Get(revisedVM.Id);

                retrieved.IsObsolete = revisedVM.IsObsolete;
                retrieved.CreateDate = revisedVM.CreateDate;
                retrieved.CreatedBy = revisedVM.CreatedBy;
                retrieved.UpdateDate = revisedVM.UpdateDate;
                retrieved.UpdatedBy = revisedVM.UpdatedBy;
                retrieved.Id = revisedVM.Id;
                retrieved.Code = revisedVM.Code;
                retrieved.Rev = revisedVM.Rev;
                retrieved.Title = revisedVM.Title;                
                if (revisedVM.FileName != retrieved.FileName)
                {
                    retrieved.Path = ChangeFileName(revisedVM.FileName, retrieved.Path);
                }
                retrieved.FileName = revisedVM.FileName;
                retrieved.DocType = revisedVM.DocType;
                var Itemlist = new List<Item>();
                if (revisedVM.Items != null)
                {
                    foreach (ItemListVM vm in revisedVM.Items)
                    {
                        var item = new Item();
                        Map.AtoB(vm, item);
                        Itemlist.Add(item);
                    }
                }
                retrieved.Items = Itemlist;
                var now = DateTime.Now;
                retrieved.UpdateDate = now;
                _unitOfWork.Complete();
                return revisedVM;
            }
            catch (Exception ex)
            {
                var message = Utility.GetRootCauseOfException(ex);
                throw new Exception(message);
            }
        }

        public List<DocumentListVM> GetDocumentsLinkedToItem(string type, int typeId)
        {
            var item = new Item();

            switch (type)
            {
                case "Assembly":
                    item = _unitOfWork.Assemblys.GetInculding("Documents", typeId);
                    break;
                case "Connector":
                    item = _unitOfWork.Connectors.GetInculding("Documents", typeId);
                    break;
                case "Contact":
                    item = _unitOfWork.Contacts.GetInculding("Documents", typeId);
                    break;
                case "Tool":
                    item = _unitOfWork.Tools.GetInculding("Documents", typeId);
                    break;
                default:
                    throw new Exception("Type " + type + " not found.");
            }

            var documentVMList = new List<DocumentListVM>();
            Map.ListToList(item.Documents.ToList(), documentVMList);

            return documentVMList;
        }

        public string LinkToItem(string type, int typeId, int documentId)
        {
            var item = new Item();

            switch (type)
            {
                case "Assembly":
                    item = _unitOfWork.Assemblys.GetInculding("Documents", typeId);
                    break;
                case "Connector":
                    item = _unitOfWork.Connectors.GetInculding("Documents", typeId);
                    break;
                case "Contact":
                    item = _unitOfWork.Contacts.GetInculding("Documents", typeId);
                    break;
                case "Tool":
                    item = _unitOfWork.Tools.GetInculding("Documents", typeId);
                    break;
                default:
                    throw new Exception("Type " + type + " not found.");                    
            }

            if (item.Documents == null)
            {
                item.Documents = new List<Document>();
            }
            else
            {
                if (item.Documents.Where(d => d.Id == documentId).Count() > 0)
                    return "duplicate";
            }

            var document = _unitOfWork.Documents.Get(documentId);
            item.Documents.Add(document);
            _unitOfWork.Complete();
            return "success";
        }

        public string UnlinkDocument(string type, int typeId, int documentId)
        {
            var item = new Item();

            switch (type)
            {
                case "Assembly":
                    item = _unitOfWork.Assemblys.GetInculding("Documents", typeId);
                    break;
                case "Connector":
                    item = _unitOfWork.Connectors.GetInculding("Documents", typeId);
                    break;
                case "Contact":
                    item = _unitOfWork.Contacts.GetInculding("Documents", typeId);
                    break;
                case "Tool":
                    item = _unitOfWork.Tools.GetInculding("Documents", typeId);
                    break;
                default:
                    throw new Exception("Type " + type + " not found.");
            }

            var document = _unitOfWork.Documents.Get(documentId);
            item.Documents.Remove(document);
            _unitOfWork.Complete();
            return "success";
        }
    }
}
