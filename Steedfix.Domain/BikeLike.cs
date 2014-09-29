using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steedfix.Domain
{
    public class BikeLike:Like
    {
        public Guid BikeId { get; set; }

        [ForeignKey("BikeId")]
        public Bike Bike { get; set; }
    }
}
