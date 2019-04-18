using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Items;
using Domain.Assemblies;
using Domain.Documents;
using Domain.Users;

namespace Persistance
{
    public class Camm2Context : DbContext
    {
        public Camm2Context() : base("Camm2Db") { }

        public DbSet<Item> Items { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<AssemblyComponent> AssemblyComponents { get; set; }
        public DbSet<Assembly> Assemblies { get; set; }        
        public DbSet<User> Users { get; set; }
        public DbSet<Tool> Tools { get; set; }
        public DbSet<Connector> Connectors { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Document> Documents { get; set; }
    }
}
