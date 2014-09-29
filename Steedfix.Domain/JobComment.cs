using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steedfix.Domain
{
    public class JobComment:Comment
    {
        public Guid JobId { get; set; }
        [ForeignKey("JobId")]
        public Job Job { get; set; }
    }
}
