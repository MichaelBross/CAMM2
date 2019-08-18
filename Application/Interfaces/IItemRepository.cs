using System.Collections.Generic;
using Domain;

namespace Application.Interfaces
{
    public interface IItemRepository : IItemRepositoryBase 
    {
        IList<Item> GetObsoleteItems();

    }
}
