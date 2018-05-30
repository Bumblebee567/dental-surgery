namespace DentalSurgery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OpinionsAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Opinions",
                c => new
                    {
                        OpinionId = c.Guid(nullable: false),
                        Content = c.String(),
                        Date = c.DateTime(nullable: false),
                        Author_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.OpinionId)
                .ForeignKey("dbo.AppUsers", t => t.Author_Id)
                .Index(t => t.Author_Id);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Opinions", "Author_Id", "dbo.AppUsers");
            DropIndex("dbo.Opinions", new[] { "Author_Id" });
            DropTable("dbo.Opinions");
        }
    }
}
