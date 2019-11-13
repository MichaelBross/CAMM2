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
    public class ContactRepository : ContactRepositoryBase, IContactRepository
    {
        public ContactRepository(Camm2Context context)
            :base(context)
        {
        }

        public Contact GetInculding(string itemsToInclude, int id)
        {
            return _entity.Include(itemsToInclude).Where(c => c.Id == id).FirstOrDefault();
        }
    }
}
