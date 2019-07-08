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
            Items = new ItemsRepository(_context);
            Connectors = new ConnectorsRepository(_context);
        }

        public IItemsRepository Items { get; private set; }
        public IConnectorsRepository Connectors { get; set; }

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
