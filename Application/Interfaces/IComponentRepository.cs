using System.Collections.Generic;
using Domain;

namespace Application.Interfaces
{
    public interface IComponentRepository : IComponentRepositoryBase 
    {
        Component GetInculding(string itemsToInclude, int id);
    }
}
