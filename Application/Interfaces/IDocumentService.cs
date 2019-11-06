using System.Collections.Generic;
using Domain;
using Application.Service;
using System.Web.Mvc;

namespace Application.Interfaces
{
    public interface IDocumentService : IDocumentServiceBase 
    {
        List<SelectListItem> LinkToSelectList();
    }    
}
