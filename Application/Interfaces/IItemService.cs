using System.Collections.Generic;
using Domain;
using Application.Service;
using System.Web.Mvc;

namespace Application.Interfaces
{
    public interface IItemService : IItemServiceBase 
    {
        List<SelectListItem> LinkToSelectList();
        string LinkToAssembly(int assemblyId, int itemId);
        List<BOMListVM> GetAssemblyItems(int assemblyId);
        string UnlinkItem(int assemblyId, int itemId);
    }
}
