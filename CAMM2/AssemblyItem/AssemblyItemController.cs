 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application;
using Application.Interfaces;
using Application.Service;
using Application.ViewModels;

namespace Presentation
{ 
    public class AssemblyItemController : AssemblyItemBaseController
    {              
        private readonly IAssemblyItemService _assemblyitemService;
        private readonly IAssemblyService _assemblyService;
        private readonly IItemService _itemService;

        public AssemblyItemController(IAssemblyItemService assemblyitemService, IAssemblyService assemblyService, IItemService itemService) : base(assemblyitemService)
        { 
            _assemblyitemService = assemblyitemService;
            _assemblyService = assemblyService;
            _itemService = itemService;
        }

        public ActionResult AssemblyItems(int id)
        {
            var code = _assemblyService.Get(id).Code;
            var parentVM = new ParentItemVM
            {
                Type = "Assembly",
                Id = id,
                Code = code,
                ItemSelectList = _assemblyitemService.LinkToSelectList()
            };

            return View(parentVM);
        }

        public JsonResult GetAssemblyItems(int assemblyId)
        {
            var assemblyItems = _assemblyitemService.GetAssemblyItems(assemblyId);

            return Json(new { data = assemblyItems, draw = Request["draw"] }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LinkItems(int assemblyId, IEnumerable<int> itemIdList)
        {
            var result = "success";

            foreach (int itemId in itemIdList)
            {
                var assemblyItem = new AssemblyItemVM
                {
                    AssemblyId = assemblyId,
                    ItemId = itemId
                };
                var message = _assemblyitemService.LinkToAssembly(assemblyItem);
                if(message != "success")
                {
                    if(result == "success")
                    {
                        result = message;
                    }
                    else
                    {
                        result += message + "\r\n";
                    }
                }

            }

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveItemLinks(IEnumerable<int> assemblyItemIdList)
        {
            if (assemblyItemIdList == null)
                throw new ArgumentNullException("assemblyItemIdList");

            var result = "success";
            foreach (int assemblyItemId in assemblyItemIdList)
            {
                if (_assemblyitemService.UnlinkItem(assemblyItemId) != "success")
                {
                    result = "Failed";
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateList(List<AssemblyItemVM> assemblyItemList)
        {
            if (assemblyItemList == null)
                throw new Exception("Nothing to save.");

            var result = "success";
            foreach(AssemblyItemVM ai in assemblyItemList)
            {
                _assemblyitemService.Update(ai);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
