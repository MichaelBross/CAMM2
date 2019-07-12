using System.Collections.Generic;
using Domain.Items;
using Domain.Users;
using Domain.Documents;
using Domain.Assemblies;

namespace Application.Interfaces
{
    public partial interface IUserRepository : IRepository<User> 
    {
        IEnumerable<User> Search(SearchParameters searchParams);
    }

    public partial interface IDocumentRepository : IRepository<Document> 
    {
        IEnumerable<Document> Search(SearchParameters searchParams);
    }

    public partial interface IComponentRepository : IRepository<Component> 
    {
        IEnumerable<Component> Search(SearchParameters searchParams);
    }

    public partial interface IConnectorRepository : IRepository<Connector> 
    {
        IEnumerable<Connector> Search(SearchParameters searchParams);
    }

    public partial interface IContactRepository : IRepository<Contact> 
    {
        IEnumerable<Contact> Search(SearchParameters searchParams);
    }

    public partial interface IItemRepository : IRepository<Item> 
    {
        IEnumerable<Item> Search(SearchParameters searchParams);
    }

    public partial interface IToolRepository : IRepository<Tool> 
    {
        IEnumerable<Tool> Search(SearchParameters searchParams);
    }

    public partial interface IAssemblyRepository : IRepository<Assembly> 
    {
        IEnumerable<Assembly> Search(SearchParameters searchParams);
    }

    public partial interface IAssemblyComponentRepository : IRepository<AssemblyComponent> 
    {
        IEnumerable<AssemblyComponent> Search(SearchParameters searchParams);
    }

}

