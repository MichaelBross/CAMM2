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

        public AssemblyItem GetAssemblyItemWithAssemblyAndItem(int id)
        {
            return Camm2Context.AssemblyItems
                .Where(a => a.Id == id)
                .Include(a => a.Item)
                .Include(a => a.Assembly)
                .FirstOrDefault();
        }

        public bool IsDuplicate(int assemblyId, int itemId)
        {
            var count = Camm2Context.AssemblyItems
                .Where(a => a.Assembly.Id == assemblyId && a.Item.Id == itemId)
                .Count();

            if (count > 0)
                return true;
            else
                return false;
        }

        public int GetNextLineNumber(int assemblyId)
        {
            var greatestLineNumber = Camm2Context.AssemblyItems
                .Where(a => a.Assembly.Id == assemblyId)
                .OrderByDescending(x => x.LineNumber)
                .FirstOrDefault().LineNumber;

            return greatestLineNumber + 1;
        }
    }
}
