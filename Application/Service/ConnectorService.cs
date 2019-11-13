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

        public string LinkDocumentToConnector(int connectorId, int documentId)
        {
            var result = "failed";

            try
            {
                var document = _unitOfWork.Documents.Get(documentId);
                var conn = _unitOfWork.Connectors.Get(connectorId);
                var duplicate = false;

                if (conn.Documents == null)
                {
                    conn.Documents = new List<Document>();
                }
                else
                {
                    duplicate = conn.Documents.Where(d => d.Id == documentId).Count() > 0;
                }

                if (duplicate)
                {
                    result = "duplicate";
                }
                else
                {
                    conn.Documents.Add(document);
                    _unitOfWork.Complete();
                    result = "success";
                }
            }
            catch (Exception ex)
            {
                result = "failed";
            }

            return result;
        }

        public string RemoveDocumentFromConnector(int connectorId, int documentId)
        {
            var result = "failed";

            try
            {
                var connAndDocuments = _unitOfWork.Connectors.GetWithDocuments(connectorId);
                var targetDocument = connAndDocuments.Documents.Where(d => d.Id == documentId).FirstOrDefault();
                if (connAndDocuments == null || targetDocument == null)
                {
                    return result;
                }

                connAndDocuments.Documents.Remove(targetDocument);
                _unitOfWork.Complete();
                result = "success";
            }
            catch (Exception ex)
            {
                result = "failed";
            }

            return result;
        }

        public UploadDocumentsVM LinkDocuments(UploadDocumentsVM uploadDocumentsVM)
        {
            foreach (var idString in uploadDocumentsVM.DocumentIdList)
            {
                var id = int.Parse(idString);

                LinkDocumentToConnector(uploadDocumentsVM.LinkToId, id);
            }

            return uploadDocumentsVM;
        }
    }
}
