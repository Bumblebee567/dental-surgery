namespace DentalSurgery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ToothInSurgeryAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Surgeries", "Tooth", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Surgeries", "Tooth");
        }
    }
}
