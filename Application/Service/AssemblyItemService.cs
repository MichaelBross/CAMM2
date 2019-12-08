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
                    AssemblyItemId = ai.Id,
                    LineNumber = ai.LineNumber,
                    AssemblyId = assemblyId,
                    ItemId = ai.Item.Id,
                    Qty = ai.Qty,
                    Code = ai.Item.Code,
                    Description = ai.Item.Description,
                    Reference = ai.Reference
                };
                assemblyItemVMList.Add(aivm);
            }
                        
            return assemblyItemVMList;
        }

        public AssemblyItemVM LinkToAssembly(AssemblyItemVM assemblyItemVM)
        {
            var assembly = _unitOfWork.Assemblys.Get(assemblyItemVM.AssemblyId);
            var item = _unitOfWork.Items.Get(assemblyItemVM.ItemId);
            var now = DateTime.Now;
            var assemblyItem = new AssemblyItem
            {
                Assembly = assembly,
                Item = item,
                Qty = assemblyItemVM.Qty,
                CreateDate = now,
                UpdateDate =now
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

        public string UnlinkItem(int assemblyItemId)
        {
            try
            {
                var assemblyItem = _unitOfWork.AssemblyItems.Get(assemblyItemId);
                _unitOfWork.AssemblyItems.Remove(assemblyItem);
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
