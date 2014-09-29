using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steedfix.Domain
{
    public class Bike:EntityBase
    {
        [Required]
        public string Title { get; set; }

        public Guid? ManufacturerId { get; set; }

        [ForeignKey("ManufacturerId")]
        public Manufacturer Manufacturer { get; set; }

        public int? Year { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ICollection<BikeLike> Likes { get; set; }
    }
}
