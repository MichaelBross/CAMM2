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
            var users = _unitOfWork.Users.GetAll();
            var userVMs = new List<UserListVM>();

            Map.AtoB(users, userVMs);

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

            return documentVM;
        }

        public IEnumerable<DocumentListVM> GetAll()
        {
            var documents = _unitOfWork.Documents.GetAll();
            var documentVMs = new List<DocumentListVM>();

            Map.AtoB(documents, documentVMs);

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
					Items = i.Items,
                }).ToList();

            return result;
        }

        public DocumentDetailVM Update(DocumentDetailVM revisedVM)
        {
            try
            {
                var retrieved = _unitOfWork.Documents.Get(revisedVM.Id);
				retrieved.Id = revisedVM.Id;
				retrieved.Code = revisedVM.Code;
				retrieved.Rev = revisedVM.Rev;
				retrieved.Title = revisedVM.Title;
				retrieved.DocType = revisedVM.DocType;
				retrieved.Items = revisedVM.Items;
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
            var components = _unitOfWork.Components.GetAll();
            var componentVMs = new List<ComponentListVM>();

            Map.AtoB(components, componentVMs);

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
					Documents = i.Documents,
					Manufacturer = i.Manufacturer,
                }).ToList();

            return result;
        }

        public ComponentDetailVM Update(ComponentDetailVM revisedVM)
        {
            try
            {
                var retrieved = _unitOfWork.Components.Get(revisedVM.Id);
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

            return connectorVM;
        }

        public IEnumerable<ConnectorListVM> GetAll()
        {
            var connectors = _unitOfWork.Connectors.GetAll();
            var connectorVMs = new List<ConnectorListVM>();

            Map.AtoB(connectors, connectorVMs);

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
					Documents = i.Documents,
					Family = i.Family,
					Comments = i.Comments,
					Tools = i.Tools,
                }).ToList();

            return result;
        }

        public ConnectorDetailVM Update(ConnectorDetailVM revisedVM)
        {
            try
            {
                var retrieved = _unitOfWork.Connectors.Get(revisedVM.Id);
				retrieved.Family = revisedVM.Family;
				retrieved.Comments = revisedVM.Comments;
				retrieved.Tools = revisedVM.Tools;
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

            return contactVM;
        }

        public IEnumerable<ContactListVM> GetAll()
        {
            var contacts = _unitOfWork.Contacts.GetAll();
            var contactVMs = new List<ContactListVM>();

            Map.AtoB(contacts, contactVMs);

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
					Documents = i.Documents,
					Size = i.Size,
					Family = i.Family,
					WireGageMin = i.WireGageMin,
					WireGageMax = i.WireGageMax,
					Comments = i.Comments,
					Tools = i.Tools,
                }).ToList();

            return result;
        }

        public ContactDetailVM Update(ContactDetailVM revisedVM)
        {
            try
            {
                var retrieved = _unitOfWork.Contacts.Get(revisedVM.Id);
				retrieved.Size = revisedVM.Size;
				retrieved.Family = revisedVM.Family;
				retrieved.WireGageMin = revisedVM.WireGageMin;
				retrieved.WireGageMax = revisedVM.WireGageMax;
				retrieved.Comments = revisedVM.Comments;
				retrieved.Tools = revisedVM.Tools;
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

            return itemVM;
        }

        public IEnumerable<ItemListVM> GetAll()
        {
            var items = _unitOfWork.Items.GetAll();
            var itemVMs = new List<ItemListVM>();

            Map.AtoB(items, itemVMs);

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
					Documents = i.Documents,
                }).ToList();

            return result;
        }

        public ItemDetailVM Update(ItemDetailVM revisedVM)
        {
            try
            {
                var retrieved = _unitOfWork.Items.Get(revisedVM.Id);
				retrieved.Id = revisedVM.Id;
				retrieved.Code = revisedVM.Code;
				retrieved.Description = revisedVM.Description;
				retrieved.UnitsOfMeasure = revisedVM.UnitsOfMeasure;
				retrieved.QtyOnHand = revisedVM.QtyOnHand;
				retrieved.Documents = revisedVM.Documents;
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

            return toolVM;
        }

        public IEnumerable<ToolListVM> GetAll()
        {
            var tools = _unitOfWork.Tools.GetAll();
            var toolVMs = new List<ToolListVM>();

            Map.AtoB(tools, toolVMs);

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
					Documents = i.Documents,
					Manufacturer = i.Manufacturer,
					BinNumber = i.BinNumber,
					MilitarySpecification = i.MilitarySpecification,
					SerialNumber = i.SerialNumber,
					Comments = i.Comments,
					Connectors = i.Connectors,
					Contacts = i.Contacts,
                }).ToList();

            return result;
        }

        public ToolDetailVM Update(ToolDetailVM revisedVM)
        {
            try
            {
                var retrieved = _unitOfWork.Tools.Get(revisedVM.Id);
				retrieved.Manufacturer = revisedVM.Manufacturer;
				retrieved.BinNumber = revisedVM.BinNumber;
				retrieved.MilitarySpecification = revisedVM.MilitarySpecification;
				retrieved.SerialNumber = revisedVM.SerialNumber;
				retrieved.Comments = revisedVM.Comments;
				retrieved.Connectors = revisedVM.Connectors;
				retrieved.Contacts = revisedVM.Contacts;
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

            return assemblyVM;
        }

        public IEnumerable<AssemblyListVM> GetAll()
        {
            var assemblys = _unitOfWork.Assemblys.GetAll();
            var assemblyVMs = new List<AssemblyListVM>();

            Map.AtoB(assemblys, assemblyVMs);

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
					Documents = i.Documents,
					Rev = i.Rev,
					AssemblyItems = i.AssemblyItems,
                }).ToList();

            return result;
        }

        public AssemblyDetailVM Update(AssemblyDetailVM revisedVM)
        {
            try
            {
                var retrieved = _unitOfWork.Assemblys.Get(revisedVM.Id);
				retrieved.Rev = revisedVM.Rev;
				retrieved.AssemblyItems = revisedVM.AssemblyItems;
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
            var assemblyitems = _unitOfWork.AssemblyItems.GetAll();
            var assemblyitemVMs = new List<AssemblyItemListVM>();

            Map.AtoB(assemblyitems, assemblyitemVMs);

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