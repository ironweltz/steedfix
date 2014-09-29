using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Steedfix.Domain
{
    public class Tag:EntityBase
    {
        [Required]
        public string Title { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<Bike> Bikes { get; set; }
    }
}
