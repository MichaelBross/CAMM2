// Generated Code! Do not manually edit. Adjust template (UnitOfWork.tt) to make changes to this file.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Persistance;

namespace Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Camm2Context _context;

        public UnitOfWork(Camm2Context context)
        {
            _context = context;
            Users = new UserRepository(_context);            
            Documents = new DocumentRepository(_context);            
            Components = new ComponentRepository(_context);            
            Connectors = new ConnectorRepository(_context);            
            Contacts = new ContactRepository(_context);            
            Items = new ItemRepository(_context);            
            Tools = new ToolRepository(_context);            
            Assemblys = new AssemblyRepository(_context);            
            AssemblyItems = new AssemblyItemRepository(_context);            
        }

        public IUserRepository Users { get; private set; }        
        public IDocumentRepository Documents { get; private set; }        
        public IComponentRepository Components { get; private set; }        
        public IConnectorRepository Connectors { get; private set; }        
        public IContactRepository Contacts { get; private set; }        
        public IItemRepository Items { get; private set; }        
        public IToolRepository Tools { get; private set; }        
        public IAssemblyRepository Assemblys { get; private set; }        
        public IAssemblyItemRepository AssemblyItems { get; private set; }        
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
