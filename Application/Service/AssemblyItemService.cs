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
    public class AssemblyItemService : AssemblyItemServiceBase, IAssemblyItemService
    {
	    private readonly IUnitOfWork _unitOfWork;

        public AssemblyItemService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<AssemblyItemVM> GetAssemblyItems(int assemblyId)
        {
            List<AssemblyItem> assemblyItemList = _unitOfWork.AssemblyItems.GetAssemblyItemsWithItems(assemblyId);
            var assemblyItemVMList = new List<AssemblyItemVM>();

            foreach(AssemblyItem ai in assemblyItemList)
            {
                var aivm = new AssemblyItemVM
                {
                    AssemblyId = assemblyId,
                    ItemId = ai.Item.Id,
                    Code = ai.Item.Code,
                    Description = ai.Item.Description,
                    Qty = ai.Qty
                };
                assemblyItemVMList.Add(aivm);
            }
                        
            return assemblyItemVMList;
        }

        public AssemblyItemVM LinkToAssembly(AssemblyItemVM assemblyItemVM)
        {
            var assembly = _unitOfWork.Assemblys.Get(assemblyItemVM.AssemblyId);
            var item = _unitOfWork.Items.Get(assemblyItemVM.ItemId);
            var assemblyItem = new AssemblyItem
            {
                Assembly = assembly,
                Item = item,
                Qty = assemblyItemVM.Qty
            };

            _unitOfWork.AssemblyItems.Add(assemblyItem);
            _unitOfWork.Complete();

            assemblyItemVM.AssemblyItemId = assemblyItem.Id;

            return assemblyItemVM;
        }

        public List<SelectListItem> LinkToSelectList()
        {
            var list = new List<SelectListItem>();

            list.Add(new SelectListItem { Text = "Assembly", Value = "Assembly" });

            return list;
        }

        public string UnlinkItem(int assemblyId)
        {
            try
            {
                var assemblyItem = _unitOfWork.AssemblyItems.Get(assemblyId);
                assemblyItem.IsObsolete = true;
                _unitOfWork.Complete();
                return "success";
            }
            catch (Exception)
            {
                return "failed";
            };
        }
    }
}
