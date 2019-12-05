 
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

        public ItemController(IItemService itemService) : base(itemService)
        { 
            _itemService = itemService;            
        }
 
    }
}
