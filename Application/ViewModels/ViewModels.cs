 
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
		[HiddenInput(DisplayValue = false)]
		public bool IsObsolete { get; set; }

		[HiddenInput(DisplayValue = false)]
		public DateTime CreateDate { get; set; }

		[HiddenInput(DisplayValue = false)]
		public User CreatedBy { get; set; }

		[HiddenInput(DisplayValue = false)]
		public DateTime UpdateDate { get; set; }

		[HiddenInput(DisplayValue = false)]
		public User UpdatedBy { get; set; }

	}

    public class ItemDetailVM : BaseDetailVM
    {
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		[Display(Name = "Item Number")]
		public string Code { get; set; }

		public string Description { get; set; }

		[Display(Name = "Units of Measure")]
		public UnitsOfMeasure UnitsOfMeasure { get; set; }

		[Range(0, 2147483646, ErrorMessage = "Value must be between 0 and 2147483646.")]
		[Display(Name = "Qty on Hand")]
		public int QtyOnHand { get; set; }

		public IList<Document> Documents { get; set; }

	}

    public class ItemListVM : BaseListVM
    {
		[HiddenInput(DisplayValue = false)]
		public int Id { get; set; }

		[Display(Name = "Item Number")]
		public string Code { get; set; }

		public string Description { get; set; }

		[Display(Name = "Units of Measure")]
		public UnitsOfMeasure UnitsOfMeasure { get; set; }

		[Range(0, 2147483646, ErrorMessage = "Value must be between 0 and 2147483646.")]
		[Display(Name = "Qty on Hand")]
		public int QtyOnHand { get; set; }

		[HiddenInput(DisplayValue = false)]
		public IList<Document> Documents { get; set; }

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

    public class DocumentDetailVM : BaseDetailVM
    {
		public int Id { get; set; }

		public string Code { get; set; }

		public string Rev { get; set; }

		public string Title { get; set; }

		public DocumentType DocType { get; set; }

		public IList<Item> Items { get; set; }

	}

    public class DocumentListVM : BaseListVM
    {
		public int Id { get; set; }

		public string Code { get; set; }

		public string Rev { get; set; }

		public string Title { get; set; }

		public DocumentType DocType { get; set; }

		public IList<Item> Items { get; set; }

	}

    public class ComponentDetailVM : ItemDetailVM
    {
		public string Manufacturer { get; set; }

	}

    public class ComponentListVM : ItemListVM
    {
		public string Manufacturer { get; set; }

	}

    public class ConnectorDetailVM : ItemDetailVM
    {
		public string Family { get; set; }

		public string Comments { get; set; }

		public IList<Tool> Tools { get; set; }

	}

    public class ConnectorListVM : ItemListVM
    {
		public string Family { get; set; }

		public string Comments { get; set; }

		public IList<Tool> Tools { get; set; }

	}

    public class ContactDetailVM : ItemDetailVM
    {
		public string Size { get; set; }

		public string Family { get; set; }

		public int WireGageMin { get; set; }

		public int WireGageMax { get; set; }

		public string Comments { get; set; }

		public IList<Tool> Tools { get; set; }

	}

    public class ContactListVM : ItemListVM
    {
		public string Size { get; set; }

		public string Family { get; set; }

		public int WireGageMin { get; set; }

		public int WireGageMax { get; set; }

		public string Comments { get; set; }

		public IList<Tool> Tools { get; set; }

	}

    public class ToolDetailVM : ItemDetailVM
    {
		public string Manufacturer { get; set; }

		public string BinNumber { get; set; }

		public string MilitarySpecification { get; set; }

		public string SerialNumber { get; set; }

		public string Comments { get; set; }

		public IList<Connector> Connectors { get; set; }

		public IList<Contact> Contacts { get; set; }

	}

    public class ToolListVM : ItemListVM
    {
		public string Manufacturer { get; set; }

		public string BinNumber { get; set; }

		public string MilitarySpecification { get; set; }

		public string SerialNumber { get; set; }

		public string Comments { get; set; }

		public IList<Connector> Connectors { get; set; }

		public IList<Contact> Contacts { get; set; }

	}

    public class AssemblyDetailVM : ItemDetailVM
    {
		public string Rev { get; set; }

		public IList<AssemblyComponent> AssemblyComponents { get; set; }

	}

    public class AssemblyListVM : ItemListVM
    {
		public string Rev { get; set; }

		public IList<AssemblyComponent> AssemblyComponents { get; set; }

	}

    public class AssemblyComponentDetailVM : BaseDetailVM
    {
		public int Id { get; set; }

		public Assembly Assembly { get; set; }

		public Component Component { get; set; }

		public Decimal Qty { get; set; }

	}

    public class AssemblyComponentListVM : BaseListVM
    {
		public int Id { get; set; }

		public Assembly Assembly { get; set; }

		public Component Component { get; set; }

		public Decimal Qty { get; set; }

	}

}