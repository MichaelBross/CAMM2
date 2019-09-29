// Generated Code! Do not manually edit. Adjust template (BaseServices.tt) to make changes to this file.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain;
using Application.Service;

namespace Application.Service
{
   public class UserServiceBase : IUserServiceBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserServiceBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public UserDetailVM Add(UserDetailVM userVM)
        {
            try
            {
                var newUser = new User();

                Map.AtoB(userVM, newUser);

                var now = DateTime.Now;
                newUser.CreateDate = now;
                newUser.UpdateDate = now;

                _unitOfWork.Users.Add(newUser);
                _unitOfWork.Complete();

                Map.AtoB(newUser, userVM);

                return userVM;
            }
            catch (Exception ex)
            {
                var message = Utility.GetRootCauseOfException(ex);

                throw new Exception(message);
            }
        }

		public UserDetailVM Get(int id)
        {
            var user = _unitOfWork.Users.Get(id);
            var userVM = new UserDetailVM();

            Map.AtoB(user, userVM);


            return userVM;
        }

        public IEnumerable<UserListVM> GetAll()
        {
            var users = _unitOfWork.Users.GetAll().ToList();
            var userVMs = new List<UserListVM>();

            Map.ListToList(users, userVMs);

            return userVMs;
        }

        public int GetTotalCount()
        {
            var count = _unitOfWork.Users.GetAll().Count();
            return count;
        }

        public void Remove(UserDetailVM userVM)
        {
            try
            {
                var toBeRemoved = _unitOfWork.Users.Get(userVM.Id);
                if (toBeRemoved == null)
                    throw new Exception("User not found.");
                _unitOfWork.Users.Remove(toBeRemoved);
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                var message = Utility.GetRootCauseOfException(ex);
                throw new Exception(message);
            }
        }

        public IEnumerable<UserListVM> Search(SearchParameters searchParams)
        {
            var result = _unitOfWork.Users.Search(searchParams)
                .Select(i => new UserListVM()
                {
					Id = i.Id,
					FirstName = i.FirstName,
					LastName = i.LastName,
					EmailAddress = i.EmailAddress,
					IsObsolete = i.IsObsolete,
					CreateDate = i.CreateDate,
					CreatedBy = i.CreatedBy,
					UpdateDate = i.UpdateDate,
					UpdatedBy = i.UpdatedBy,
                }).ToList();

            return result;
        }

