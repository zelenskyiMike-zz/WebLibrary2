namespace WebLibrary2.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitAuthorBook : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AuthorBooks", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.AuthorBooks", "ID");
            AddColumn("dbo.AuthorBooks", "BookID", c => c.Int(nullable: false, identity: false));
            CreateIndex("dbo.AuthorBooks", "BookID");
            AddColumn("dbo.AuthorBooks", "AuthorID", c => c.Int(nullable: false, identity: false));
            CreateIndex("dbo.AuthorBooks", "AuthorID");
            AddForeignKey("dbo.AuthorBooks", "AuthorID", "dbo.Authors", "AuthorID", cascadeDelete: true);
            AddForeignKey("dbo.AuthorBooks", "BookID", "dbo.Books", "BookID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AuthorBooks", "BookID", "dbo.Books");
            DropForeignKey("dbo.AuthorBooks", "AuthorID", "dbo.Authors");
            DropIndex("dbo.AuthorBooks", new[] { "AuthorID" });
            DropIndex("dbo.AuthorBooks", new[] { "BookID" });
            DropPrimaryKey("dbo.AuthorBooks");
            DropColumn("dbo.AuthorBooks", "ID");
            AddPrimaryKey("dbo.AuthorBooks", new[] { "BookID", "AuthorID" });
        }
    }
}
