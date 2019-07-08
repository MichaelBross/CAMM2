using System.Collections.Generic;
using Domain.Items;

namespace Application.Interfaces
{
    public interface IConnectorsRepository : IRepository<Connector>
    {
        IEnumerable<Connector> Search(SearchParameters searchParams);
    }
}
