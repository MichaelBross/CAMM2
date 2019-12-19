
// Generated Code! Do not manually edit. Adjust template (BaseRepositoryInterfaces.tt) to make changes to this file.
using System.Collections.Generic;
using Domain;

namespace Application.Interfaces
{
    public partial interface IUserRepositoryBase : IRepository<User> 
    {
        IEnumerable<User> Search(SearchParameters searchParams);
		int SearchResultsCount(SearchParameters searchParams);
    }

    public partial interface IDocumentRepositoryBase : IRepository<Document> 
    {
        IEnumerable<Document> Search(SearchParameters searchParams);
		int SearchResultsCount(SearchParameters searchParams);
    }

    public partial interface IComponentRepositoryBase : IRepository<Component> 
    {
        IEnumerable<Component> Search(SearchParameters searchParams);
		int SearchResultsCount(SearchParameters searchParams);
    }

    public partial interface IConnectorRepositoryBase : IRepository<Connector> 
    {
        IEnumerable<Connector> Search(SearchParameters searchParams);
		int SearchResultsCount(SearchParameters searchParams);
    }

    public partial interface IContactRepositoryBase : IRepository<Contact> 
    {
        IEnumerable<Contact> Search(SearchParameters searchParams);
		int SearchResultsCount(SearchParameters searchParams);
    }

    public partial interface IItemRepositoryBase : IRepository<Item> 
    {
        IEnumerable<Item> Search(SearchParameters searchParams);
		int SearchResultsCount(SearchParameters searchParams);
    }

    public partial interface IToolRepositoryBase : IRepository<Tool> 
    {
        IEnumerable<Tool> Search(SearchParameters searchParams);
		int SearchResultsCount(SearchParameters searchParams);
    }

    public partial interface IAssemblyRepositoryBase : IRepository<Assembly> 
    {
        IEnumerable<Assembly> Search(SearchParameters searchParams);
		int SearchResultsCount(SearchParameters searchParams);
    }

    public partial interface IAssemblyItemRepositoryBase : IRepository<AssemblyItem> 
    {
        IEnumerable<AssemblyItem> Search(SearchParameters searchParams);
		int SearchResultsCount(SearchParameters searchParams);
    }

    public partial interface IWorkOrderRepositoryBase : IRepository<WorkOrder> 
    {
        IEnumerable<WorkOrder> Search(SearchParameters searchParams);
		int SearchResultsCount(SearchParameters searchParams);
    }

}
