using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Items;
using System.Web;

namespace Application.Interfaces
{
    public interface IItemsRepository : IRepository<Item> 
    {
        IEnumerable<Item> Search(SearchParameters searchParams);
    }
}
