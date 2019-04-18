using System.Collections.Generic;
using Domain.Items;

namespace Persistance.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Persistance.Camm2Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Persistance.Camm2Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var connectors = new List<Connector>
            {
                new Connector
                {
                    Code = "D38999WD26N",
                    Description = "Connector Multipin",
                    UnitsOfMeasure = UnitsOfMeasure.Each,
                    QtyOnHand = 1,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                }
            };
        }
    }
}
