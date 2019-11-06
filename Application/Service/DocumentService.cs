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
    public class DocumentService : DocumentServiceBase, IDocumentService
    {
	    private readonly IUnitOfWork _unitOfWork;

        public DocumentService(IUnitOfWork unitOfWork) : base(unitOfWork)
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
            list.Add(new SelectListItem { Text = "Tool", Value = "Tool" });            

            return list;
        }

	}
}
