namespace DentalSurgery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                        AppUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .ForeignKey("dbo.AppUsers", t => t.AppUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.AppUser_Id);
            
            CreateTable(
                "dbo.Surgeries",
                c => new
                    {
                        SurgeryId = c.Guid(nullable: false),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.SurgeryId);
            
            CreateTable(
                "dbo.Visits",
                c => new
                    {
                        VisitId = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Patient_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.VisitId)
                .ForeignKey("dbo.AppUsers", t => t.Patient_Id)
                .Index(t => t.Patient_Id);
            
            CreateTable(
                "dbo.AppUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserId = c.Guid(nullable: false),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        AppUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUsers", t => t.AppUser_Id)
                .Index(t => t.AppUser_Id);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        AppUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AppUsers", t => t.AppUser_Id)
                .Index(t => t.AppUser_Id);
            
            CreateTable(
                "dbo.VisitSurgeries",
                c => new
                    {
                        Visit_VisitId = c.Guid(nullable: false),
                        Surgery_SurgeryId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Visit_VisitId, t.Surgery_SurgeryId })
                .ForeignKey("dbo.Visits", t => t.Visit_VisitId, cascadeDelete: true)
                .ForeignKey("dbo.Surgeries", t => t.Surgery_SurgeryId, cascadeDelete: true)
                .Index(t => t.Visit_VisitId)
                .Index(t => t.Surgery_SurgeryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VisitSurgeries", "Surgery_SurgeryId", "dbo.Surgeries");
            DropForeignKey("dbo.VisitSurgeries", "Visit_VisitId", "dbo.Visits");
            DropForeignKey("dbo.Visits", "Patient_Id", "dbo.AppUsers");
            DropForeignKey("dbo.UserRoles", "AppUser_Id", "dbo.AppUsers");
            DropForeignKey("dbo.UserLogins", "AppUser_Id", "dbo.AppUsers");
            DropForeignKey("dbo.IdentityUserClaims", "AppUser_Id", "dbo.AppUsers");
            DropForeignKey("dbo.UserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropIndex("dbo.VisitSurgeries", new[] { "Surgery_SurgeryId" });
            DropIndex("dbo.VisitSurgeries", new[] { "Visit_VisitId" });
            DropIndex("dbo.UserLogins", new[] { "AppUser_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "AppUser_Id" });
            DropIndex("dbo.Visits", new[] { "Patient_Id" });
            DropIndex("dbo.UserRoles", new[] { "AppUser_Id" });
            DropIndex("dbo.UserRoles", new[] { "IdentityRole_Id" });
            DropTable("dbo.VisitSurgeries");
            DropTable("dbo.UserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.AppUsers");
            DropTable("dbo.Visits");
            DropTable("dbo.Surgeries");
            DropTable("dbo.UserRoles");
            DropTable("dbo.IdentityRoles");
        }
    }
}
