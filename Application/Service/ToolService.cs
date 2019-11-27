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
    public class ToolService : ToolServiceBase, IToolService
    {
	    private readonly IUnitOfWork _unitOfWork;

        public ToolService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<SelectListItem> LinkToSelectList()
        {
            var list = new List<SelectListItem>();

            list.Add(new SelectListItem { Text = "-", Value = "" });
            list.Add(new SelectListItem { Text = "Assembly", Value = "Assembly" });
            list.Add(new SelectListItem { Text = "Component", Value = "Component" });
            list.Add(new SelectListItem { Text = "Connector", Value = "Connector" });
            list.Add(new SelectListItem { Text = "Contact", Value = "Contact" });
            list.Add(new SelectListItem { Text = "Item", Value = "Item" });            

            return list;
        }

        public List<ToolListVM> GetToolsLinkedToItem(string type, int typeId)
        {
            var item = new Item();

            switch (type)
            {
                case "Item":
                    item = _unitOfWork.Items.GetInculding("Tools", typeId);
                    break;
                case "Assembly":
                    item = _unitOfWork.Assemblys.GetInculding("Tools", typeId);
                    break;
                case "Component":
                    item = _unitOfWork.Components.GetInculding("Tools", typeId);
                    break;
                case "Connector":
                    item = _unitOfWork.Connectors.GetInculding("Tools", typeId);
                    break;
                case "Contact":
                    item = _unitOfWork.Contacts.GetInculding("Tools", typeId);
                    break;
                default:
                    throw new Exception("Type " + type + " not found.");
            }

            var toolVMList = new List<ToolListVM>();
            Map.ListToList(item.Tools.ToList(), toolVMList);

            return toolVMList;
        }

        public string LinkToItem(string type, int typeId, int toolId)
        {
            var item = new Item();

            switch (type)
            {
                case "Item":
                    item = _unitOfWork.Items.GetInculding("Tools", typeId);
                    break;
                case "Assembly":
                    item = _unitOfWork.Assemblys.GetInculding("Tools", typeId);
                    break;
                case "Component":
                    item = _unitOfWork.Components.GetInculding("Tools", typeId);
                    break;
                case "Connector":
                    item = _unitOfWork.Connectors.GetInculding("Tools", typeId);
                    break;
                case "Contact":
                    item = _unitOfWork.Contacts.GetInculding("Tools", typeId);
                    break;
                default:
                    throw new Exception("Type " + type + " not found.");
            }

            if (item.Tools == null)
            {
                item.Tools = new List<Tool>();
            }
            else
            {
                //if (item.Tools.Where(d => d.Id == toolId).Count() > 0)
                //    return "duplicate";
            }

            var tool = _unitOfWork.Tools.Get(toolId);
            item.Tools.Add(tool);
            _unitOfWork.Complete();
            return "success";
        }

        public string UnlinkTool(string type, int typeId, int toolId)
        {
            var item = new Item();

            switch (type)
            {
                case "Item":
                    item = _unitOfWork.Items.GetInculding("Tools", typeId);
                    break;
                case "Assembly":
                    item = _unitOfWork.Assemblys.GetInculding("Tools", typeId);
                    break;
                case "Component":
                    item = _unitOfWork.Components.GetInculding("Tools", typeId);
                    break;
                case "Connector":
                    item = _unitOfWork.Connectors.GetInculding("Tools", typeId);
                    break;
                case "Contact":
                    item = _unitOfWork.Contacts.GetInculding("Tools", typeId);
                    break;
                case "Tool":
                    item = _unitOfWork.Tools.GetInculding("Tools", typeId);
                    break;
                default:
                    throw new Exception("Type " + type + " not found.");
            }

            var tool = _unitOfWork.Tools.Get(toolId);
            item.Tools.Remove(tool);
            _unitOfWork.Complete();
            return "success";
        }
    }
}
