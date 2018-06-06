namespace DentalSurgery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ToothTableAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teeth",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Surgeries", "Tooth_Id", c => c.Guid());
            CreateIndex("dbo.Surgeries", "Tooth_Id");
            AddForeignKey("dbo.Surgeries", "Tooth_Id", "dbo.Teeth", "Id");
            DropColumn("dbo.Surgeries", "Tooth");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Surgeries", "Tooth", c => c.String());
            DropForeignKey("dbo.Surgeries", "Tooth_Id", "dbo.Teeth");
            DropIndex("dbo.Surgeries", new[] { "Tooth_Id" });
            DropColumn("dbo.Surgeries", "Tooth_Id");
            DropTable("dbo.Teeth");
        }
    }
}
