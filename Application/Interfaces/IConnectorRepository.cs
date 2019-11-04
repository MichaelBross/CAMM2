using System.Collections.Generic;
using Domain;

namespace Application.Interfaces
{
    public interface IConnectorRepository : IConnectorRepositoryBase 
    {
        Connector GetWithDocuments(int connectorId);
    }    
}
