using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steedfix.Domain
{
    public abstract class Item:EntityBase
    {
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Let the ref column be optional as users may not want to have to fill this in
        /// </summary>
        public string Ref { get; set; }

        [Required]
        public Guid ManufacturerId { get; set; }

        [ForeignKey("ManufacturerId")]
        public Manufacturer Manufacturer { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }
    }
}
