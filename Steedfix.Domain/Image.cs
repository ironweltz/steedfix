using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Steedfix.Domain
{
    public class Image:EntityBase
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public byte[] ImageBytes { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string ContentType { get; set; }

        public virtual ICollection<Bike> Bikes { get; set; }
    }
}
