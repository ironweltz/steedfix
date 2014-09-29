using System.Collections.Generic;

namespace Steedfix.Domain
{
    public class Tool:Item
    {
        public virtual ICollection<Job> Jobs { get; set; }
    }
}
