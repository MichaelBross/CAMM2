 
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
    public class ItemController : ItemBaseController
    {              
        private readonly IItemService _itemService;
        private readonly IAssemblyService _assemblyService;

        public ItemController(IItemService itemService, IAssemblyService assemblyService) : base(itemService)
        { 
            _itemService = itemService;
            _assemblyService = assemblyService;
        }

        public ActionResult AssemblyItems(int id)
        {
            var parentVM = new ParentItemVM
            {
                Type = "Assembly",
                Id = id,
                Code = "Assembly Number",
                ItemSelectList = _itemService.LinkToSelectList()
            };

            return View(parentVM);
        }

        public JsonResult GetAssemblyItems(int assemblyId)
        {
            var assemblyItems = _itemService.GetAssemblyItems(assemblyId);

            return Json(new { data = assemblyItems, draw = Request["draw"] }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LinkItems(int assemblyId, IEnumerable<int> itemIdList)
        {
            foreach (int itemId in itemIdList)
            {
                _itemService.LinkToAssembly(assemblyId, itemId);
            }

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveItemLinks(int assemblyId, IEnumerable<int> itemIdList)
        {
            var result = "success";
            foreach (int itemId in itemIdList)
            {
                if (_itemService.UnlinkItem(assemblyId, itemId) != "success")
                {
                    result = "Failed";
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
