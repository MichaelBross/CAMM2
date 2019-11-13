using System.Collections.Generic;
using Domain;

namespace Application.Interfaces
{
    public interface IItemRepository : IItemRepositoryBase 
    {
        Item GetInculding(string itemsToInclude, int id);
    }
}
