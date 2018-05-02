namespace WebLibrary2.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatecontextmigr : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "BookName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "BookName", c => c.String(nullable: false));
        }
    }
}
