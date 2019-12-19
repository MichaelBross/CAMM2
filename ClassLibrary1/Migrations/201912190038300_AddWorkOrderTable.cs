namespace Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWorkOrderTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        QtyToBuild = c.Int(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        CustomerPO = c.String(),
                        IsObsolete = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(nullable: false),
                        Assembly_Id = c.Int(),
                        CreatedBy_Id = c.Int(),
                        UpdatedBy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.Assembly_Id)
                .ForeignKey("dbo.Users", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Users", t => t.UpdatedBy_Id)
                .Index(t => t.Assembly_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.UpdatedBy_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkOrders", "UpdatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.WorkOrders", "CreatedBy_Id", "dbo.Users");
            DropForeignKey("dbo.WorkOrders", "Assembly_Id", "dbo.Items");
            DropIndex("dbo.WorkOrders", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.WorkOrders", new[] { "CreatedBy_Id" });
            DropIndex("dbo.WorkOrders", new[] { "Assembly_Id" });
            DropTable("dbo.WorkOrders");
        }
    }
}
