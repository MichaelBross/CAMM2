 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Persistance.Repository;
using Domain;
using Application;
using Application.Interfaces;

namespace Persistance
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(Camm2Context context)
            :base(context)
        {
        }
               
        public IEnumerable<User> Search(SearchParameters searchParams)
        {
            var query = Camm2Context.Users.AsQueryable();

            if (!String.IsNullOrEmpty(searchParams.SearchValue))
            {
                string[] terms = searchParams.SearchValue.Split(' ');

                foreach (string term in terms)
                {
                    query = query.Where(q =>
					q.FirstName.Contains(term)
					|| q.LastName.Contains(term)
					|| q.EmailAddress.Contains(term)
					);					
                }
            }

            query = query.OrderBy(searchParams.SortColumnName + " " + searchParams.SortDirection);
            query = query.Skip(searchParams.Start).Take(searchParams.Length);

            return query.ToList();
        }
        public Camm2Context Camm2Context
        {
            get { return Context as Camm2Context; }
        }
    }

    public class DocumentRepository : Repository<Document>, IDocumentRepository
    {
        public DocumentRepository(Camm2Context context)
            :base(context)
        {
        }
               
        public IEnumerable<Document> Search(SearchParameters searchParams)
        {
            var query = Camm2Context.Documents.AsQueryable();

            if (!String.IsNullOrEmpty(searchParams.SearchValue))
            {
                string[] terms = searchParams.SearchValue.Split(' ');

                foreach (string term in terms)
                {
                    query = query.Where(q =>
					q.Code.Contains(term)
					|| q.Rev.Contains(term)
					|| q.Title.Contains(term)
					);					
                }
            }

            query = query.OrderBy(searchParams.SortColumnName + " " + searchParams.SortDirection);
            query = query.Skip(searchParams.Start).Take(searchParams.Length);

            return query.ToList();
        }
        public Camm2Context Camm2Context
        {
            get { return Context as Camm2Context; }
        }
    }

    public class ComponentRepository : Repository<Component>, IComponentRepository
    {
        public ComponentRepository(Camm2Context context)
            :base(context)
        {
        }
               
        public IEnumerable<Component> Search(SearchParameters searchParams)
        {
            var query = Camm2Context.Components.AsQueryable();

            if (!String.IsNullOrEmpty(searchParams.SearchValue))
            {
                string[] terms = searchParams.SearchValue.Split(' ');

                foreach (string term in terms)
                {
                    query = query.Where(q =>
					q.Manufacturer.Contains(term)
					);					
                }
            }

            query = query.OrderBy(searchParams.SortColumnName + " " + searchParams.SortDirection);
            query = query.Skip(searchParams.Start).Take(searchParams.Length);

            return query.ToList();
        }
        public Camm2Context Camm2Context
        {
            get { return Context as Camm2Context; }
        }
    }

    public class ConnectorRepository : Repository<Connector>, IConnectorRepository
    {
        public ConnectorRepository(Camm2Context context)
            :base(context)
        {
        }
               
        public IEnumerable<Connector> Search(SearchParameters searchParams)
        {
            var query = Camm2Context.Connectors.AsQueryable();

            if (!String.IsNullOrEmpty(searchParams.SearchValue))
            {
                string[] terms = searchParams.SearchValue.Split(' ');

                foreach (string term in terms)
                {
                    query = query.Where(q =>
					q.Family.Contains(term)
					|| q.Comments.Contains(term)
					);					
                }
            }

            query = query.OrderBy(searchParams.SortColumnName + " " + searchParams.SortDirection);
            query = query.Skip(searchParams.Start).Take(searchParams.Length);

            return query.ToList();
        }
        public Camm2Context Camm2Context
        {
            get { return Context as Camm2Context; }
        }
    }

    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        public ContactRepository(Camm2Context context)
            :base(context)
        {
        }
               
        public IEnumerable<Contact> Search(SearchParameters searchParams)
        {
            var query = Camm2Context.Contacts.AsQueryable();

            if (!String.IsNullOrEmpty(searchParams.SearchValue))
            {
                string[] terms = searchParams.SearchValue.Split(' ');

                foreach (string term in terms)
                {
                    query = query.Where(q =>
					q.Size.Contains(term)
					|| q.Family.Contains(term)
					|| q.Comments.Contains(term)
					);					
                }
            }

            query = query.OrderBy(searchParams.SortColumnName + " " + searchParams.SortDirection);
            query = query.Skip(searchParams.Start).Take(searchParams.Length);

            return query.ToList();
        }
        public Camm2Context Camm2Context
        {
            get { return Context as Camm2Context; }
        }
    }

    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(Camm2Context context)
            :base(context)
        {
        }
               
        public IEnumerable<Item> Search(SearchParameters searchParams)
        {
            var query = Camm2Context.Items.AsQueryable();

            if (!String.IsNullOrEmpty(searchParams.SearchValue))
            {
                string[] terms = searchParams.SearchValue.Split(' ');

                foreach (string term in terms)
                {
                    query = query.Where(q =>
					q.Code.Contains(term)
					|| q.Description.Contains(term)
					);					
                }
            }

            query = query.OrderBy(searchParams.SortColumnName + " " + searchParams.SortDirection);
            query = query.Skip(searchParams.Start).Take(searchParams.Length);

            return query.ToList();
        }
        public Camm2Context Camm2Context
        {
            get { return Context as Camm2Context; }
        }
    }

    public class ToolRepository : Repository<Tool>, IToolRepository
    {
        public ToolRepository(Camm2Context context)
            :base(context)
        {
        }
               
        public IEnumerable<Tool> Search(SearchParameters searchParams)
        {
            var query = Camm2Context.Tools.AsQueryable();

            if (!String.IsNullOrEmpty(searchParams.SearchValue))
            {
                string[] terms = searchParams.SearchValue.Split(' ');

                foreach (string term in terms)
                {
                    query = query.Where(q =>
					q.Manufacturer.Contains(term)
					|| q.BinNumber.Contains(term)
					|| q.MilitarySpecification.Contains(term)
					|| q.SerialNumber.Contains(term)
					|| q.Comments.Contains(term)
					);					
                }
            }

            query = query.OrderBy(searchParams.SortColumnName + " " + searchParams.SortDirection);
            query = query.Skip(searchParams.Start).Take(searchParams.Length);

            return query.ToList();
        }
        public Camm2Context Camm2Context
        {
            get { return Context as Camm2Context; }
        }
    }

    public class AssemblyRepository : Repository<Assembly>, IAssemblyRepository
    {
        public AssemblyRepository(Camm2Context context)
            :base(context)
        {
        }
               
        public IEnumerable<Assembly> Search(SearchParameters searchParams)
        {
            var query = Camm2Context.Assemblies.AsQueryable();

            if (!String.IsNullOrEmpty(searchParams.SearchValue))
            {
                string[] terms = searchParams.SearchValue.Split(' ');

                foreach (string term in terms)
                {
                    query = query.Where(q =>
					q.Rev.Contains(term)
					);					
                }
            }

            query = query.OrderBy(searchParams.SortColumnName + " " + searchParams.SortDirection);
            query = query.Skip(searchParams.Start).Take(searchParams.Length);

            return query.ToList();
        }
        public Camm2Context Camm2Context
        {
            get { return Context as Camm2Context; }
        }
    }

    public class AssemblyComponentRepository : Repository<AssemblyComponent>, IAssemblyComponentRepository
    {
        public AssemblyComponentRepository(Camm2Context context)
            :base(context)
        {
        }
        public Camm2Context Camm2Context
        {
            get { return Context as Camm2Context; }
        }
    }

}
