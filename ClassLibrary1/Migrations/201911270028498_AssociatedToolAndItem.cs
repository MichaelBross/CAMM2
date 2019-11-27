namespace Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssociatedToolAndItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ToolItems",
                c => new
                    {
                        Tool_Id = c.Int(nullable: false),
                        Item_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tool_Id, t.Item_Id })
                .ForeignKey("dbo.Items", t => t.Tool_Id)
                .ForeignKey("dbo.Items", t => t.Item_Id)
                .Index(t => t.Tool_Id)
                .Index(t => t.Item_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToolItems", "Item_Id", "dbo.Items");
            DropForeignKey("dbo.ToolItems", "Tool_Id", "dbo.Items");
            DropIndex("dbo.ToolItems", new[] { "Item_Id" });
            DropIndex("dbo.ToolItems", new[] { "Tool_Id" });
            DropTable("dbo.ToolItems");
        }
    }
}
