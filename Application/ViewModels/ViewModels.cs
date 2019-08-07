 
using System;
using Domain;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Application.Base;
using System.Collections.Generic;

namespace Application.Service
{
    public class BaseDetailVM 
    {
		public bool IsObsolete { get; set; }

		public DateTime CreateDate { get; set; }

		public User CreatedBy { get; set; }

		public DateTime UpdateDate { get; set; }

		public User UpdatedBy { get; set; }

	}

	public class BaseListVM 
    {
		public bool IsObsolete { get; set; }

		public DateTime CreateDate { get; set; }

		public User CreatedBy { get; set; }

		public DateTime UpdateDate { get; set; }

		public User UpdatedBy { get; set; }

	}

    public class UserDetailVM 
    {
		public int Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string EmailAddress { get; set; }

		public bool IsObsolete { get; set; }

		public DateTime CreateDate { get; set; }

		public int CreatedBy { get; set; }

		public DateTime UpdateDate { get; set; }

		public int UpdatedBy { get; set; }

	}

	public class UserListVM 
    {
		public int Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string EmailAddress { get; set; }

		public bool IsObsolete { get; set; }

		public DateTime CreateDate { get; set; }

		public int CreatedBy { get; set; }

		public DateTime UpdateDate { get; set; }

		public int UpdatedBy { get; set; }

	}

    public class DocumentDetailVM  : BaseDetailVM
    {
		public int Id { get; set; }

		public string Code { get; set; }

		public string Rev { get; set; }

		public string Title { get; set; }

		public DocumentType DocType { get; set; }

		public IList<Item> Items { get; set; }

	}

	public class DocumentListVM  : BaseDetailVM
    {
		public int Id { get; set; }

		public string Code { get; set; }

		public string Rev { get; set; }

		public string Title { get; set; }

		public DocumentType DocType { get; set; }

		public IList<Item> Items { get; set; }

	}

    public class ComponentDetailVM  : Item
    {
		public string Manufacturer { get; set; }

	}

	public class ComponentListVM  : Item
    {
		public string Manufacturer { get; set; }

	}

    public class ConnectorDetailVM  : Item
    {
		public string Family { get; set; }

		public string Comments { get; set; }

		public IList<Tool> Tools { get; set; }

	}

	public class ConnectorListVM  : Item
    {
		public string Family { get; set; }

		public string Comments { get; set; }

		public IList<Tool> Tools { get; set; }

	}

    public class ContactDetailVM  : Item
    {
		public string Size { get; set; }

		public string Family { get; set; }

		public int WireGageMin { get; set; }

		public int WireGageMax { get; set; }

		public string Comments { get; set; }

		public IList<Tool> Tools { get; set; }

	}

	public class ContactListVM  : Item
    {
		public string Size { get; set; }

		public string Family { get; set; }

		public int WireGageMin { get; set; }

		public int WireGageMax { get; set; }

		public string Comments { get; set; }

		public IList<Tool> Tools { get; set; }

	}

    public class ItemDetailVM  : BaseDetailVM
    {
		public int Id { get; set; }

		[StringLength(100)]
		public string Code { get; set; }

		public string Description { get; set; }

		public UnitsOfMeasure UnitsOfMeasure { get; set; }

		public int QtyOnHand { get; set; }

		public IList<Document> Documents { get; set; }

	}

	public class ItemListVM  : BaseDetailVM
    {
		public int Id { get; set; }

		[StringLength(100)]
		public string Code { get; set; }

		public string Description { get; set; }

		public UnitsOfMeasure UnitsOfMeasure { get; set; }

		public int QtyOnHand { get; set; }

		public IList<Document> Documents { get; set; }

	}

    public class ToolDetailVM  : Item
    {
		public string Manufacturer { get; set; }

		public string BinNumber { get; set; }

		public string MilitarySpecification { get; set; }

		public string SerialNumber { get; set; }

		public string Comments { get; set; }

		public IList<Connector> Connectors { get; set; }

		public IList<Contact> Contacts { get; set; }

	}

	public class ToolListVM  : Item
    {
		public string Manufacturer { get; set; }

		public string BinNumber { get; set; }

		public string MilitarySpecification { get; set; }

		public string SerialNumber { get; set; }

		public string Comments { get; set; }

		public IList<Connector> Connectors { get; set; }

		public IList<Contact> Contacts { get; set; }

	}

    public class AssemblyDetailVM  : Item
    {
		public string Rev { get; set; }

		public IList<AssemblyComponent> AssemblyComponents { get; set; }

	}

	public class AssemblyListVM  : Item
    {
		public string Rev { get; set; }

		public IList<AssemblyComponent> AssemblyComponents { get; set; }

	}

    public class AssemblyComponentDetailVM  : BaseDetailVM
    {
		public int Id { get; set; }

		public Assembly Assembly { get; set; }

		public Component Component { get; set; }

		public Decimal Qty { get; set; }

	}

	public class AssemblyComponentListVM  : BaseDetailVM
    {
		public int Id { get; set; }

		public Assembly Assembly { get; set; }

		public Component Component { get; set; }

		public Decimal Qty { get; set; }

	}

}