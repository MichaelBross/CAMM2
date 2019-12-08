namespace Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAssemblyItemPropertiesLineNumberAndReference : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AssemblyItems", "LineNumber", c => c.Int(nullable: false));
            AddColumn("dbo.AssemblyItems", "Reference", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AssemblyItems", "Reference");
            DropColumn("dbo.AssemblyItems", "LineNumber");
        }
    }
}
