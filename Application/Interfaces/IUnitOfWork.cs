﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IItemsRepository Items { get; } 
        IConnectorsRepository Connectors { get; }
        int Complete();
    }
}
