using System.Collections.Generic;
using Domain;
using Application.Service;
using System.Web.Mvc;

namespace Application.Interfaces
{
    public interface IToolService : IToolServiceBase 
    {
        List<SelectListItem> LinkToSelectList();
        string LinkToItem(string type, int typeId, int toolId);
    }
}
