using System.Collections.Generic;
using Domain;
using Application.Service;
using System.Web.Mvc;

namespace Application.Interfaces
{
    public interface IAssemblyItemService : IAssemblyItemServiceBase 
    {
        List<SelectListItem> LinkToSelectList();
        AssemblyItemVM LinkToAssembly(AssemblyItemVM assemblyItemDetailVM);
        List<AssemblyItemVM> GetAssemblyItems(int assemblyId);
        string UnlinkItem(int assemblyItemId);
    }
}
