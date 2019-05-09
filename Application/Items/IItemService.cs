using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Items
{
    public interface IItemService
    {
        IEnumerable<ItemDetailVM> Search(SearchParameters searchParams);
        IEnumerable<ItemDetailVM> GetAll();
        int GetTotalCount();
        ItemDetailVM Add(ItemDetailVM itemVM);
        ItemDetailVM Update(ItemDetailVM itemVM);
        void Remove(ItemDetailVM itemVM);
    }
}
