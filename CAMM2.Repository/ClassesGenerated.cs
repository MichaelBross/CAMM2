
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
	public class Base
	{	 
		public bool IsObsolete { get; set; }

		public DateTime CreateDate { get; set; }

		public User CreatedBy { get; set; }

		public DateTime UpdateDate { get; set; }

		public User UpdatedBy { get; set; }

	}
	public class User
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
	public class Document : Base
	{	 
		public int Id { get; set; }

		public string Code { get; set; }

		public string Rev { get; set; }

		public string Title { get; set; }

		public string FileName { get; set; }

		public string Path { get; set; }

		public DocumentType DocType { get; set; }

		public IList<Item> Items { get; set; }

	}
	public class Component : Item
	{	 
		public string Manufacturer { get; set; }

	}
	public class Connector : Item
	{	 
		public string Family { get; set; }

		public string Comments { get; set; }

		public IList<Tool> Tools { get; set; }

	}
	public class Contact : Item
	{	 
		public string Size { get; set; }

		public string Family { get; set; }

		public int WireGageMin { get; set; }

		public int WireGageMax { get; set; }

		public string Comments { get; set; }

		public IList<Tool> Tools { get; set; }

	}
	public class Item : Base
	{	 
		public int Id { get; set; }

		[Index(IsUnique = true)]
		[StringLength(100)]
		public string Code { get; set; }

		public string Description { get; set; }

		public UnitsOfMeasure UnitsOfMeasure { get; set; }

		public int QtyOnHand { get; set; }

		public IList<Document> Documents { get; set; }

	}
	public class Tool : Item
	{	 
		public string Manufacturer { get; set; }

		public string BinNumber { get; set; }

		public string MilitarySpecification { get; set; }

		public string SerialNumber { get; set; }

		public string Comments { get; set; }

		public IList<Connector> Connectors { get; set; }

		public IList<Contact> Contacts { get; set; }

	}
	public class Assembly : Item
	{	 
		public string Rev { get; set; }

		public IList<AssemblyItem> AssemblyItems { get; set; }

	}
	public class AssemblyItem : Base
	{	 
		public int Id { get; set; }

		public Assembly Assembly { get; set; }

		public Item Item { get; set; }

		public Decimal Qty { get; set; }

	}
}

