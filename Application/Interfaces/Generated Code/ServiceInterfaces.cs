// Generated Code! Do not manually edit. Adjust template (ServiceInterfaces.tt) to make changes to this file.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public interface IUserServiceBase
    {
        IEnumerable<UserListVM> Search(SearchParameters searchParams);
		int SearchResultsCount(SearchParameters searchParams);
		UserDetailVM Get(int id);
        IEnumerable<UserListVM> GetAll();
        int GetTotalCount();
        UserDetailVM Add(UserDetailVM userVM);
        UserDetailVM Update(UserDetailVM userVM);
        void Remove(UserDetailVM userVM);
    }
    public interface IDocumentServiceBase
    {
        IEnumerable<DocumentListVM> Search(SearchParameters searchParams);
		int SearchResultsCount(SearchParameters searchParams);
		DocumentDetailVM Get(int id);
        IEnumerable<DocumentListVM> GetAll();
        int GetTotalCount();
        DocumentDetailVM Add(DocumentDetailVM documentVM);
        DocumentDetailVM Update(DocumentDetailVM documentVM);
        void Remove(DocumentDetailVM documentVM);
    }
    public interface IComponentServiceBase
    {
        IEnumerable<ComponentListVM> Search(SearchParameters searchParams);
		int SearchResultsCount(SearchParameters searchParams);
		ComponentDetailVM Get(int id);
        IEnumerable<ComponentListVM> GetAll();
        int GetTotalCount();
        ComponentDetailVM Add(ComponentDetailVM componentVM);
        ComponentDetailVM Update(ComponentDetailVM componentVM);
        void Remove(ComponentDetailVM componentVM);
    }
    public interface IConnectorServiceBase
    {
        IEnumerable<ConnectorListVM> Search(SearchParameters searchParams);
		int SearchResultsCount(SearchParameters searchParams);
		ConnectorDetailVM Get(int id);
        IEnumerable<ConnectorListVM> GetAll();
        int GetTotalCount();
        ConnectorDetailVM Add(ConnectorDetailVM connectorVM);
        ConnectorDetailVM Update(ConnectorDetailVM connectorVM);
        void Remove(ConnectorDetailVM connectorVM);
    }
    public interface IContactServiceBase
    {
        IEnumerable<ContactListVM> Search(SearchParameters searchParams);
		int SearchResultsCount(SearchParameters searchParams);
		ContactDetailVM Get(int id);
        IEnumerable<ContactListVM> GetAll();
        int GetTotalCount();
        ContactDetailVM Add(ContactDetailVM contactVM);
        ContactDetailVM Update(ContactDetailVM contactVM);
        void Remove(ContactDetailVM contactVM);
    }
    public interface IItemServiceBase
    {
        IEnumerable<ItemListVM> Search(SearchParameters searchParams);
		int SearchResultsCount(SearchParameters searchParams);
		ItemDetailVM Get(int id);
        IEnumerable<ItemListVM> GetAll();
        int GetTotalCount();
        ItemDetailVM Add(ItemDetailVM itemVM);
        ItemDetailVM Update(ItemDetailVM itemVM);
        void Remove(ItemDetailVM itemVM);
    }
    public interface IToolServiceBase
    {
        IEnumerable<ToolListVM> Search(SearchParameters searchParams);
		int SearchResultsCount(SearchParameters searchParams);
		ToolDetailVM Get(int id);
        IEnumerable<ToolListVM> GetAll();
        int GetTotalCount();
        ToolDetailVM Add(ToolDetailVM toolVM);
        ToolDetailVM Update(ToolDetailVM toolVM);
        void Remove(ToolDetailVM toolVM);
    }
    public interface IAssemblyServiceBase
    {
        IEnumerable<AssemblyListVM> Search(SearchParameters searchParams);
		int SearchResultsCount(SearchParameters searchParams);
		AssemblyDetailVM Get(int id);
        IEnumerable<AssemblyListVM> GetAll();
        int GetTotalCount();
        AssemblyDetailVM Add(AssemblyDetailVM assemblyVM);
        AssemblyDetailVM Update(AssemblyDetailVM assemblyVM);
        void Remove(AssemblyDetailVM assemblyVM);
    }
    public interface IAssemblyItemServiceBase
    {
        IEnumerable<AssemblyItemListVM> Search(SearchParameters searchParams);
		int SearchResultsCount(SearchParameters searchParams);
		AssemblyItemDetailVM Get(int id);
        IEnumerable<AssemblyItemListVM> GetAll();
        int GetTotalCount();
        AssemblyItemDetailVM Add(AssemblyItemDetailVM assemblyitemVM);
        AssemblyItemDetailVM Update(AssemblyItemDetailVM assemblyitemVM);
        void Remove(AssemblyItemDetailVM assemblyitemVM);
    }
    public interface IWorkOrderServiceBase
    {
        IEnumerable<WorkOrderListVM> Search(SearchParameters searchParams);
		int SearchResultsCount(SearchParameters searchParams);
		WorkOrderDetailVM Get(int id);
        IEnumerable<WorkOrderListVM> GetAll();
        int GetTotalCount();
        WorkOrderDetailVM Add(WorkOrderDetailVM workorderVM);
        WorkOrderDetailVM Update(WorkOrderDetailVM workorderVM);
        void Remove(WorkOrderDetailVM workorderVM);
    }
}