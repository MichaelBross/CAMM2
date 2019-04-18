using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Items;


namespace Application.Items
{
    public class ItemService : IItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public ItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ItemVM Add(ItemVM itemVM)
        {
            try
            {
                var newItem = new Item
                {
                    Code = itemVM.Code,
                    Description = itemVM.Description,
                    QtyOnHand = itemVM.QtyOnHand,
                    UnitsOfMeasure = itemVM.UnitsOfMeasure,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                };

                _unitOfWork.Items.Add(newItem);
                _unitOfWork.Complete();

                itemVM.Id = newItem.Id;

                return itemVM;
            }
            catch (Exception ex)
            {
                switch (ex.Message)
                {
                    case string a when a.Contains("Duplicate Code"):
                        throw new Exception("Duplicate Code");
                        
                    case string a when a.Contains("See the inner exception for details."):
                        if (ex.InnerException.Message.Contains("See the inner exception for details."))
                            throw new Exception(ex.InnerException.InnerException.Message);
                        else
                            throw new Exception(ex.InnerException.Message);
                        
                    default:
                        throw new Exception(ex.Message);                        
                }
            }
        }

        public IEnumerable<ItemVM> GetAll()
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount()
        {
            throw new NotImplementedException();
        }

        public bool Remove(ItemVM itemVM)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemVM> Search(SearchParameters searchParams)
        {
            var result = _unitOfWork.Items.Search(searchParams)
                .Select(i => new ItemVM()
                {
                    Id = i.Id,
                    Code = i.Code,
                    Description = i.Description,
                    QtyOnHand = i.QtyOnHand,
                    UnitsOfMeasure = i.UnitsOfMeasure
                    
                }).ToList();

            return result;
        }

        public ItemVM Update(ItemVM itemVM)
        {
            throw new NotImplementedException();
        }
    }
}
