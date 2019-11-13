using System.Collections.Generic;
using Domain;

namespace Application.Interfaces
{
    public interface IAssemblyRepository : IAssemblyRepositoryBase 
    {
        Assembly GetInculding(string itemsToInclude, int id);
    }
}
