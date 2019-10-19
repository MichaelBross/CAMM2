namespace Persistance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFileNameAndPathToDocuments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "FileName", c => c.String());
            AddColumn("dbo.Documents", "Path", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Documents", "Path");
            DropColumn("dbo.Documents", "FileName");
        }
    }
}
