using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Persistance.Repository;
using Domain.Items;
using Application;
using Application.Interfaces;

namespace Persistance
{
    public class ConnectorsRepository : Repository<Connector>, IConnectorsRepository
    {
        public ConnectorsRepository(Camm2Context context)
            :base(context)
        {
        }
               
        public IEnumerable<Connector> Search(SearchParameters searchParams)
        {
            var query = Camm2Context.Connectors.AsQueryable();

            if (!String.IsNullOrEmpty(searchParams.SearchValue))
            {
                string[] terms = searchParams.SearchValue.Split(' ');

                foreach (string term in terms)
                {
                    query = query.Where(q => q.Code.Contains(term)
                    || q.Description.Contains(term));
                }
            }

            query = query.OrderBy(searchParams.SortColumnName + " " + searchParams.SortDirection);
            query = query.Skip(searchParams.Start).Take(searchParams.Length);

            return query.ToList();
        }

        public Camm2Context Camm2Context
        {
            get { return Context as Camm2Context; }
        }
    }
}
