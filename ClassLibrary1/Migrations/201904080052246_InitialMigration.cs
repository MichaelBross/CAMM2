namespace Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Description = c.String(),
                        UnitsOfMeasure = c.Int(nullable: false),
                        QtyOnHand = c.Int(nullable: false),
                        IsObsolete = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        Rev = c.String(),
                        Manufacturer = c.String(),
                        Family = c.String(),
                        Comments = c.String(),
                        Manufacturer1 = c.String(),
                        BinNumber = c.String(),
                        MilitarySpecification = c.String(),
                        SerialNumber = c.String(),
                        Comments1 = c.String(),
                        Size = c.String(),
                        Family1 = c.String(),
                        WireGageMin = c.Int(),
                        WireGageMax = c.Int(),
                        Comments2 = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        CreatedBy_Id = c.Int(),
                        UpdatedBy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Users", t => t.UpdatedBy_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.UpdatedBy_Id);
            
            CreateTable(
                "dbo.AssemblyComponents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsObsolete = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        Assembly_Id = c.Int(),
                        Component_Id = c.Int(),
                        CreatedBy_Id = c.Int(),
                        UpdatedBy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.Assembly_Id)
                .ForeignKey("dbo.Items", t => t.Component_Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Users", t => t.UpdatedBy_Id)
                .Index(t => t.Assembly_Id)
                .Index(t => t.Component_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.UpdatedBy_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        EmailAddress = c.String(),
                        IsObsolete = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        UpdatedBy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Rev = c.String(),
                        Title = c.String(),
                        DocType = c.Int(nullable: false),
                        IsObsolete = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        CreatedBy_Id = c.Int(),
                        UpdatedBy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Users", t => t.UpdatedBy_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.UpdatedBy_Id);
            
            CreateTable(
                "dbo.ItemDocuments",
                c => new
                    {
                        Item_Id = c.Int(nullable: false),
                        Document_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Item_Id, t.Document_Id })
                .ForeignKey("dbo.Items", t => t.Item_Id, cascadeDelete: true)
                .ForeignKey("dbo.Documents", t => t.Document_Id, cascadeDelete: true)
                .Index(t => t.Item_Id)
                .Index(t => t.Document_Id);
            
            CreateTable(
                "dbo.ToolConnectors",
                c => new
                    {
                        Tool_Id = c.Int(nullable: false),
                        Connector_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tool_Id, t.Connector_Id })
                .ForeignKey("dbo.Items", t => t.Tool_Id)
                .ForeignKey("dbo.Items", t => t.Connector_Id)
                .Index(t => t.Tool_Id)
                .Index(t => t.Connector_Id);
            
            CreateTable(
                "dbo.ContactTools",
                c => new
                    {
                        Contact_Id = c.Int(nullable: false),
                        Tool_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Contact_Id, t.Tool_Id })
                .ForeignKey("dbo.Items", t => t.Contact_Id)
                .ForeignKey("dbo.Items", t => t.Tool_Id)
                .Index(t => t.Contact_Id)
                .Index(t => t.Tool_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssemblyComponents", "UpdatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.AssemblyComponents", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.AssemblyComponents", "Component_Id", "dbo.Items");
            DropForeignKey("dbo.Documents", "UpdatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ContactTools", "Tool_Id", "dbo.Items");
            DropForeignKey("dbo.ContactTools", "Contact_Id", "dbo.Items");
            DropForeignKey("dbo.ToolConnectors", "Connector_Id", "dbo.Items");
            DropForeignKey("dbo.ToolConnectors", "Tool_Id", "dbo.Items");
            DropForeignKey("dbo.Items", "UpdatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.ItemDocuments", "Document_Id", "dbo.Documents");
            DropForeignKey("dbo.ItemDocuments", "Item_Id", "dbo.Items");
            DropForeignKey("dbo.Items", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.Documents", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.AssemblyComponents", "Assembly_Id", "dbo.Items");
            DropIndex("dbo.ContactTools", new[] { "Tool_Id" });
            DropIndex("dbo.ContactTools", new[] { "Contact_Id" });
            DropIndex("dbo.ToolConnectors", new[] { "Connector_Id" });
            DropIndex("dbo.ToolConnectors", new[] { "Tool_Id" });
            DropIndex("dbo.ItemDocuments", new[] { "Document_Id" });
            DropIndex("dbo.ItemDocuments", new[] { "Item_Id" });
            DropIndex("dbo.Documents", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.Documents", new[] { "CreatedBy_Id" });
            DropIndex("dbo.AssemblyComponents", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.AssemblyComponents", new[] { "CreatedBy_Id" });
            DropIndex("dbo.AssemblyComponents", new[] { "Component_Id" });
            DropIndex("dbo.AssemblyComponents", new[] { "Assembly_Id" });
            DropIndex("dbo.Items", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.Items", new[] { "CreatedBy_Id" });
            DropTable("dbo.ContactTools");
            DropTable("dbo.ToolConnectors");
            DropTable("dbo.ItemDocuments");
            DropTable("dbo.Documents");
            DropTable("dbo.Users");
            DropTable("dbo.AssemblyComponents");
            DropTable("dbo.Items");
        }
    }
}
