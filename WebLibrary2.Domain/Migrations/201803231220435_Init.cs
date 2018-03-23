namespace WebLibrary2.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AuthorBooks", "BookID", "dbo.Books");
            DropForeignKey("dbo.AuthorBooks", "AuthorID", "dbo.Authors");
            DropIndex("dbo.AuthorBooks", new[] { "BookID" });
            DropIndex("dbo.AuthorBooks", new[] { "AuthorID" });
            CreateTable(
                "dbo.BookAuthors",
                c => new
                    {
                        BookID = c.Int(nullable: false),
                        AuthorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookID, t.AuthorID })
                .ForeignKey("dbo.Authors", t => t.AuthorID, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.BookID, cascadeDelete: true)
                .Index(t => t.BookID)
                .Index(t => t.AuthorID);
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AuthorBooks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BookID = c.Int(nullable: false),
                        AuthorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.BookAuthors", "BookID", "dbo.Books");
            DropForeignKey("dbo.BookAuthors", "AuthorID", "dbo.Authors");
            DropIndex("dbo.BookAuthors", new[] { "AuthorID" });
            DropIndex("dbo.BookAuthors", new[] { "BookID" });
            DropTable("dbo.BookAuthors");
            CreateIndex("dbo.AuthorBooks", "AuthorID");
            CreateIndex("dbo.AuthorBooks", "BookID");
            AddForeignKey("dbo.AuthorBooks", "AuthorID", "dbo.Authors", "AuthorID", cascadeDelete: true);
            AddForeignKey("dbo.AuthorBooks", "BookID", "dbo.Books", "BookID", cascadeDelete: true);
            RenameTable(name: "dbo.BookAuthor1", newName: "BookAuthors");
        }
    }
}
