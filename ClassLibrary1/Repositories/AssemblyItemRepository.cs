using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using Persistance.Repository;
using Domain;
using Application;
using Application.Interfaces;

namespace Persistance
{
    public class AssemblyItemRepository : AssemblyItemRepositoryBase, IAssemblyItemRepository
    {
        public AssemblyItemRepository(Camm2Context context)
            : base(context)
        {
        }

        public AssemblyItem GetInculding(string itemsToInclude, int id)
        {
            return _entity.Include(itemsToInclude).Where(c => c.Id == id).FirstOrDefault();
        }

        public List<AssemblyItem> GetAssemblyItemsWithItems(int assemblyId)
        {
            return Camm2Context.AssemblyItems
                .Where(a => a.Assembly.Id == assemblyId)
                .Include(a => a.Item)
                .ToList();
        }
    }
}
