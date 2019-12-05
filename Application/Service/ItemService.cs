using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Domain;
using Application;
using Application.Interfaces;
using System.Web.Mvc;

namespace Application.Service
{
    public class ItemService : ItemServiceBase, IItemService
    {
	    private readonly IUnitOfWork _unitOfWork;

        public ItemService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
               
        new public IEnumerable<ItemListVM> Search(SearchParameters searchParams)
        {
            var result = _unitOfWork.Items.Search(searchParams)
                .Select(i => new ItemListVM()
                {
                    IsObsolete = i.IsObsolete,
                    CreateDate = i.CreateDate,
                    CreatedBy = i.CreatedBy,
                    UpdateDate = i.UpdateDate,
                    UpdatedBy = i.UpdatedBy,
                    Id = i.Id,
                    Code = i.Code,
                    Description = i.Description,
                    UnitsOfMeasure = i.UnitsOfMeasure,
                    QtyOnHand = i.QtyOnHand
                }).OrderBy(i => i.Code).ToList();

            return result;
        }
    }
}
