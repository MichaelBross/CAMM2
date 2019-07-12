
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; } 
        
        IDocumentRepository Documents { get; } 
        
        IComponentRepository Components { get; } 
        
        IConnectorRepository Connectors { get; } 
        
        IContactRepository Contacts { get; } 
        
        IItemRepository Items { get; } 
        
        IToolRepository Tools { get; } 
        
        IAssemblyRepository Assemblys { get; } 
        
        IAssemblyComponentRepository AssemblyComponents { get; } 
        
        int Complete();
    }
}

