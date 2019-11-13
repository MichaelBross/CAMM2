using System.Collections.Generic;
using Domain;

namespace Application.Interfaces
{
    public interface IToolRepository : IToolRepositoryBase 
    {
        Tool GetInculding(string itemsToInclude, int id);
    }
}
