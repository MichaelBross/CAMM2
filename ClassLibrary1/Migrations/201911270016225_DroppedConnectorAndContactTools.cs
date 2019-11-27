namespace Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DroppedConnectorAndContactTools : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ToolConnectors", "Tool_Id", "dbo.Items");
            DropForeignKey("dbo.ToolConnectors", "Connector_Id", "dbo.Items");
            DropForeignKey("dbo.ContactTools", "Contact_Id", "dbo.Items");
            DropForeignKey("dbo.ContactTools", "Tool_Id", "dbo.Items");
            DropIndex("dbo.ToolConnectors", new[] { "Tool_Id" });
            DropIndex("dbo.ToolConnectors", new[] { "Connector_Id" });
            DropIndex("dbo.ContactTools", new[] { "Contact_Id" });
            DropIndex("dbo.ContactTools", new[] { "Tool_Id" });
            DropTable("dbo.ToolConnectors");
            DropTable("dbo.ContactTools");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ContactTools",
                c => new
                    {
                        Contact_Id = c.Int(nullable: false),
                        Tool_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Contact_Id, t.Tool_Id });
            
            CreateTable(
                "dbo.ToolConnectors",
                c => new
                    {
                        Tool_Id = c.Int(nullable: false),
                        Connector_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tool_Id, t.Connector_Id });

            CreateIndex("dbo.ContactTools", "Tool_Id");
            CreateIndex("dbo.ContactTools", "Contact_Id");
            CreateIndex("dbo.ToolConnectors", "Connector_Id");
            CreateIndex("dbo.ToolConnectors", "Tool_Id");
            AddForeignKey("dbo.ContactTools", "Tool_Id", "dbo.Items", "Id");
            AddForeignKey("dbo.ContactTools", "Contact_Id", "dbo.Items", "Id");
            AddForeignKey("dbo.ToolConnectors", "Connector_Id", "dbo.Items", "Id");
            AddForeignKey("dbo.ToolConnectors", "Tool_Id", "dbo.Items", "Id");
        }
    }
}
