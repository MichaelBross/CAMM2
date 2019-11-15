using System.Collections.Generic;
using Domain;
using Application.Service;
using Application.ViewModels;

namespace Application.Interfaces
{
    public interface IConnectorService : IConnectorServiceBase 
    {
        ConnectorDetailVM GetConnectorAndDocuments(int connectorId);
    }
}
