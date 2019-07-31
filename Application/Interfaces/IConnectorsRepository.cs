using System.Collections.Generic;
using Domain;

namespace Application.Interfaces
{
    public interface IConnectorsRepository : IRepository<Connector>
    {
        IEnumerable<Connector> Search(SearchParameters searchParams);
    }
}
