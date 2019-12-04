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

        public List<BOMListVM> GetAssemblyItems(int assemblyId)
        {
            var assembly = _unitOfWork.Assemblys.GetInculding("AssemblyItems", assemblyId);            

            var list = new List<BOMListVM>();

            foreach(var ai in assembly.AssemblyItems)
            {
                var item = _unitOfWork.Items.Get(ai.Id);

                var lineItem = new BOMListVM
                {
                    Id = ai.Id,
                    Qty = ai.Qty,
                    AssemblyId = ai.Assembly.Id,
                    ItemId = item.Id,
                    Code = item.Code,
                    Description = item.Description
                };

                list.Add(lineItem);
            }

            return list;
        }

        public List<SelectListItem> LinkToSelectList()
        {
            var list = new List<SelectListItem>();

            list.Add(new SelectListItem { Text = "Assembly", Value = "Assembly" });

            return list;
        }

        public string LinkToAssembly(int assemblyId, int itemId)
        {
            throw new NotImplementedException();
        }

        public string UnlinkItem(int assemblyId, int itemId)
        {
            throw new NotImplementedException();
        }
    }
}
