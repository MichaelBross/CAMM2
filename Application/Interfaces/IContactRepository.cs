using System.Collections.Generic;
using Domain;

namespace Application.Interfaces
{
    public interface IContactRepository : IContactRepositoryBase 
    {
        Contact GetInculding(string itemsToInclude, int id);
    }
}
