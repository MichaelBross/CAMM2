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
            throw new NotImplementedException();
        }
    }
}
