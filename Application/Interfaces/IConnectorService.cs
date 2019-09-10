using System.Collections.Generic;
using Domain;
using Application.Service;

namespace Application.Interfaces
{
    public interface IConnectorService : IConnectorServiceBase 
    {
        string LinkDocumentToConnector(int connectorId, int documentId);
    }
}