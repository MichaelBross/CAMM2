 
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
            var parentVM = new ParentItemVM
            {
                Type = "Assembly",
                Id = id,
                Code = "Assembly Number",
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
            foreach (int itemId in itemIdList)
            {
                var assemblyItem = new AssemblyItemVM
                {
                    AssemblyId = assemblyId,
                    ItemId = itemId
                };
                _assemblyitemService.LinkToAssembly(assemblyItem);
            }

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveItemLinks(IEnumerable<int> assemblyItemIdList)
        {
            var result = "success";
            foreach (int itemId in assemblyItemIdList)
            {
                if (_assemblyitemService.UnlinkItem(itemId) != "success")
                {
                    result = "Failed";
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
