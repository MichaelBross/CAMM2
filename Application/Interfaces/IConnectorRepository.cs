using System.Collections.Generic;
using Domain;

namespace Application.Interfaces
{
    public interface IConnectorRepository : IConnectorRepositoryBase 
    {        
        Connector GetInculding(string itemsToInclude, int id);
    }    
}
