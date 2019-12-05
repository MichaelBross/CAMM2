using System.Collections.Generic;
using Domain;

namespace Application.Interfaces
{
    public interface IAssemblyItemRepository : IAssemblyItemRepositoryBase 
    {
        AssemblyItem GetInculding(string itemsToInclude, int id);
        List<AssemblyItem> GetAssemblyItemsWithItems(int assemblyId);
    }
}
