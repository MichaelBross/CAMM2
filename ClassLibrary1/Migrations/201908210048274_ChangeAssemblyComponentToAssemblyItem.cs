namespace Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAssemblyComponentToAssemblyItem : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AssemblyComponents", newName: "AssemblyItems");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.AssemblyItems", newName: "AssemblyComponents");
        }
    }
}
