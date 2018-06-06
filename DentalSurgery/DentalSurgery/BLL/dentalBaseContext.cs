using DentalSurgery.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace DentalSurgery.BLL
{
    public class DentalBaseContext : IdentityDbContext<AppUser>
    {
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Surgery> Surgeries { get; set; }
        public DbSet<Opinion> Opinions { get; set; }
        public DbSet<Tooth> Teeth { get; set; }

        public DentalBaseContext() : base("DentalBaseConnectionString")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole>()
                .HasKey(k => new { k.RoleId, k.UserId })
                .ToTable("UserRoles");

            modelBuilder.Entity<IdentityUserLogin>()
                .HasKey(k => new { k.LoginProvider, k.ProviderKey, k.UserId })
                .ToTable("UserLogins");

            modelBuilder.Entity<Opinion>()
                .HasKey(k => new { k.OpinionId })
                .ToTable("Opinions");

        }
    }
}