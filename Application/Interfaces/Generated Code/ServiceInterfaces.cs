﻿ 

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
        IEnumerable<UserListVM> GetAll();
        int GetTotalCount();
        UserDetailVM Add(UserDetailVM userVM);
        UserDetailVM Update(UserDetailVM userVM);
        void Remove(UserDetailVM userVM);
    }
    public interface IDocumentServiceBase
    {
        IEnumerable<DocumentListVM> Search(SearchParameters searchParams);
        IEnumerable<DocumentListVM> GetAll();
        int GetTotalCount();
        DocumentDetailVM Add(DocumentDetailVM documentVM);
        DocumentDetailVM Update(DocumentDetailVM documentVM);
        void Remove(DocumentDetailVM documentVM);
    }
    public interface IComponentServiceBase
    {
        IEnumerable<ComponentListVM> Search(SearchParameters searchParams);
        IEnumerable<ComponentListVM> GetAll();
        int GetTotalCount();
        ComponentDetailVM Add(ComponentDetailVM componentVM);
        ComponentDetailVM Update(ComponentDetailVM componentVM);
        void Remove(ComponentDetailVM componentVM);
    }
    public interface IConnectorServiceBase
    {
        IEnumerable<ConnectorListVM> Search(SearchParameters searchParams);
        IEnumerable<ConnectorListVM> GetAll();
        int GetTotalCount();
        ConnectorDetailVM Add(ConnectorDetailVM connectorVM);
        ConnectorDetailVM Update(ConnectorDetailVM connectorVM);
        void Remove(ConnectorDetailVM connectorVM);
    }
    public interface IContactServiceBase
    {
        IEnumerable<ContactListVM> Search(SearchParameters searchParams);
        IEnumerable<ContactListVM> GetAll();
        int GetTotalCount();
        ContactDetailVM Add(ContactDetailVM contactVM);
        ContactDetailVM Update(ContactDetailVM contactVM);
        void Remove(ContactDetailVM contactVM);
    }
    public interface IItemServiceBase
    {
        IEnumerable<ItemListVM> Search(SearchParameters searchParams);
        IEnumerable<ItemListVM> GetAll();
        int GetTotalCount();
        ItemDetailVM Add(ItemDetailVM itemVM);
        ItemDetailVM Update(ItemDetailVM itemVM);
        void Remove(ItemDetailVM itemVM);
    }
    public interface IToolServiceBase
    {
        IEnumerable<ToolListVM> Search(SearchParameters searchParams);
        IEnumerable<ToolListVM> GetAll();
        int GetTotalCount();
        ToolDetailVM Add(ToolDetailVM toolVM);
        ToolDetailVM Update(ToolDetailVM toolVM);
        void Remove(ToolDetailVM toolVM);
    }
    public interface IAssemblyServiceBase
    {
        IEnumerable<AssemblyListVM> Search(SearchParameters searchParams);
        IEnumerable<AssemblyListVM> GetAll();
        int GetTotalCount();
        AssemblyDetailVM Add(AssemblyDetailVM assemblyVM);
        AssemblyDetailVM Update(AssemblyDetailVM assemblyVM);
        void Remove(AssemblyDetailVM assemblyVM);
    }
    public interface IAssemblyComponentServiceBase
    {
        IEnumerable<AssemblyComponentListVM> GetAll();
        int GetTotalCount();
        AssemblyComponentDetailVM Add(AssemblyComponentDetailVM assemblycomponentVM);
        AssemblyComponentDetailVM Update(AssemblyComponentDetailVM assemblycomponentVM);
        void Remove(AssemblyComponentDetailVM assemblycomponentVM);
    }
}