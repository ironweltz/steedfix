using System.ComponentModel.DataAnnotations;

namespace Steedfix.Domain
{
    public abstract class Comment:EntityBase
    {
        [Required]
        public string Description { get; set; }
    }
}
