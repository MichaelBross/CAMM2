 
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
		[Display(Name = "Is Obsolete", Order = 1000)]
		public bool IsObsolete { get; set; }

		[Display(Name = "Create Date", Order = 1010)]
		public DateTime CreateDate { get; set; }

		[Display(Name = "Created By", Order = 1020)]
		public User CreatedBy { get; set; }

		[Display(Name = "Update Date", Order = 1030)]
		public DateTime UpdateDate { get; set; }

		[Display(Name = "Updated By", Order = 1040)]
		public User UpdatedBy { get; set; }

	}

    public class BaseListVM
    {
		[HiddenInput(DisplayValue = false)]
		[Display(Name = "Is Obsolete", Order = 1000)]
		public bool IsObsolete { get; set; }

		[HiddenInput(DisplayValue = false)]
		[Display(Name = "Create Date", Order = 1010)]
		public DateTime CreateDate { get; set; }

		[HiddenInput(DisplayValue = false)]
		[Display(Name = "Created By", Order = 1020)]
		public User CreatedBy { get; set; }

		[HiddenInput(DisplayValue = false)]
		[Display(Name = "Update Date", Order = 1030)]
		public DateTime UpdateDate { get; set; }

		[HiddenInput(DisplayValue = false)]
		[Display(Name = "Updated By", Order = 1040)]
		public User UpdatedBy { get; set; }

	}

    public class ItemDetailVM : BaseDetailVM
    {
		[HiddenInput(DisplayValue = false)]
		[Display(Name = "Id", Order = -100)]
		public int Id { get; set; }

		[Display(Name = "Item Number", Order = -90)]
		public string Code { get; set; }

		[Display(Name = "Description", Order = -80)]
		public string Description { get; set; }

		[Display(Name = "Units of Measure", Order = -70)]
		public UnitsOfMeasure UnitsOfMeasure { get; set; }

		[Range(0, 2147483646, ErrorMessage = "Value must be between 0 and 2147483646.")]
		[Display(Name = "Qty on Hand", Order = -60)]
		public int QtyOnHand { get; set; }

		[Display(Name = "Documents", Order = -50)]
		public IList<Document> Documents { get; set; }

	}

    public class ItemListVM : BaseListVM
    {
		[HiddenInput(DisplayValue = false)]
		[Display(Name = "Item Id", Order = -90)]
		public int Id { get; set; }

		[Display(Name = "Item Number", Order = -80)]
		public string Code { get; set; }

		[Display(Name = "Description", Order = -70)]
		public string Description { get; set; }

		[Display(Name = "Units of Measure", Order = -60)]
		public UnitsOfMeasure UnitsOfMeasure { get; set; }

		[Range(0, 2147483646, ErrorMessage = "Value must be between 0 and 2147483646.")]
		[Display(Name = "Qty on Hand", Order = -50)]
		public int QtyOnHand { get; set; }

		[HiddenInput(DisplayValue = false)]
		[Display(Name = "Documents", Order = -40)]
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

		[Display(Name = "Document Number", Order = -90)]
		public string Code { get; set; }

		[Display(Name = "Rev", Order = -80)]
		public string Rev { get; set; }

		[Display(Name = "Title", Order = -70)]
		public string Title { get; set; }

		[Display(Name = "Category", Order = -60)]
		public DocumentType DocType { get; set; }

		public IList<Item> Items { get; set; }

	}

    public class DocumentListVM : BaseListVM
    {
		[HiddenInput(DisplayValue = false)]
		[Display(Name = "Document Id", Order = -90)]
		public int Id { get; set; }

		[Display(Name = "Document Number", Order = -80)]
		public string Code { get; set; }

		[Display(Name = "Rev", Order = -70)]
		public string Rev { get; set; }

		[Display(Name = "Title", Order = -60)]
		public string Title { get; set; }

		[Display(Name = "Type", Order = -50)]
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
		[Display(Name = "Family", Order = -69)]
		public string Family { get; set; }

		[Display(Name = "Comments", Order = -68)]
		public string Comments { get; set; }

		[Display(Name = "Tools", Order = -59)]
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

		public IList<AssemblyItem> AssemblyItems { get; set; }

	}

    public class AssemblyListVM : ItemListVM
    {
		public string Rev { get; set; }

		public IList<AssemblyItem> AssemblyItems { get; set; }

	}

    public class AssemblyItemDetailVM : BaseDetailVM
    {
		public int Id { get; set; }

		public Assembly Assembly { get; set; }

		public Item Item { get; set; }

		public Decimal Qty { get; set; }

	}

    public class AssemblyItemListVM : BaseListVM
    {
		public int Id { get; set; }

		public Assembly Assembly { get; set; }

		public Item Item { get; set; }

		public Decimal Qty { get; set; }

	}

}