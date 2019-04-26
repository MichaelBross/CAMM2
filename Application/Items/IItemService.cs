using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Items
{
    public interface IItemService
    {
        IEnumerable<ItemVM> Search(SearchParameters searchParams);
        IEnumerable<ItemVM> GetAll();
        int GetTotalCount();
        ItemVM Add(ItemVM itemVM);
        ItemVM Update(ItemVM itemVM);
        void Remove(ItemVM itemVM);
    }
}
