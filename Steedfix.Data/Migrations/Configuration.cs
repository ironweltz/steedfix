using System.Collections.ObjectModel;
using System.IO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Steedfix.Data.Utils;
using Steedfix.Domain;

namespace Steedfix.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SteedfixContext>
    {
        private const string SteedfixAdmin = @"SteedfixAdmin";

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        /// <summary>
        /// Seed the database with some data to get us going
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(SteedfixContext context)
        {
            //Create some users
            var identityUtil = new IdentityUtil(context);
            identityUtil.SeedUsers(SteedfixAdmin);

            var adminUserId = identityUtil.GetUserId(SteedfixAdmin);

            //Create some images
            var imageUtil = new ImageUtil();
            imageUtil.SeedImages(context, adminUserId);

            SeedManufacturers(context, adminUserId);
            SeedTools(context, adminUserId);
            SeedParts(context, adminUserId);
            SeedJobs(context, adminUserId);
        }

        private void SeedJobs(SteedfixContext context, string createdByUserId)
        {
            var identityUtil = new IdentityUtil(context);
            var brad = identityUtil.GetUserId("BradleyWiggins");
            var tony = identityUtil.GetUserId("TonyMartin");
            var jens = identityUtil.GetUserId("JensVoigt");

            context.Jobs.AddOrUpdate(j => j.Title,
                new Job
                {
                    CreatedByUserId = createdByUserId,
                    Title = "Fix a front wheel puncture",
                    Description = "Detailed steps showing how to fix a front wheel puncture",
                    ImageId = context.Images.Single(i => i.FileName.Equals("font-wheel-flat-tyre.jpg")).Id,
                    Parts = context.Parts.ToList(),
                    Tools = context.Tools.ToList(),
                    Comments = new Collection<JobComment>()
                    {
                        new JobComment(){CreatedByUserId = brad,Description = "This is a really good procedure for fixing a puncture."},
                        new JobComment(){CreatedByUserId = tony,Description = "Very well written."},
                        new JobComment(){CreatedByUserId = jens,Description = "Excellent. I will try this right now."}
                    },
                    Likes = new Collection<JobLike>()
                    {
                        new JobLike(){CreatedByUserId = brad},
                        new JobLike(){CreatedByUserId = jens}
                    },
                    Ratings = new Collection<JobRating>()
                    {
                        new JobRating(){CreatedByUserId = brad,Value = 5},
                        new JobRating(){CreatedByUserId = tony,Value = 4},
                        new JobRating(){CreatedByUserId = jens,Value = 3}
                    },
                    Steps = new Collection<Step>()
                    {
                        new Step()
                        {
                            CreatedByUserId = createdByUserId,
                            Title = "Tools and parts",
                            Description = "These are the tools and parts you will need",
                            Order = 1,
                            ImageId = context.Images.Single(i=>i.FileName.Equals("puncture-repair-tools.jpg")).Id
                        },
                        new Step()
                        {
                            CreatedByUserId = createdByUserId,
                            Title = "Undo the quick release skewer",
                            Description = "Grip the quick release skewer and levering it open.",
                            Order = 2,
                            ImageId = context.Images.Single(i=>i.FileName.Equals("undo-skewer.jpg")).Id
                        },
                        new Step()
                        {
                            CreatedByUserId = createdByUserId,
                            Title = "Undo the brakes",
                            Description = "Using your left hand, pinch the cantilevers together to create slack in the cable. Using your right hand pull the cable up and out of the cable guide. ",
                            Order = 3,
                            ImageId = context.Images.Single(i=>i.FileName.Equals("undo-cantilever-brakes.jpg")).Id
                        },
                        new Step()
                        {
                            CreatedByUserId = createdByUserId,
                            Title = "Remove the wheel",
                            Description = "Lift the handlebars to remove the wheel, then stand the bike on it front forks.",
                            Order = 4,
                            ImageId = context.Images.Single(i=>i.FileName.Equals("stand-bike-up-on-fork.jpg")).Id
                        },
                        new Step()
                        {
                            CreatedByUserId = createdByUserId,
                            Title = "Begin removing the tyre",
                            Description = "Take a tyre lever andwedge it between the tyre bead and the wheel rim.",
                            Order = 5,
                            ImageId = context.Images.Single(i=>i.FileName.Equals("lever-tyre-rim.jpg")).Id
                        },
                        new Step()
                        {
                            CreatedByUserId = createdByUserId,
                            Title = "Clip the tyre lever",
                            Description = "Pull the tyre lever down and hook its end around a spoke.",
                            Order = 6,
                            ImageId = context.Images.Single(i=>i.FileName.Equals("lever-in-tyre-clipped.jpg")).Id
                        },
                        new Step()
                        {
                            CreatedByUserId = createdByUserId,
                            Title = "Work the tyre off the rim",
                            Description = "Reapeat the previous step with another lever, gradually working your way around the wheel.",
                            Order = 7,
                            ImageId = context.Images.Single(i=>i.FileName.Equals("lever-off-tyre.jpg")).Id
                        },
                        new Step()
                        {
                            CreatedByUserId = createdByUserId,
                            Title = "Pull the tube out of the tyre",
                            Description = "Insert your hand into the tyre and pull the tube out, starting from the opposite side to the valve. Then remove the valve.",
                            Order = 8,
                            ImageId = context.Images.Single(i=>i.FileName.Equals("pull-tube-from-tyre.jpg")).Id
                        },
                        new Step()
                        {
                            CreatedByUserId = createdByUserId,
                            Title = "Inflate the tube",
                            Description = "Use the hand pump to inflate the tube.",
                            Order = 9,
                            ImageId = context.Images.Single(i=>i.FileName.Equals("inflate-tube.jpg")).Id
                        },
                        new Step()
                        {
                            CreatedByUserId = createdByUserId,
                            Title = "Find the hole!",
                            Description = "Inspect the tube to find the hole. Listening for a hissing sound will help. If you can't find it, run the tube through a bucket of water and look for bubbles of air.",
                            Order = 10,
                            ImageId = context.Images.Single(i=>i.FileName.Equals("tube-in-water-bubble.jpg")).Id
                        },
                        new Step()
                        {
                            CreatedByUserId = createdByUserId,
                            Title = "Rough up the tube",
                            Description = "Once the hole is found, take the sandpaper in the puncture repair kit and rough up the area around the hole. This helps the glue to stick.",
                            Order = 11,
                            ImageId = context.Images.Single(i=>i.FileName.Equals("sand-tube.jpg")).Id,
                            Comments = new Collection<StepComment>()
                            {
                                new StepComment(){CreatedByUserId = jens, Description = "Don't sand it too much or you'll just get another puncture."}
                            }
                        },
                        new Step()
                        {
                            CreatedByUserId = createdByUserId,
                            Title = "Apply the glue",
                            Description = "Deflate the tube by pressing in the valve, then apply a thin layer of vulcanising glue a bit bigger than the size of a patch to the area around the hole. Leave the glue to become tacky for 5 minutes.",
                            Order = 12,
                            ImageId = context.Images.Single(i=>i.FileName.Equals("glue-on-tube.jpg")).Id
                        },
                        new Step()
                        {
                            CreatedByUserId = createdByUserId,
                            Title = "Apply the patch",
                            Description = "Peel the backing off a patch and press it over the hole. Once stuck down, peel off the protective film.",
                            Order = 13,
                            ImageId = context.Images.Single(i=>i.FileName.Equals("patch-on-tube.jpg")).Id,
                            Comments = new Collection<StepComment>()
                            {
                                new StepComment(){CreatedByUserId = brad,Description = "This bit is tricky!"},
                                new StepComment(){CreatedByUserId = tony,Description = "Yeah, I've wasted loads of patches here."},
                            }
                        },
                        new Step()
                        {
                            CreatedByUserId = createdByUserId,
                            Title = "Chalk dust the tube",
                            Description = "Scrape the chalk with your fingernail to cover the glued area of the tube in chalk dust. Rub the chalk dust in so that the patched area is not tacky.",
                            Order = 14,
                            ImageId = context.Images.Single(i=>i.FileName.Equals("chalk-on-tube.jpg")).Id
                        },
                        new Step()
                        {
                            CreatedByUserId = createdByUserId,
                            Title = "Inspect the tyre",
                            Description = "Run your finger around the inside of the tyre to locate any thorns or glass that might be stck in it. Remove these with pliers or by hand.",
                            Order = 15,
                            ImageId = context.Images.Single(i=>i.FileName.Equals("thorn-in-tyre.jpg")).Id
                        },
                        new Step()
                        {
                            CreatedByUserId = createdByUserId,
                            Title = "Re-fit the tube",
                            Description = "Re-fit the inner tube by inserting the valve into the rim first, then placing the remainder of the tube inside the tyre. Be careful not to twist the tube.",
                            Order = 16,
                            ImageId = context.Images.Single(i=>i.FileName.Equals("valve-in-rim-first.jpg")).Id
                        },
                        new Step()
                        {
                            CreatedByUserId = createdByUserId,
                            Title = "Re-fit the tyre",
                            Description = "Re-fit the tyre by first using your hands to lift the tyre bead over the rim, then use tyre levers when this becomes too difficult. Be careful not to catch the inner tube between the tyre bead and the rim.",
                            Order = 17,
                            ImageId = context.Images.Single(i=>i.FileName.Equals("puncture-fixed.jpg")).Id
                        },
                        new Step()
                        {
                            CreatedByUserId = createdByUserId,
                            Title = "Inflate the tyre",
                            Description = "Use your hand pump of track pump to re-inflate the tyre.",
                            Order = 18,
                            ImageId = context.Images.Single(i=>i.FileName.Equals("inflate-tyre.jpg")).Id
                        },
                        new Step()
                        {
                            CreatedByUserId = createdByUserId,
                            Title = "Re-fit the wheel",
                            Description = "Re-fit the wheel, close and redo the cantilever brakes and tighten and close the quick release skewer.",
                            Order = 19,
                            ImageId = context.Images.Single(i=>i.FileName.Equals("close-cantilever.jpg")).Id
                        }
                    }
                });
        }

        /// <summary>
        /// Create some manufacturers
        /// </summary>
        /// <param name="context"></param>
        /// <param name="createdByUserId"></param>
        private void SeedManufacturers(SteedfixContext context, string createdByUserId)
        {
            context.Manufacturers.AddOrUpdate(m => m.Name,
                new Manufacturer()
                {
                    CreatedByUserId = createdByUserId,
                    Name = "Park Tool"
                },
                new Manufacturer()
                {
                    CreatedByUserId = createdByUserId,
                    Name = "Blackburn"
                },
                new Manufacturer()
                {
                    CreatedByUserId = createdByUserId,
                    Name = "Generic"
                });
        }

        /// <summary>
        /// Create some tools
        /// </summary>
        /// <param name="context"></param>
        /// <param name="createdbyUserId"></param>
        private void SeedTools(SteedfixContext context, string createdbyUserId)
        {
            context.Tools.AddOrUpdate(t => t.Title,
                new Tool()
                {
                    CreatedByUserId = createdbyUserId,
                    Title = "Park Tool Tyre Levers - 3 X Hooked",
                    Ref = "TL1",
                    ManufacturerId = context.Manufacturers.Single(m => m.Name.Equals("Park Tool")).Id
                },
                new Tool()
                {
                    CreatedByUserId = createdbyUserId,
                    Title = "Hand Pump",
                    Ref = "",
                    ManufacturerId = context.Manufacturers.Single(m => m.Name.Equals("Blackburn")).Id
                },
                new Tool()
                {
                    CreatedByUserId = createdbyUserId,
                    Title = "Bucket of Water",
                    Ref = "",
                    ManufacturerId = context.Manufacturers.Single(m => m.Name.Equals("Generic")).Id
                },
                new Tool()
                {
                    CreatedByUserId = createdbyUserId,
                    Title = "Small Towel",
                    Ref = "",
                    ManufacturerId = context.Manufacturers.Single(m => m.Name.Equals("Generic")).Id
                }
                );
        }

        /// <summary>
        /// Create some parts
        /// </summary>
        /// <param name="context"></param>
        /// <param name="createdbyUserId"></param>
        private void SeedParts(SteedfixContext context, string createdbyUserId)
        {
            context.Parts.AddOrUpdate(p => p.Title,
                new Part()
                {
                    CreatedByUserId = createdbyUserId,
                    Title = "Park Tool Vulcanising Patch Kit",
                    Ref = "VP1",
                    ManufacturerId = context.Manufacturers.Single(m => m.Name.Equals("Park Tool")).Id
                },
                new Part()
                {
                    CreatedByUserId = createdbyUserId,
                    Title = "Park Tool Super Patch Kit ",
                    Ref = "GP-2",
                    ManufacturerId = context.Manufacturers.Single(m => m.Name.Equals("Park Tool")).Id
                });
        }
    }
}