        public UserDetailVM Update(UserDetailVM revisedVM)
        {
            try
            {
                var retrieved = _unitOfWork.Users.Get(revisedVM.Id);

				retrieved.Id = revisedVM.Id;
				retrieved.FirstName = revisedVM.FirstName;
				retrieved.LastName = revisedVM.LastName;
				retrieved.EmailAddress = revisedVM.EmailAddress;
				retrieved.IsObsolete = revisedVM.IsObsolete;
				retrieved.CreateDate = revisedVM.CreateDate;
				retrieved.CreatedBy = revisedVM.CreatedBy;
				retrieved.UpdateDate = revisedVM.UpdateDate;
				retrieved.UpdatedBy = revisedVM.UpdatedBy;
                var now = DateTime.Now;
                retrieved.UpdateDate = now;
                _unitOfWork.Complete();
                return revisedVM;
            }
            catch (Exception ex)
            {
                var message = Utility.GetRootCauseOfException(ex);
                throw new Exception(message);
            }
        }
    }

   public class DocumentServiceBase : IDocumentServiceBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DocumentServiceBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public DocumentDetailVM Add(DocumentDetailVM documentVM)
        {
            try
            {
                var newDocument = new Document();

                Map.AtoB(documentVM, newDocument);

                var now = DateTime.Now;
                newDocument.CreateDate = now;
                newDocument.UpdateDate = now;

                _unitOfWork.Documents.Add(newDocument);
                _unitOfWork.Complete();

                Map.AtoB(newDocument, documentVM);

                return documentVM;
            }
            catch (Exception ex)
            {
                var message = Utility.GetRootCauseOfException(ex);

                throw new Exception(message);
            }
        }

		public DocumentDetailVM Get(int id)
        {
            var document = _unitOfWork.Documents.Get(id);
            var documentVM = new DocumentDetailVM();

            Map.AtoB(document, documentVM);

			var ItemList = new List<ItemListVM>();
			if(document.Items != null)
            {
				foreach(Item item in document.Items)
				{
					var vm = new ItemListVM();
					Map.AtoB(item, vm);
					ItemList.Add(vm);
				}
			}
			documentVM.Items = ItemList;

            return documentVM;
        }

        public IEnumerable<DocumentListVM> GetAll()
        {
            var documents = _unitOfWork.Documents.GetAll().ToList();
            var documentVMs = new List<DocumentListVM>();

            Map.ListToList(documents, documentVMs);

            return documentVMs;
        }

        public int GetTotalCount()
        {
            var count = _unitOfWork.Documents.GetAll().Count();
            return count;
        }

        public void Remove(DocumentDetailVM documentVM)
        {
            try
            {
                var toBeRemoved = _unitOfWork.Documents.Get(documentVM.Id);
                if (toBeRemoved == null)
                    throw new Exception("Document not found.");
                _unitOfWork.Documents.Remove(toBeRemoved);
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                var message = Utility.GetRootCauseOfException(ex);
                throw new Exception(message);
            }
        }

        public IEnumerable<DocumentListVM> Search(SearchParameters searchParams)
        {
            var result = _unitOfWork.Documents.Search(searchParams)
                .Select(i => new DocumentListVM()
                {
					IsObsolete = i.IsObsolete,
					CreateDate = i.CreateDate,
					CreatedBy = i.CreatedBy,
					UpdateDate = i.UpdateDate,
					UpdatedBy = i.UpdatedBy,
					Id = i.Id,
					Code = i.Code,
					Rev = i.Rev,
					Title = i.Title,
					DocType = i.DocType,
                }).ToList();

            return result;
        }

        public DocumentDetailVM Update(DocumentDetailVM revisedVM)
        {
            try
            {
                var retrieved = _unitOfWork.Documents.Get(revisedVM.Id);

				retrieved.IsObsolete = revisedVM.IsObsolete;
				retrieved.CreateDate = revisedVM.CreateDate;
				retrieved.CreatedBy = revisedVM.CreatedBy;
				retrieved.UpdateDate = revisedVM.UpdateDate;
				retrieved.UpdatedBy = revisedVM.UpdatedBy;
				retrieved.Id = revisedVM.Id;
				retrieved.Code = revisedVM.Code;
				retrieved.Rev = revisedVM.Rev;
				retrieved.Title = revisedVM.Title;
				retrieved.DocType = revisedVM.DocType;
				var Itemlist = new List<Item>();
				if (revisedVM.Items != null)
				{                    
					foreach(ItemListVM vm in revisedVM.Items)
					{
						var item = new Item();
						Map.AtoB(vm, item);
						Itemlist.Add(item);
					}
				}
				retrieved.Items = Itemlist;
                var now = DateTime.Now;
                retrieved.UpdateDate = now;
                _unitOfWork.Complete();
                return revisedVM;
            }
            catch (Exception ex)
            {
                var message = Utility.GetRootCauseOfException(ex);
                throw new Exception(message);
            }
        }
    }

   public class ComponentServiceBase : IComponentServiceBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ComponentServiceBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ComponentDetailVM Add(ComponentDetailVM componentVM)
        {
            try
            {
                var newComponent = new Component();

                Map.AtoB(componentVM, newComponent);

                var now = DateTime.Now;
                newComponent.CreateDate = now;
                newComponent.UpdateDate = now;

                _unitOfWork.Components.Add(newComponent);
                _unitOfWork.Complete();

                Map.AtoB(newComponent, componentVM);

                return componentVM;
            }
            catch (Exception ex)
            {
                var message = Utility.GetRootCauseOfException(ex);

                throw new Exception(message);
            }
        }

		public ComponentDetailVM Get(int id)
        {
            var component = _unitOfWork.Components.Get(id);
            var componentVM = new ComponentDetailVM();

            Map.AtoB(component, componentVM);


            return componentVM;
        }

        public IEnumerable<ComponentListVM> GetAll()
        {
            var components = _unitOfWork.Components.GetAll().ToList();
            var componentVMs = new List<ComponentListVM>();

            Map.ListToList(components, componentVMs);

            return componentVMs;
        }

        public int GetTotalCount()
        {
            var count = _unitOfWork.Components.GetAll().Count();
            return count;
        }

        public void Remove(ComponentDetailVM componentVM)
        {
            try
            {
                var toBeRemoved = _unitOfWork.Components.Get(componentVM.Id);
                if (toBeRemoved == null)
                    throw new Exception("Component not found.");
                _unitOfWork.Components.Remove(toBeRemoved);
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                var message = Utility.GetRootCauseOfException(ex);
                throw new Exception(message);
            }
        }

        public IEnumerable<ComponentListVM> Search(SearchParameters searchParams)
        {
            var result = _unitOfWork.Components.Search(searchParams)
                .Select(i => new ComponentListVM()
                {
					Id = i.Id,
					Code = i.Code,
					Description = i.Description,
					UnitsOfMeasure = i.UnitsOfMeasure,
					QtyOnHand = i.QtyOnHand,
					IsObsolete = i.IsObsolete,
					CreateDate = i.CreateDate,
					CreatedBy = i.CreatedBy,
					UpdateDate = i.UpdateDate,
					UpdatedBy = i.UpdatedBy,
					Manufacturer = i.Manufacturer,
                }).ToList();

            return result;
        }

        public ComponentDetailVM Update(ComponentDetailVM revisedVM)
        {
            try
            {
                var retrieved = _unitOfWork.Components.Get(revisedVM.Id);

				retrieved.Id = revisedVM.Id;
				retrieved.Code = revisedVM.Code;
				retrieved.Description = revisedVM.Description;
				retrieved.UnitsOfMeasure = revisedVM.UnitsOfMeasure;
				retrieved.QtyOnHand = revisedVM.QtyOnHand;
				var Documentlist = new List<Document>();
				if (revisedVM.Documents != null)
				{                    
					foreach(DocumentListVM vm in revisedVM.Documents)
					{
						var document = new Document();
						Map.AtoB(vm, document);
						Documentlist.Add(document);
					}
				}
				retrieved.Documents = Documentlist;
				retrieved.IsObsolete = revisedVM.IsObsolete;
				retrieved.CreateDate = revisedVM.CreateDate;
				retrieved.CreatedBy = revisedVM.CreatedBy;
				retrieved.UpdateDate = revisedVM.UpdateDate;
				retrieved.UpdatedBy = revisedVM.UpdatedBy;
				retrieved.Manufacturer = revisedVM.Manufacturer;
                var now = DateTime.Now;
                retrieved.UpdateDate = now;
                _unitOfWork.Complete();
                return revisedVM;
            }
            catch (Exception ex)
            {
                var message = Utility.GetRootCauseOfException(ex);
                throw new Exception(message);
            }
        }
    }

   public class ConnectorServiceBase : IConnectorServiceBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConnectorServiceBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ConnectorDetailVM Add(ConnectorDetailVM connectorVM)
        {
            try
            {
                var newConnector = new Connector();

                Map.AtoB(connectorVM, newConnector);

                var now = DateTime.Now;
                newConnector.CreateDate = now;
                newConnector.UpdateDate = now;

                _unitOfWork.Connectors.Add(newConnector);
                _unitOfWork.Complete();

                Map.AtoB(newConnector, connectorVM);

                return connectorVM;
            }
            catch (Exception ex)
            {
                var message = Utility.GetRootCauseOfException(ex);

                throw new Exception(message);
            }
        }

		public ConnectorDetailVM Get(int id)
        {
            var connector = _unitOfWork.Connectors.Get(id);
            var connectorVM = new ConnectorDetailVM();

            Map.AtoB(connector, connectorVM);

			var ToolList = new List<ToolListVM>();
			if(connector.Tools != null)
            {
				foreach(Tool tool in connector.Tools)
				{
					var vm = new ToolListVM();
					Map.AtoB(tool, vm);
					ToolList.Add(vm);
				}
			}
			connectorVM.Tools = ToolList;

            return connectorVM;
        }

        public IEnumerable<ConnectorListVM> GetAll()
        {
            var connectors = _unitOfWork.Connectors.GetAll().ToList();
            var connectorVMs = new List<ConnectorListVM>();

            Map.ListToList(connectors, connectorVMs);

            return connectorVMs;
        }

        public int GetTotalCount()
        {
            var count = _unitOfWork.Connectors.GetAll().Count();
            return count;
        }

        public void Remove(ConnectorDetailVM connectorVM)
        {
            try
            {
                var toBeRemoved = _unitOfWork.Connectors.Get(connectorVM.Id);
                if (toBeRemoved == null)
                    throw new Exception("Connector not found.");
                _unitOfWork.Connectors.Remove(toBeRemoved);
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                var message = Utility.GetRootCauseOfException(ex);
                throw new Exception(message);
            }
        }

        public IEnumerable<ConnectorListVM> Search(SearchParameters searchParams)
        {
            var result = _unitOfWork.Connectors.Search(searchParams)
                .Select(i => new ConnectorListVM()
                {
					Id = i.Id,
					Code = i.Code,
					Description = i.Description,
					UnitsOfMeasure = i.UnitsOfMeasure,
					QtyOnHand = i.QtyOnHand,
					IsObsolete = i.IsObsolete,
					CreateDate = i.CreateDate,
					CreatedBy = i.CreatedBy,
					UpdateDate = i.UpdateDate,
					UpdatedBy = i.UpdatedBy,
					Family = i.Family,
					Comments = i.Comments,
                }).ToList();

            return result;
        }

        public ConnectorDetailVM Update(ConnectorDetailVM revisedVM)
        {
            try
            {
                var retrieved = _unitOfWork.Connectors.Get(revisedVM.Id);

				retrieved.Id = revisedVM.Id;
				retrieved.Code = revisedVM.Code;
				retrieved.Description = revisedVM.Description;
				retrieved.UnitsOfMeasure = revisedVM.UnitsOfMeasure;
				retrieved.QtyOnHand = revisedVM.QtyOnHand;
				var Documentlist = new List<Document>();
				if (revisedVM.Documents != null)
				{                    
					foreach(DocumentListVM vm in revisedVM.Documents)
					{
						var document = new Document();
						Map.AtoB(vm, document);
						Documentlist.Add(document);
					}
				}
				retrieved.Documents = Documentlist;
				retrieved.IsObsolete = revisedVM.IsObsolete;
				retrieved.CreateDate = revisedVM.CreateDate;
				retrieved.CreatedBy = revisedVM.CreatedBy;
				retrieved.UpdateDate = revisedVM.UpdateDate;
				retrieved.UpdatedBy = revisedVM.UpdatedBy;
				retrieved.Family = revisedVM.Family;
				retrieved.Comments = revisedVM.Comments;
				var Toollist = new List<Tool>();
				if (revisedVM.Tools != null)
				{                    
					foreach(ToolListVM vm in revisedVM.Tools)
					{
						var tool = new Tool();
						Map.AtoB(vm, tool);
						Toollist.Add(tool);
					}
				}
				retrieved.Tools = Toollist;
                var now = DateTime.Now;
                retrieved.UpdateDate = now;
                _unitOfWork.Complete();
                return revisedVM;
            }
            catch (Exception ex)
            {
                var message = Utility.GetRootCauseOfException(ex);
                throw new Exception(message);
            }
        }
    }

   public class ContactServiceBase : IContactServiceBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactServiceBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ContactDetailVM Add(ContactDetailVM contactVM)
        {
            try
            {
                var newContact = new Contact();

                Map.AtoB(contactVM, newContact);

                var now = DateTime.Now;
                newContact.CreateDate = now;
                newContact.UpdateDate = now;

                _unitOfWork.Contacts.Add(newContact);
                _unitOfWork.Complete();

                Map.AtoB(newContact, contactVM);

                return contactVM;
            }
            catch (Exception ex)
            {
                var message = Utility.GetRootCauseOfException(ex);

                throw new Exception(message);
            }
        }

		public ContactDetailVM Get(int id)
        {
            var contact = _unitOfWork.Contacts.Get(id);
            var contactVM = new ContactDetailVM();

            Map.AtoB(contact, contactVM);

			var ToolList = new List<ToolListVM>();
			if(contact.Tools != null)
            {
				foreach(Tool tool in contact.Tools)
				{
					var vm = new ToolListVM();
					Map.AtoB(tool, vm);
					ToolList.Add(vm);
				}
			}
			contactVM.Tools = ToolList;

            return contactVM;
        }

        public IEnumerable<ContactListVM> GetAll()
        {
            var contacts = _unitOfWork.Contacts.GetAll().ToList();
            var contactVMs = new List<ContactListVM>();

            Map.ListToList(contacts, contactVMs);

            return contactVMs;
        }

        public int GetTotalCount()
        {
            var count = _unitOfWork.Contacts.GetAll().Count();
            return count;
        }

        public void Remove(ContactDetailVM contactVM)
        {
            try
            {
                var toBeRemoved = _unitOfWork.Contacts.Get(contactVM.Id);
                if (toBeRemoved == null)
                    throw new Exception("Contact not found.");
                _unitOfWork.Contacts.Remove(toBeRemoved);
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                var message = Utility.GetRootCauseOfException(ex);
                throw new Exception(message);
            }
        }

        public IEnumerable<ContactListVM> Search(SearchParameters searchParams)
        {
            var result = _unitOfWork.Contacts.Search(searchParams)
                .Select(i => new ContactListVM()
                {
					Id = i.Id,
					Code = i.Code,
					Description = i.Description,
					UnitsOfMeasure = i.UnitsOfMeasure,
					QtyOnHand = i.QtyOnHand,
					IsObsolete = i.IsObsolete,
					CreateDate = i.CreateDate,
					CreatedBy = i.CreatedBy,
					UpdateDate = i.UpdateDate,
					UpdatedBy = i.UpdatedBy,
					Size = i.Size,
					Family = i.Family,
					WireGageMin = i.WireGageMin,
					WireGageMax = i.WireGageMax,
					Comments = i.Comments,
                }).ToList();

            return result;
        }

        public ContactDetailVM Update(ContactDetailVM revisedVM)
        {
            try
            {
                var retrieved = _unitOfWork.Contacts.Get(revisedVM.Id);

				retrieved.Id = revisedVM.Id;
				retrieved.Code = revisedVM.Code;
				retrieved.Description = revisedVM.Description;
				retrieved.UnitsOfMeasure = revisedVM.UnitsOfMeasure;
				retrieved.QtyOnHand = revisedVM.QtyOnHand;
				var Documentlist = new List<Document>();
				if (revisedVM.Documents != null)
				{                    
					foreach(DocumentListVM vm in revisedVM.Documents)
					{
						var document = new Document();
						Map.AtoB(vm, document);
						Documentlist.Add(document);
					}
				}
				retrieved.Documents = Documentlist;
				retrieved.IsObsolete = revisedVM.IsObsolete;
				retrieved.CreateDate = revisedVM.CreateDate;
				retrieved.CreatedBy = revisedVM.CreatedBy;
				retrieved.UpdateDate = revisedVM.UpdateDate;
				retrieved.UpdatedBy = revisedVM.UpdatedBy;
				retrieved.Size = revisedVM.Size;
				retrieved.Family = revisedVM.Family;
				retrieved.WireGageMin = revisedVM.WireGageMin;
				retrieved.WireGageMax = revisedVM.WireGageMax;
				retrieved.Comments = revisedVM.Comments;
				var Toollist = new List<Tool>();
				if (revisedVM.Tools != null)
				{                    
					foreach(ToolListVM vm in revisedVM.Tools)
					{
						var tool = new Tool();
						Map.AtoB(vm, tool);
						Toollist.Add(tool);
					}
				}
				retrieved.Tools = Toollist;
                var now = DateTime.Now;
                retrieved.UpdateDate = now;
                _unitOfWork.Complete();
                return revisedVM;
            }
            catch (Exception ex)
            {
                var message = Utility.GetRootCauseOfException(ex);
                throw new Exception(message);
            }
        }
    }

   public class ItemServiceBase : IItemServiceBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ItemServiceBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ItemDetailVM Add(ItemDetailVM itemVM)
        {
            try
            {
                var newItem = new Item();

                Map.AtoB(itemVM, newItem);

                var now = DateTime.Now;
                newItem.CreateDate = now;
                newItem.UpdateDate = now;

                _unitOfWork.Items.Add(newItem);
                _unitOfWork.Complete();

                Map.AtoB(newItem, itemVM);

                return itemVM;
            }
            catch (Exception ex)
            {
                var message = Utility.GetRootCauseOfException(ex);

                throw new Exception(message);
            }
        }

		public ItemDetailVM Get(int id)
        {
            var item = _unitOfWork.Items.Get(id);
            var itemVM = new ItemDetailVM();

            Map.AtoB(item, itemVM);

			var DocumentList = new List<DocumentListVM>();
			if(item.Documents != null)
            {
				foreach(Document document in item.Documents)
				{
					var vm = new DocumentListVM();
					Map.AtoB(document, vm);
					DocumentList.Add(vm);
				}
			}
			itemVM.Documents = DocumentList;

            return itemVM;
        }

        public IEnumerable<ItemListVM> GetAll()
        {
            var items = _unitOfWork.Items.GetAll().ToList();
            var itemVMs = new List<ItemListVM>();

            Map.ListToList(items, itemVMs);

            return itemVMs;
        }

        public int GetTotalCount()
        {
            var count = _unitOfWork.Items.GetAll().Count();
            return count;
        }

        public void Remove(ItemDetailVM itemVM)
        {
            try
            {
                var toBeRemoved = _unitOfWork.Items.Get(itemVM.Id);
                if (toBeRemoved == null)
                    throw new Exception("Item not found.");
                _unitOfWork.Items.Remove(toBeRemoved);
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                var message = Utility.GetRootCauseOfException(ex);
                throw new Exception(message);
            }
        }

        public IEnumerable<ItemListVM> Search(SearchParameters searchParams)
        {
            var result = _unitOfWork.Items.Search(searchParams)
                .Select(i => new ItemListVM()
                {
					IsObsolete = i.IsObsolete,
					CreateDate = i.CreateDate,
					CreatedBy = i.CreatedBy,
					UpdateDate = i.UpdateDate,
					UpdatedBy = i.UpdatedBy,
					Id = i.Id,
					Code = i.Code,
					Description = i.Description,
					UnitsOfMeasure = i.UnitsOfMeasure,
					QtyOnHand = i.QtyOnHand,
                }).ToList();

            return result;
        }

        public ItemDetailVM Update(ItemDetailVM revisedVM)
        {
            try
            {
                var retrieved = _unitOfWork.Items.Get(revisedVM.Id);

				retrieved.IsObsolete = revisedVM.IsObsolete;
				retrieved.CreateDate = revisedVM.CreateDate;
				retrieved.CreatedBy = revisedVM.CreatedBy;
				retrieved.UpdateDate = revisedVM.UpdateDate;
				retrieved.UpdatedBy = revisedVM.UpdatedBy;
				retrieved.Id = revisedVM.Id;
				retrieved.Code = revisedVM.Code;
				retrieved.Description = revisedVM.Description;
				retrieved.UnitsOfMeasure = revisedVM.UnitsOfMeasure;
				retrieved.QtyOnHand = revisedVM.QtyOnHand;
				var Documentlist = new List<Document>();
				if (revisedVM.Documents != null)
				{                    
					foreach(DocumentListVM vm in revisedVM.Documents)
					{
						var document = new Document();
						Map.AtoB(vm, document);
						Documentlist.Add(document);
					}
				}
				retrieved.Documents = Documentlist;
                var now = DateTime.Now;
                retrieved.UpdateDate = now;
                _unitOfWork.Complete();
                return revisedVM;
            }
            catch (Exception ex)
            {
                var message = Utility.GetRootCauseOfException(ex);
                throw new Exception(message);
            }
        }
    }

   public class ToolServiceBase : IToolServiceBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ToolServiceBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ToolDetailVM Add(ToolDetailVM toolVM)
        {
            try
            {
                var newTool = new Tool();

                Map.AtoB(toolVM, newTool);

                var now = DateTime.Now;
                newTool.CreateDate = now;
                newTool.UpdateDate = now;

                _unitOfWork.Tools.Add(newTool);
                _unitOfWork.Complete();

                Map.AtoB(newTool, toolVM);

                return toolVM;
            }
            catch (Exception ex)
            {
                var message = Utility.GetRootCauseOfException(ex);

                throw new Exception(message);
            }
        }

		public ToolDetailVM Get(int id)
        {
            var tool = _unitOfWork.Tools.Get(id);
            var toolVM = new ToolDetailVM();

            Map.AtoB(tool, toolVM);

			var ConnectorList = new List<ConnectorListVM>();
			if(tool.Connectors != null)
            {
				foreach(Connector connector in tool.Connectors)
				{
					var vm = new ConnectorListVM();
					Map.AtoB(connector, vm);
					ConnectorList.Add(vm);
				}
			}
			toolVM.Connectors = ConnectorList;
			var ContactList = new List<ContactListVM>();
			if(tool.Contacts != null)
            {
				foreach(Contact contact in tool.Contacts)
				{
					var vm = new ContactListVM();
					Map.AtoB(contact, vm);
					ContactList.Add(vm);
				}
			}
			toolVM.Contacts = ContactList;

            return toolVM;
        }

        public IEnumerable<ToolListVM> GetAll()
        {
            var tools = _unitOfWork.Tools.GetAll().ToList();
            var toolVMs = new List<ToolListVM>();

            Map.ListToList(tools, toolVMs);

            return toolVMs;
        }

        public int GetTotalCount()
        {
            var count = _unitOfWork.Tools.GetAll().Count();
            return count;
        }

        public void Remove(ToolDetailVM toolVM)
        {
            try
            {
                var toBeRemoved = _unitOfWork.Tools.Get(toolVM.Id);
                if (toBeRemoved == null)
                    throw new Exception("Tool not found.");
                _unitOfWork.Tools.Remove(toBeRemoved);
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                var message = Utility.GetRootCauseOfException(ex);
                throw new Exception(message);
            }
        }

        public IEnumerable<ToolListVM> Search(SearchParameters searchParams)
        {
            var result = _unitOfWork.Tools.Search(searchParams)
                .Select(i => new ToolListVM()
                {
					Id = i.Id,
					Code = i.Code,
					Description = i.Description,
					UnitsOfMeasure = i.UnitsOfMeasure,
					QtyOnHand = i.QtyOnHand,
					IsObsolete = i.IsObsolete,
					CreateDate = i.CreateDate,
					CreatedBy = i.CreatedBy,
					UpdateDate = i.UpdateDate,
					UpdatedBy = i.UpdatedBy,
					Manufacturer = i.Manufacturer,
					BinNumber = i.BinNumber,
					MilitarySpecification = i.MilitarySpecification,
					SerialNumber = i.SerialNumber,
					Comments = i.Comments,
                }).ToList();

            return result;
        }

        public ToolDetailVM Update(ToolDetailVM revisedVM)
        {
            try
            {
                var retrieved = _unitOfWork.Tools.Get(revisedVM.Id);

				retrieved.Id = revisedVM.Id;
				retrieved.Code = revisedVM.Code;
				retrieved.Description = revisedVM.Description;
				retrieved.UnitsOfMeasure = revisedVM.UnitsOfMeasure;
				retrieved.QtyOnHand = revisedVM.QtyOnHand;
				var Documentlist = new List<Document>();
				if (revisedVM.Documents != null)
				{                    
					foreach(DocumentListVM vm in revisedVM.Documents)
					{
						var document = new Document();
						Map.AtoB(vm, document);
						Documentlist.Add(document);
					}
				}
				retrieved.Documents = Documentlist;
				retrieved.IsObsolete = revisedVM.IsObsolete;
				retrieved.CreateDate = revisedVM.CreateDate;
				retrieved.CreatedBy = revisedVM.CreatedBy;
				retrieved.UpdateDate = revisedVM.UpdateDate;
				retrieved.UpdatedBy = revisedVM.UpdatedBy;
				retrieved.Manufacturer = revisedVM.Manufacturer;
				retrieved.BinNumber = revisedVM.BinNumber;
				retrieved.MilitarySpecification = revisedVM.MilitarySpecification;
				retrieved.SerialNumber = revisedVM.SerialNumber;
				retrieved.Comments = revisedVM.Comments;
				var Connectorlist = new List<Connector>();
				if (revisedVM.Connectors != null)
				{                    
					foreach(ConnectorListVM vm in revisedVM.Connectors)
					{
						var connector = new Connector();
						Map.AtoB(vm, connector);
						Connectorlist.Add(connector);
					}
				}
				retrieved.Connectors = Connectorlist;
				var Contactlist = new List<Contact>();
				if (revisedVM.Contacts != null)
				{                    
					foreach(ContactListVM vm in revisedVM.Contacts)
					{
						var contact = new Contact();
						Map.AtoB(vm, contact);
						Contactlist.Add(contact);
					}
				}
				retrieved.Contacts = Contactlist;
                var now = DateTime.Now;
                retrieved.UpdateDate = now;
                _unitOfWork.Complete();
                return revisedVM;
            }
            catch (Exception ex)
            {
                var message = Utility.GetRootCauseOfException(ex);
                throw new Exception(message);
            }
        }
    }

   public class AssemblyServiceBase : IAssemblyServiceBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AssemblyServiceBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public AssemblyDetailVM Add(AssemblyDetailVM assemblyVM)
        {
            try
            {
                var newAssembly = new Assembly();

                Map.AtoB(assemblyVM, newAssembly);

                var now = DateTime.Now;
                newAssembly.CreateDate = now;
                newAssembly.UpdateDate = now;

                _unitOfWork.Assemblys.Add(newAssembly);
                _unitOfWork.Complete();

                Map.AtoB(newAssembly, assemblyVM);

                return assemblyVM;
            }
            catch (Exception ex)
            {
                var message = Utility.GetRootCauseOfException(ex);

                throw new Exception(message);
            }
        }

		public AssemblyDetailVM Get(int id)
        {
            var assembly = _unitOfWork.Assemblys.Get(id);
            var assemblyVM = new AssemblyDetailVM();

            Map.AtoB(assembly, assemblyVM);

			var AssemblyItemList = new List<AssemblyItemListVM>();
			if(assembly.AssemblyItems != null)
            {
				foreach(AssemblyItem assemblyitem in assembly.AssemblyItems)
				{
					var vm = new AssemblyItemListVM();
					Map.AtoB(assemblyitem, vm);
					AssemblyItemList.Add(vm);
				}
			}
			assemblyVM.AssemblyItems = AssemblyItemList;

            return assemblyVM;
        }

        public IEnumerable<AssemblyListVM> GetAll()
        {
            var assemblys = _unitOfWork.Assemblys.GetAll().ToList();
            var assemblyVMs = new List<AssemblyListVM>();

            Map.ListToList(assemblys, assemblyVMs);

            return assemblyVMs;
        }

        public int GetTotalCount()
        {
            var count = _unitOfWork.Assemblys.GetAll().Count();
            return count;
        }

        public void Remove(AssemblyDetailVM assemblyVM)
        {
            try
            {
                var toBeRemoved = _unitOfWork.Assemblys.Get(assemblyVM.Id);
                if (toBeRemoved == null)
                    throw new Exception("Assembly not found.");
                _unitOfWork.Assemblys.Remove(toBeRemoved);
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                var message = Utility.GetRootCauseOfException(ex);
                throw new Exception(message);
            }
        }

        public IEnumerable<AssemblyListVM> Search(SearchParameters searchParams)
        {
            var result = _unitOfWork.Assemblys.Search(searchParams)
                .Select(i => new AssemblyListVM()
                {
					Id = i.Id,
					Code = i.Code,
					Description = i.Description,
					UnitsOfMeasure = i.UnitsOfMeasure,
					QtyOnHand = i.QtyOnHand,
					IsObsolete = i.IsObsolete,
					CreateDate = i.CreateDate,
					CreatedBy = i.CreatedBy,
					UpdateDate = i.UpdateDate,
					UpdatedBy = i.UpdatedBy,
					Rev = i.Rev,
                }).ToList();

            return result;
        }

        public AssemblyDetailVM Update(AssemblyDetailVM revisedVM)
        {
            try
            {
                var retrieved = _unitOfWork.Assemblys.Get(revisedVM.Id);

				retrieved.Id = revisedVM.Id;
				retrieved.Code = revisedVM.Code;
				retrieved.Description = revisedVM.Description;
				retrieved.UnitsOfMeasure = revisedVM.UnitsOfMeasure;
				retrieved.QtyOnHand = revisedVM.QtyOnHand;
				var Documentlist = new List<Document>();
				if (revisedVM.Documents != null)
				{                    
					foreach(DocumentListVM vm in revisedVM.Documents)
					{
						var document = new Document();
						Map.AtoB(vm, document);
						Documentlist.Add(document);
					}
				}
				retrieved.Documents = Documentlist;
				retrieved.IsObsolete = revisedVM.IsObsolete;
				retrieved.CreateDate = revisedVM.CreateDate;
				retrieved.CreatedBy = revisedVM.CreatedBy;
				retrieved.UpdateDate = revisedVM.UpdateDate;
				retrieved.UpdatedBy = revisedVM.UpdatedBy;
				retrieved.Rev = revisedVM.Rev;
				var AssemblyItemlist = new List<AssemblyItem>();
				if (revisedVM.AssemblyItems != null)
				{                    
					foreach(AssemblyItemListVM vm in revisedVM.AssemblyItems)
					{
						var assemblyitem = new AssemblyItem();
						Map.AtoB(vm, assemblyitem);
						AssemblyItemlist.Add(assemblyitem);
					}
				}
				retrieved.AssemblyItems = AssemblyItemlist;
                var now = DateTime.Now;
                retrieved.UpdateDate = now;
                _unitOfWork.Complete();
                return revisedVM;
            }
            catch (Exception ex)
            {
                var message = Utility.GetRootCauseOfException(ex);
                throw new Exception(message);
            }
        }
    }

   public class AssemblyItemServiceBase : IAssemblyItemServiceBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AssemblyItemServiceBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public AssemblyItemDetailVM Add(AssemblyItemDetailVM assemblyitemVM)
        {
            try
            {
                var newAssemblyItem = new AssemblyItem();

                Map.AtoB(assemblyitemVM, newAssemblyItem);

                var now = DateTime.Now;
                newAssemblyItem.CreateDate = now;
                newAssemblyItem.UpdateDate = now;

                _unitOfWork.AssemblyItems.Add(newAssemblyItem);
                _unitOfWork.Complete();

                Map.AtoB(newAssemblyItem, assemblyitemVM);

                return assemblyitemVM;
            }
            catch (Exception ex)
            {
                var message = Utility.GetRootCauseOfException(ex);

                throw new Exception(message);
            }
        }

		public AssemblyItemDetailVM Get(int id)
        {
            var assemblyitem = _unitOfWork.AssemblyItems.Get(id);
            var assemblyitemVM = new AssemblyItemDetailVM();

            Map.AtoB(assemblyitem, assemblyitemVM);


            return assemblyitemVM;
        }

        public IEnumerable<AssemblyItemListVM> GetAll()
        {
            var assemblyitems = _unitOfWork.AssemblyItems.GetAll().ToList();
            var assemblyitemVMs = new List<AssemblyItemListVM>();

            Map.ListToList(assemblyitems, assemblyitemVMs);

            return assemblyitemVMs;
        }

        public int GetTotalCount()
        {
            var count = _unitOfWork.AssemblyItems.GetAll().Count();
            return count;
        }

        public void Remove(AssemblyItemDetailVM assemblyitemVM)
        {
            try
            {
                var toBeRemoved = _unitOfWork.AssemblyItems.Get(assemblyitemVM.Id);
                if (toBeRemoved == null)
                    throw new Exception("AssemblyItem not found.");
                _unitOfWork.AssemblyItems.Remove(toBeRemoved);
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                var message = Utility.GetRootCauseOfException(ex);
                throw new Exception(message);
            }
        }


        public AssemblyItemDetailVM Update(AssemblyItemDetailVM revisedVM)
        {
            try
            {
                var retrieved = _unitOfWork.AssemblyItems.Get(revisedVM.Id);

				retrieved.IsObsolete = revisedVM.IsObsolete;
				retrieved.CreateDate = revisedVM.CreateDate;
				retrieved.CreatedBy = revisedVM.CreatedBy;
				retrieved.UpdateDate = revisedVM.UpdateDate;
				retrieved.UpdatedBy = revisedVM.UpdatedBy;
				retrieved.Id = revisedVM.Id;
				retrieved.Assembly = revisedVM.Assembly;
				retrieved.Item = revisedVM.Item;
				retrieved.Qty = revisedVM.Qty;
                var now = DateTime.Now;
                retrieved.UpdateDate = now;
                _unitOfWork.Complete();
                return revisedVM;
            }
            catch (Exception ex)
            {
                var message = Utility.GetRootCauseOfException(ex);
                throw new Exception(message);
            }
        }
    }

}