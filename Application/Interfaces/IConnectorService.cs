using System.Collections.Generic;
using Domain;
using Application.Service;
using Application.ViewModels;

namespace Application.Interfaces
{
    public interface IConnectorService : IConnectorServiceBase 
    {
        string LinkDocumentToConnector(int connectorId, int documentId);
        ConnectorDetailVM GetConnectorAndDocuments(int connectorId);
        string RemoveDocumentFromConnector(int connectorId, int documentId);
        UploadDocumentsVM LinkDocuments(UploadDocumentsVM uploadDocumentsVM);
    }
}
