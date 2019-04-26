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
                var newItem = new Item();

                Map.AtoB(itemVM, newItem);

                var now = DateTime.Now;
                newItem.CreateDate = now;
                newItem.UpdateDate = now;

                _unitOfWork.Items.Add(newItem);
                _unitOfWork.Complete();
                                
                Map.AtoB(newItem, itemVM);

                return itemVM;
            }
            catch (Exception ex)
            {  
                var message = Utility.GetRootCauseOfException(ex);

                throw new Exception(message);
            }
        }

        public IEnumerable<ItemVM> GetAll()
        {
            var items = _unitOfWork.Items.GetAll();
            var itemVMs = new List<ItemVM>();

            Map.AtoB(items, itemVMs);

            return itemVMs;
        }

        public int GetTotalCount()
        {
            var count = _unitOfWork.Items.GetAll().Count();
            return count;
        }

        public void Remove(ItemVM itemVM)
        {
            try
            {               
                var toBeRemoved = _unitOfWork.Items.Get(itemVM.Id);
                if (toBeRemoved == null)
                    throw new Exception("Item not found.");
                _unitOfWork.Items.Remove(toBeRemoved);
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                var message = Utility.GetRootCauseOfException(ex);
                throw new Exception(message);
            }
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

        public ItemVM Update(ItemVM revisedVM)
        {
            try
            {
                var retrieved = _unitOfWork.Items.Get(revisedVM.Id);
                retrieved.Code = revisedVM.Code;
                retrieved.Description = revisedVM.Description;
                retrieved.QtyOnHand = revisedVM.QtyOnHand;
                retrieved.UnitsOfMeasure = revisedVM.UnitsOfMeasure;
                var now = DateTime.Now;
                retrieved.UpdateDate = now;                
                _unitOfWork.Complete();
                return revisedVM;
            }
            catch (Exception ex)
            {
                var message = Utility.GetRootCauseOfException(ex);
                throw new Exception(message);
            }
        }
    }
}
