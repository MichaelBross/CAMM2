namespace Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CodeColumnUniqueIndex : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Items", "Code", c => c.String(maxLength: 100));
            CreateIndex("dbo.Items", "Code", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Items", new[] { "Code" });
            AlterColumn("dbo.Items", "Code", c => c.String());
        }
    }
}
