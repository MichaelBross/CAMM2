using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Domain;
using Application;
using Application.Interfaces;

namespace Application.Service
{
    public class ConnectorService : ConnectorServiceBase, IConnectorService
    {
	    private readonly IUnitOfWork _unitOfWork;

        public ConnectorService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string LinkDocumentToConnector(int connectorId, int documentId)
        {
            var result = "failed";

            try
            {
                var document = _unitOfWork.Documents.Get(documentId);
                var conn = _unitOfWork.Connectors.Get(connectorId);
                var duplicate = false;

                if(conn.Documents == null)
                {
                    conn.Documents = new List<Document>();
                }
                else
                {
                    duplicate = conn.Documents.Where(d => d.Id == documentId).Count() > 0;                    
                }

                if(duplicate)
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
	}
}