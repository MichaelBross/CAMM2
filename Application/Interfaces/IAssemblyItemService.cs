using System.Collections.Generic;
using Domain;
using Application.Service;
using System.Web.Mvc;

namespace Application.Interfaces
{
    public interface IAssemblyItemService : IAssemblyItemServiceBase 
    {
        List<SelectListItem> LinkToSelectList();
        string LinkToAssembly(AssemblyItemVM assemblyItemVM);
        List<AssemblyItemVM> GetAssemblyItems(int assemblyId);
        string UnlinkItem(int assemblyItemId);
        AssemblyItemVM Update(AssemblyItemVM assemblyItemVM);
    }
}
