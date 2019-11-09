using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.ViewModels;
using Domain;


namespace Application.Connectors
{
    public class ConnectorService : IConnectorService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public ConnectorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ConnectorDetailVM Add(ConnectorDetailVM connectorVM)
        {
            try
            {
                var newConnector = new Connector();

                Map.AtoB(connectorVM, newConnector);

                var now = DateTime.Now;
                newConnector.CreateDate = now;
                newConnector.UpdateDate = now;

                _unitOfWork.Connectors.Add(newConnector);
                _unitOfWork.Complete();
                                
                Map.AtoB(newConnector, connectorVM);

                return connectorVM;
            }
            catch (Exception ex)
            {  
                var message = Utility.GetRootCauseOfException(ex);

                throw new Exception(message);
            }
        }

        public IEnumerable<ConnectorListVM> GetAll()
        {
            var connectors = _unitOfWork.Connectors.GetAll();
            var connectorVMs = new List<ConnectorListVM>();

            Map.AtoB(connectors, connectorVMs);

            return connectorVMs;
        }

        public int GetTotalCount()
        {
            var count = _unitOfWork.Connectors.GetAll().Count();
            return count;
        }
               
        public void Remove(ConnectorDetailVM connectorVM)
        {
            try
            {               
                var toBeRemoved = _unitOfWork.Connectors.Get(connectorVM.Id);
                if (toBeRemoved == null)
                    throw new Exception("Connector not found.");
                _unitOfWork.Connectors.Remove(toBeRemoved);
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                var message = Utility.GetRootCauseOfException(ex);
                throw new Exception(message);
            }
        }

        public IEnumerable<ConnectorListVM> Search(SearchParameters searchParams)
        {
            var result = _unitOfWork.Connectors.Search(searchParams)
                .Select(i => new ConnectorListVM()
                {
                    Id = i.Id,
                    Code = i.Code,
                    Description = i.Description,
                    QtyOnHand = i.QtyOnHand,
                    UnitsOfMeasure = i.UnitsOfMeasure,
                    CreateDate = i.CreateDate,
                    UpdateDate = i.UpdateDate
                    
                }).ToList();

            return result;
        }

        public ConnectorDetailVM Update(ConnectorDetailVM revisedVM)
        {
            try
            {
                var retrieved = _unitOfWork.Connectors.Get(revisedVM.Id);
                retrieved.Code = revisedVM.Code;
                retrieved.Description = revisedVM.Description;
                retrieved.QtyOnHand = revisedVM.QtyOnHand;
                retrieved.UnitsOfMeasure = revisedVM.UnitsOfMeasure;
                var now = DateTime.Now;
                retrieved.UpdateDate = now;                
                _unitOfWork.Complete();
                return revisedVM;
            }
            catch (Exception ex)
            {
                var message = Utility.GetRootCauseOfException(ex);
                throw new Exception(message);
            }
        }


    }
}
