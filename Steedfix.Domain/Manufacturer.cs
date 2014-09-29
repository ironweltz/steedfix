using System.ComponentModel.DataAnnotations;

namespace Steedfix.Domain
{
    public class Manufacturer:EntityBase
    {
        [Required]
        public string Name { get; set; }
    }
}
