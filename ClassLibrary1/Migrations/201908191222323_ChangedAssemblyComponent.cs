namespace Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedAssemblyComponent : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ItemDocuments", newName: "DocumentItems");
            RenameColumn(table: "dbo.AssemblyComponents", name: "Component_Id", newName: "Item_Id");
            RenameIndex(table: "dbo.AssemblyComponents", name: "IX_Component_Id", newName: "IX_Item_Id");
            DropPrimaryKey("dbo.DocumentItems");
            AddPrimaryKey("dbo.DocumentItems", new[] { "Document_Id", "Item_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.DocumentItems");
            AddPrimaryKey("dbo.DocumentItems", new[] { "Item_Id", "Document_Id" });
            RenameIndex(table: "dbo.AssemblyComponents", name: "IX_Item_Id", newName: "IX_Component_Id");
            RenameColumn(table: "dbo.AssemblyComponents", name: "Item_Id", newName: "Component_Id");
            RenameTable(name: "dbo.DocumentItems", newName: "ItemDocuments");
        }
    }
}
