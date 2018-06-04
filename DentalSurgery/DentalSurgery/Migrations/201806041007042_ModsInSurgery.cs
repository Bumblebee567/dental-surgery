namespace DentalSurgery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModsInSurgery : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Surgeries", "EstimatedTime", c => c.Double(nullable: false));
            AddColumn("dbo.Surgeries", "HasTooth", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Surgeries", "HasTooth");
            DropColumn("dbo.Surgeries", "EstimatedTime");
        }
    }
}
