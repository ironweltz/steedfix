using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Steedfix.Domain
{
    public class JobRating:Rating
    {
        public Guid JobId { get; set; }
        [ForeignKey("JobId")]
        public Job Job { get; set; }
    }
}
