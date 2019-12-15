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

        public string LinkToAssembly(AssemblyItemVM assemblyItemVM)
        {            
            var item = _unitOfWork.Items.Get(assemblyItemVM.ItemId);
            var message = "Failed to add item " + item.Code;

            if (_unitOfWork.AssemblyItems.IsDuplicate(assemblyItemVM.AssemblyId, assemblyItemVM.ItemId))
            {
                message = "Item " + item.Code + " would have been a duplicate but was not added to the list.";
                return message;
            }
                
            var assembly = _unitOfWork.Assemblys.Get(assemblyItemVM.AssemblyId);
            
            var now = DateTime.Now;
            
            if (assemblyItemVM.LineNumber == 0)
                assemblyItemVM.LineNumber = _unitOfWork.AssemblyItems.GetNextLineNumber(assemblyItemVM.AssemblyId);

            var assemblyItem = new AssemblyItem
            {
                LineNumber = assemblyItemVM.LineNumber,
                Assembly = assembly,
                Item = item,
                Qty = assemblyItemVM.Qty,
                Reference = assemblyItemVM.Reference,
                CreateDate = now,
                UpdateDate =now
            };

            _unitOfWork.AssemblyItems.Add(assemblyItem);
            _unitOfWork.Complete();

            assemblyItemVM.AssemblyItemId = assemblyItem.Id;

            message = "success";
            return message;
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

        public AssemblyItemVM Update(AssemblyItemVM assemblyItemVM)
        {
            var retrieved = _unitOfWork.AssemblyItems.GetAssemblyItemWithAssemblyAndItem(assemblyItemVM.AssemblyItemId);
                       
            var changes = "";

            if (retrieved.Assembly.Id != assemblyItemVM.AssemblyId)
            {
                changes += "AssemblyId ";
                retrieved.Assembly = _unitOfWork.Assemblys.Get(assemblyItemVM.AssemblyId);
            }

            if (retrieved.Item.Id != assemblyItemVM.ItemId)
            {
                changes += "ItemId ";
                retrieved.Item = _unitOfWork.Items.Get(assemblyItemVM.ItemId);                
            }                

            if (retrieved.LineNumber != assemblyItemVM.LineNumber)
            {
                changes += "LineNumber ";
                retrieved.LineNumber = assemblyItemVM.LineNumber;
            }                

            if (retrieved.Qty != assemblyItemVM.Qty)
            {
                changes += "Qty ";
                retrieved.Qty = assemblyItemVM.Qty;
            }   

            if (retrieved.Reference != assemblyItemVM.Reference)
            {
                changes += "Reference ";
                retrieved.Reference = assemblyItemVM.Reference;
            }   

            if (changes == "")
                return assemblyItemVM;

            var now = DateTime.Now;
            retrieved.UpdateDate = now;
            
            _unitOfWork.Complete();
            
            return assemblyItemVM;
        }

        //private bool PropertyValuesAreEqual<T, TU>(this T A, TU B)
        //{
        //    var sourceProps = typeof(T).GetProperties().Where(x => x.CanRead).ToList();
        //    var destProps = typeof(TU).GetProperties().Where(x => x.CanRead).ToList();

        //    foreach (var sourceProp in sourceProps)
        //    {
        //        if (destProps.Any(x => x.Name == sourceProp.Name))
        //        {
        //            var destProp = destProps.First(x => x.Name == sourceProp.Name);
        //            if (destProp.GetValue(destProp).ToString() != sourceProp.GetValue(sourceProp).ToString()) 
        //            {
        //                return false;
        //            }
        //        }
        //    }

        //    return true;
        //}
    }
}
