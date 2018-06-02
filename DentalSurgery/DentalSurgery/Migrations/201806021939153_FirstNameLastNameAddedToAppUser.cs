namespace DentalSurgery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstNameLastNameAddedToAppUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppUsers", "FirstName", c => c.String());
            AddColumn("dbo.AppUsers", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppUsers", "LastName");
            DropColumn("dbo.AppUsers", "FirstName");
        }
    }
}
