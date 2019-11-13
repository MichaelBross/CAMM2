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
    public class ItemRepository : ItemRepositoryBase, IItemRepository
    {
        public ItemRepository(Camm2Context context)
            :base(context)
        {
        }

        public IList<Item> GetObsoleteItems()
        {
            var list = Camm2Context.Items.Where(i => i.IsObsolete == true).ToList();
            return list;
        }

        public Item GetInculding(string itemsToInclude, int id)
        {
            return _entity.Include(itemsToInclude).Where(c => c.Id == id).FirstOrDefault();
        }
    }
}
