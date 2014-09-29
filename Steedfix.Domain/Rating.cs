using System.ComponentModel.DataAnnotations;

namespace Steedfix.Domain
{
    public abstract class Rating:EntityBase
    {
        [Required]
        public int Value { get; set; }
    }
}
