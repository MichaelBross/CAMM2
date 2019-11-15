using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Domain;
using Application;
using Application.Interfaces;
using Application.ViewModels;

namespace Application.Service
{
    public class ConnectorService : ConnectorServiceBase, IConnectorService
    {
	    private readonly IUnitOfWork _unitOfWork;

        public ConnectorService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ConnectorDetailVM GetConnectorAndDocuments(int connectorId)
        {
            var vm = new ConnectorDetailVM();
            vm.Documents = new List<DocumentListVM>();
            var connector = _unitOfWork.Connectors.GetWithDocuments(connectorId);
            Map.AtoB(connector, vm);
            foreach(Document doc in connector.Documents)
            {
                var docVm = new DocumentListVM();
                Map.AtoB(doc, docVm);
                vm.Documents.Add(docVm);
            }
            return vm;
        }
    }
}
