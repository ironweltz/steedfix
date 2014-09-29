using System.Collections.Generic;

namespace Steedfix.Domain
{
    public class Part:Item
    {
        public virtual ICollection<Job> Jobs { get; set; }
    }
}
