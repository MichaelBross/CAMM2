using System.Collections.Generic;
using Domain;
using Application.Service;
using System.Web.Mvc;
using System.Web;

namespace Application.Interfaces
{
    public interface IDocumentService : IDocumentServiceBase 
    {
        List<SelectListItem> LinkToSelectList();
        List<DocumentDetailVM> UploadFiles(HttpRequestBase Request);
        List<NameVM> IdentifyDuplicateNames(List<string> fileNames);
        List<DocumentDetailVM> UpdateDocuments(List<DocumentVM> revisedDocuments);
        string LinkToItem(string type, int typeId, int documentId);
        string UnlinkDocument(string type, int typeId, int documentId);
        List<Document> GetDocumentsLinkedToItem(string type, int typeId);
    }    
}
