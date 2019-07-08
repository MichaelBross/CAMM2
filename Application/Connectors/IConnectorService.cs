using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Connectors
{
    public interface IConnectorService
    {
        IEnumerable<ConnectorListVM> Search(SearchParameters searchParams);
        IEnumerable<ConnectorListVM> GetAll();
        int GetTotalCount();
        ConnectorDetailVM Add(ConnectorDetailVM connectorVM);
        ConnectorDetailVM Update(ConnectorDetailVM connectorVM);
        void Remove(ConnectorDetailVM connectorVM);
    }
}
