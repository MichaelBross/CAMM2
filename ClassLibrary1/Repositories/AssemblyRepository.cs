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
    public class AssemblyRepository : AssemblyRepositoryBase, IAssemblyRepository
    {
        public AssemblyRepository(Camm2Context context)
            :base(context)
        {
        }

        public Assembly GetInculding(string itemsToInclude, int id)
        {
            return _entity.Include(itemsToInclude).Where(c => c.Id == id).FirstOrDefault();
        }

    }
}
