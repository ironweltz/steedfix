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
                .HasMany(j => j.Items)
                .WithMany(i => i.Jobs)
                .Map(j => j.MapRightKey("JobId")
                    .MapLeftKey("ItemId")
                    .ToTable("JobItems"));

            modelBuilder.Entity<Bike>()
                .HasMany(b => b.Images)
                .WithMany(i => i.Bikes)
                .Map(b => b.MapRightKey("BikeId")
                    .MapLeftKey("ImageId")
                    .ToTable("BikeImages"));

            modelBuilder.Entity<Bike>()
                .HasMany(b => b.Tags)
                .WithMany(t => t.Bikes)
                .Map(j => j.MapRightKey("BikeId")
                    .MapLeftKey("TagId")
                    .ToTable("BikeTags"));

        }

        public DbSet<Bike> Bikes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        //public DbSet<Part> Parts { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<Tag> Tags { get; set; }
        //public DbSet<Tool> Tools { get; set; }

        
    }
}
