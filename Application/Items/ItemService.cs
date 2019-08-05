using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using Application.Service;


namespace Application.Items
{
    public class ItemService : IItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ItemDetailVM Add(ItemDetailVM itemVM)
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

        public IEnumerable<ItemListVM> GetAll()
        {
            var items = _unitOfWork.Items.GetAll();
            var itemVMs = new List<ItemListVM>();

            Map.AtoB(items, itemVMs);

            return itemVMs;
        }

        public int GetTotalCount()
        {
            var count = _unitOfWork.Items.GetAll().Count();
            return count;
        }

        public void Remove(ItemDetailVM itemVM)
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

        public IEnumerable<ItemListVM> Search(SearchParameters searchParams)
        {
            var result = _unitOfWork.Items.Search(searchParams)
                .Select(i => new ItemListVM()
                {
                    Id = i.Id,
                    Code = i.Code,
                    Description = i.Description,
                    QtyOnHand = i.QtyOnHand,
                    UnitsOfMeasure = i.UnitsOfMeasure,
                    CreateDate = i.CreateDate,
                    UpdateDate = i.UpdateDate

                }).ToList();

            return result;
        }

        public ItemDetailVM Update(ItemDetailVM revisedVM)
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
