using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;
using Steedfix.Domain;

namespace Steedfix.Data
{
    public class SteedfixContext : IdentityDbContext<User>
    {
        public SteedfixContext():base("DefaultConnection")
        {
            //Disable lazy loading. This can cause an error when json is returned by the web api.
            Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer(new DropCreateDatabaseAlways<SteedfixContext>());
        }

        public static SteedfixContext Create()
        {
            return new SteedfixContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<Job>()
                .HasMany(j => j.Tags)
                .WithMany(t => t.Jobs)
                .Map(j => j.MapRightKey("JobId")
                    .MapLeftKey("TagId")
                    .ToTable("JobTags"));

            modelBuilder.Entity<Job>()
                .HasMany(j => j.Parts)
                .WithMany(t => t.Jobs)
                .Map(j => j.MapRightKey("JobId")
                    .MapLeftKey("PartId")
                    .ToTable("JobParts"));

            modelBuilder.Entity<Job>()
                .HasMany(j => j.Tools)
                .WithMany(t => t.Jobs)
                .Map(j => j.MapRightKey("JobId")
                    .MapLeftKey("ToolId")
                    .ToTable("JobTools"));

            modelBuilder.Entity<Bike>()
                .HasMany(j => j.Images)
                .WithMany(t => t.Bikes)
                .Map(j => j.MapRightKey("BikeId")
                    .MapLeftKey("ImageId")
                    .ToTable("BikeImages"));

            modelBuilder.Entity<Bike>()
                .HasMany(j => j.Tags)
                .WithMany(t => t.Bikes)
                .Map(j => j.MapRightKey("BikeId")
                    .MapLeftKey("TagId")
                    .ToTable("BikeTags"));

        }

        public DbSet<Bike> Bikes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Tool> Tools { get; set; }


    }
}
