using System.Collections.Generic;
using Domain;
using Application.Service;

namespace Application.Interfaces
{
    public interface IConnectorService : IConnectorServiceBase 
    {
        string LinkDocumentToConnector(int connectorId, int documentId);
        ConnectorDetailVM GetConnectorAndDocuments(int connectorId);

        string RemoveDocumentFromConnector(int connectorId, int documentId);
    }
}
